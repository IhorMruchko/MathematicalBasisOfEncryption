using System.Linq;
using MathematicalBasisOfEncryption.CipherBase;

namespace MathematicalBasisOfEncryption.CipherForceDecoders;

public class CeasarCipherForceDecoder : CipherForceDecoder
{
    public override object GetKey(string message, string encodedMessage)
    {
        var firstSymbolsOfMessage = string.Join("", message.Take(FIRST_SYMBOLS_AMOUNT));
        var firstSymbolsOfEncodedMessage = string.Join("", encodedMessage.Take(FIRST_SYMBOLS_AMOUNT));
        var power = Cipher.CodingAlphabet.Power;

        for (var i = 0; i < power; ++i)
        {
            var tryToDecodeMessage = Cipher
                .SetKey(i)
                .Decode(firstSymbolsOfEncodedMessage);
            if (IsForceDecoded(firstSymbolsOfMessage, tryToDecodeMessage))
            {
                return i;
            }
        }

        return null;
    }

    protected override string FormatKey(object probableKey)
    {
        return $"Key = {(int)probableKey}";
    }
}
