using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres.CipherValidators.RSAValidators;
using MathematicalBasisOfEncryption.Extensions;
using static System.Math;

namespace MathematicalBasisOfEncryption.Cipheres;

public class RSACipher : Cipher
{
    private readonly List<IKeyValidator> _validators = new ()
    {
        new IntTupleKeyValidator(),
        new PValuePrimeKeyValidator(),
        new QValuePrimeKeyValidator(),
        new EValueOddKeyValidator(),
        new EValueIsCoprimeKeyValidator()
    };

    private readonly bool _isSign;

    public RSACipher(bool isSign = false)
    {
        _isSign = isSign;
    }

    public long EValue { get; protected set; }

    public long N { get; protected set; }

    public long D { get; protected set; }

    public long Modulus { get; protected set; }

    public override CipherForceDecoder ForceDecoder => null;

    public string ValidationMessage { get; private set; }

    public override string Decode(string message)
    {
       return _isSign 
            ? DesingMessage(message)
            : DecodeMessage(message);
    }

    private string DesingMessage(string message)
    {
        var normalizedBlockSize = (int)Floor(Log10(N));
        var builder = new StringBuilder();
        var blocks = message.Split().Where(t => t.Length > 0).ToArray();
        for (var i = 0; i < blocks.Length - 1; ++i)
        {
            builder.Append(blocks[i].ToLong()
                                    .ExponentPow(EValue, N)
                                    .ToString()
                                    .NormalizeStart("0", normalizedBlockSize));
        }
        builder.Append(blocks[^1].ToLong().ExponentPow(EValue, N));

        return builder.ToString();
    }

    private string DecodeMessage(string message)
    {
        var blockSize = GetBlockSize();
        var builder = new StringBuilder();

        for (var i = 0; i < message.Length; i += blockSize)
        {
            var numeric = message.Slice(i, blockSize).TrimStart('0').TryParse()
                .ExponentPow(D, N).ToString().NormalizeStart("0", blockSize);

            for (var j = 0; j < numeric.Length; j += 2)
            {
                builder.Append(CodingAlphabet[(int)numeric.Slice(j, 2).TrimStart('0').TryParse()]);
            }
        }
        
        return builder.ToString();
    }

    public override string Encode(string message)
    {
        return _isSign
            ? SingMessage(message)
            : EncodeMessage(message);
       
    }

    private string SingMessage(string message)
    {
        var blockSize = (int)Floor(Log10(N));
        message = message.FromHexToDec();
        var builder = new StringBuilder();

        for (var i = 0; i < message.Length; i += blockSize)
        {
            builder.Append(message.Slice(i, blockSize)
                                  .ToLong()
                                  .ExponentPow(D, N))
                   .Append(' ');
        }

        return builder.ToString();
    }

    private string EncodeMessage(string message)
    {
        var blockSize = GetBlockSize();
        var builder = new StringBuilder();

        for (var i = 0; i < message.Length; i += blockSize)
        {
            var numeric = message.Slice(i, blockSize).TrimStart('0').TryParse()
                .ExponentPow(EValue, N).ToString().NormalizeStart("0", blockSize);

            for (var j = 0; j < numeric.Length; j += 2)
            {
                builder.Append(CodingAlphabet[(int)numeric.Slice(j, 2).TrimStart('0').TryParse()]);
            }
        }

        return builder.ToString();
    }

    public override bool ValidateKey(object key)
    {
        ValidationMessage = _validators.FirstOrDefault(v => v.IsValid(key) == false)?.Reason;
        return ValidationMessage == null;
    }

    protected override char GetDecodedLetter(char letter)
    {
        return char.MinValue;
    }

    protected override char GetEncodedLetter(char letter)
    {
        return char.MinValue;
    }

    protected override Cipher SetDefaultKey()
    {
        EValue = 17;
        N = 53 * 67;
        Modulus = 52 * 66;
        D = EValue.Invert(Modulus);
        return this;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        var (p, q, e) = ((int, int, int))key;
        N = p * q;
        EValue = e;
        Modulus = (p - 1) * (q - 1);
        D = EValue.Invert(Modulus);
        return this;
    }

    private int GetBlockSize()
    {
        for (var i = 1; i < 4; ++i)
        {
            var lowerBound = int.Parse(CodingAlphabet.Power.Repeat(i));
            var upperBound = int.Parse(CodingAlphabet.Power.Repeat(i + 1));
            if (N < upperBound && N > lowerBound)
            {
                return lowerBound.ToString().Length;
            }
        }

        return 0;
    }
}
