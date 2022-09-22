using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Cipheres.CipherValidators.RSAValidators;
using MathematicalBasisOfEncryption.Extensions;

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

    public long EValue { get; protected set; }

    public long N { get; protected set; }

    public long D { get; protected set; }

    public long Modulus { get; protected set; }

    public override CipherForceDecoder ForceDecoder => null;

    public string ValidationMessage { get; private set; }

    public override string Decode(string message)
    {
        var blockSize = GetBlockSize();
        var builder = new StringBuilder();

        for (var i = 0; i < message.Length; i += blockSize)
        {
            var numeric = message.Slice(i, blockSize).TrimStart('0').TryParse()
                .ExponentPow(D, N).ToString().NormalizeStart("0", blockSize);

            for (var j = 0; j < numeric.Length; j += 2)
            {
                builder.Append(CodingAlphabet[(int)numeric.Slice(j, 2).TrimStart('0')
                    .TryParse()]);
            }
        }

        return builder.ToString();
    }

    public override string Encode(string message)
    {
        var convertedToNumeric = string.Join(
            string.Empty,
            message.Select(c => CodingAlphabet[c].ToString("00")));
        var blockSize = GetBlockSize();
        var builder = new StringBuilder();

        for (var i = 0; i < convertedToNumeric.Length; i += blockSize)
        {
            builder.Append(convertedToNumeric.Slice(i, blockSize).TryParse()
                .ExponentPow(EValue, N).ToString().NormalizeStart("0", blockSize));
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
