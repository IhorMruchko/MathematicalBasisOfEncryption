using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using static System.Math;

namespace MathematicalBasisOfEncryption.CipherForceDecoders;

public class VigenereCipherForceDecoder : CipherForceDecoder
{
    public override object GetKey(string message, string encodedMessage)
    {
        var alphabet = Cipher.CodingAlphabet;
        var builder = new StringBuilder();
        var messageLenght = Min(message.Length, encodedMessage.Length);

        for (var i = 0; i < messageLenght; ++i)
        {
            var keyIndex = alphabet[encodedMessage[i]] + alphabet.Power - alphabet[message[i]];
            builder.Append(alphabet[keyIndex]);
        }

        return builder.ToString();
    }

    protected override string FormatKey(object probableKey)
    {
        return (string)probableKey;
    }
}
