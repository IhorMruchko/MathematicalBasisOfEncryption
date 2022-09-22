using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using static System.Math;

namespace MathematicalBasisOfEncryption.CipherForceDecoders;

public class GammaCipherForceDecoder : CipherForceDecoder
{
    public override object GetKey(string message, string encodedMessage)
    {
        var builder = new StringBuilder();
        var lowerBoundOfMessages = Min(message.Length, encodedMessage.Length);

        for (var i = 0; i < lowerBoundOfMessages; ++i)
        {
            builder.Append(XorChar(message[i], encodedMessage[i]));
        }

        return builder.ToString();
    }

    protected override string FormatKey(object probableKey)
    {
        return (string)probableKey;
    }

    private static char XorChar(char v1, char v2)
    {
        return (char)(v1 ^ v2);
    }
}
