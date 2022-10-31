using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Extensions;
using System;
using System.Linq;
using System.Text;

namespace MathematicalBasisOfEncryption.Cipheres;

public class LFSR : Cipher
{
    public const int BITS_IN_BYTE = 8;

    private string _key;

    public override CipherForceDecoder ForceDecoder => null;

    public override string Decode(string message)
    {
        var key = new StringBuilder(_key).ToString();
        return Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(message)
                                                          .Select(b => Generate(b, ref key))
                                                          .ToArray());
    }

    public override string Encode(string message)
    {
        var key = new StringBuilder(_key).ToString();
        return Encoding.Unicode.GetString(Encoding.Unicode.GetBytes(message)
                                                          .Select(b => Generate(b, ref key))
                                                          .ToArray());
    }

    public override bool ValidateKey(object key)
    {
        return key is string k && k.Length == 11;
    }

    protected override char GetDecodedLetter(char letter)
    {
        throw new System.NotImplementedException();
    }

    protected override char GetEncodedLetter(char letter)
    {
        throw new System.NotImplementedException();
    }

    protected override Cipher SetCurrentKey(object key)
    {
        _key = key as string;
        return this;
    }

    protected override Cipher SetDefaultKey()
    {
        _key = "01001000001";
        return this;
    }

    private static byte Generate(byte input, ref string key)
    {
        var stringRepresentation = Convert.ToString(input, 2).NormalizeStart("0", BITS_IN_BYTE);
        var result = new StringBuilder();
        foreach(var b in stringRepresentation)
        {
            result.Append(b ^ key[^1]);
            key = ((key[1] ^ key[4] ^ key[10] ^ 1) % 2).ToString() + key.Remove(key.Length - 1);
        }

        return (byte)Convert.ToInt32(result.ToString(), 2);
    }
}
