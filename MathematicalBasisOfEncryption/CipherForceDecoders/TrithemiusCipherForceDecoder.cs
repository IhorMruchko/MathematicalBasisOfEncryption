using System.Linq;
using MathematicalBasisOfEncryption.CipherBase;

namespace MathematicalBasisOfEncryption.CipherForceDecoders;

public class TrithemiusCipherForceDecoder : CipherForceDecoder
{
    public override object GetKey(string message, string encodedMessage)
    {
        var firstSymbolsOfMessage = string.Join("", message.Take(FIRST_SYMBOLS_AMOUNT));
        var firstSymbolsOfEncodedMessage = string.Join("", encodedMessage.Take(FIRST_SYMBOLS_AMOUNT));
        var power = Cipher.CodingAlphabet.Power;

        for (var a = 0; a < power; ++a)
        {
            for (var b = 0; b < power; ++b)
            {
                for (var c = 0; c < power; ++c)
                {
                    Cipher.SetKey((a, b, c));
                    if (IsForceDecoded(firstSymbolsOfMessage, Cipher.Decode(firstSymbolsOfEncodedMessage)))
                    {
                        return (a, b, c);
                    }
                }
            }
        }

        return null;
    }

    protected override string FormatKey(object probableKey)
    {
        var (A, B, C) = ((int, int, int))probableKey;
        return $"A = {A};\nB = {B};\nC = {C}";
    }
}
