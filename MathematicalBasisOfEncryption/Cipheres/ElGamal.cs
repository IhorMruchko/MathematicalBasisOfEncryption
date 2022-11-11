using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.CipherBase.Alphabets;
using MathematicalBasisOfEncryption.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MathematicalBasisOfEncryption.Cipheres;

public class ElGamal : Cipher
{
    private readonly Random _random = new ();

    private readonly RSACipher _digitalSignature = (RSACipher)new RSACipher(true).SetAlphabet(new EnglishNumericCodingAlphabet());

    private long _a;

    public const char OPENNING = '(';

    public const char CLOSING = ')';

    public const char SEPARATOR = ',';

    public const char SIGN_SEPARATOR = '|';

    public const string BAD_SIGN_ERROR = "ERROR: message is bad signed.";

    public override CipherForceDecoder ForceDecoder => null;

    public long P { get; private set; }

    public long Q { get; private set; }

    public long H { get; private set; }

    public long PRSA { get; private set; }

    public long QRSA { get; private set; }

    public long E { get; private set;  }

    public int R => _random.Next((int)P - 1);

    public override string Decode(string message)
    {
        var splittedText = message.Split(SIGN_SEPARATOR);
        var text = splittedText[0];
        var sign = splittedText[1];
        Debug.WriteLine(text.Hash().FromHexToDec());
        return text.Hash().FromHexToDec() == _digitalSignature.Decode(sign) 
            ? "".Join(text.Split(CLOSING).Select(block => DecodeBlock(block)))
            : BAD_SIGN_ERROR;
    }

    public override string Encode(string message)
    {
        SetRandoms();
        var blockSize = GetBlockSize();
        var convertedToNumeric = string.Join(
          string.Empty,
          message.Select(c => CodingAlphabet[c].ToString("00")));
        var builder = new StringBuilder();

        for (var i = 0; i < convertedToNumeric.Length; i += blockSize)
        {
            var r = R;
            builder.Append(OPENNING)
                   .Append(BigInteger.Pow(Q, r) % P)
                   .Append(SEPARATOR)
                   .Append((BigInteger)convertedToNumeric.Slice(i, blockSize).TryParse() * BigInteger.Pow(H, r) % P)
                   .Append(CLOSING);
        }

        var result = builder.ToString();

        return builder.Append(SIGN_SEPARATOR)
                      .Append(_digitalSignature.Encode(result.Hash()))
                      .ToString();
    }

    public override bool ValidateKey(object key)
    {
        if (key is (long, long, long, long, long))
        {
            var (p, q, pr, qr, e) = ((long, long, long, long, long))key;
            return p > 0 && p.IsPrime() && p.HasPrimitiveRoot(q) && _digitalSignature.ValidateKey((pr, qr, e));
        }
        return false;
    }

    protected override char GetDecodedLetter(char letter)
    {
        return letter;
    }

    protected override char GetEncodedLetter(char letter)
    {
        return letter;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        (P, Q, PRSA, QRSA, E) = ((long, long, long, long, long))key;  
        _digitalSignature.SetKey((PRSA, QRSA, E));
        return this;
    }

    protected override Cipher SetDefaultKey()
    {
        (P, Q) = (347, 134);
        _digitalSignature.SetKey((PRSA, QRSA, E));
        return this;
    }

    private void SetRandoms()
    {
        _a = _random.Next((int)P - 1);
        H = Q.ExponentPow(_a, P);
    }

    private int GetBlockSize()
    {
        for (var i = 1; i < 4; ++i)
        {
            var lowerBound = int.Parse(CodingAlphabet.Power.Repeat(i));
            var upperBound = int.Parse(CodingAlphabet.Power.Repeat(i + 1));
            if (P < upperBound && P > lowerBound)
            {
                return lowerBound.ToString().Length;
            }
        }

        return 0;
    }

    private string DecodeBlock(string block)
    {
        if (string.IsNullOrEmpty(block))
        {
            return string.Empty;
        }
        var slices = block.Split(SEPARATOR);
        var (c1, c2) = (slices[0].TrimStart(OPENNING).ToLong(), slices[1].ToLong());
        var x = BigInteger.Pow(c1, (int)_a) % P;
        return CodingAlphabet[(int)(c2 * BigInteger.Pow(x, (int)P - 2) % P)].ToString();
    }
}
