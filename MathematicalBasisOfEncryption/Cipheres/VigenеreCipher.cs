using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.CipherForceDecoders;

namespace MathematicalBasisOfEncryption.Cipheres;

public class VigenеreCipher : Cipher
{
    private int _currentIndex;
    private string _key;

    public override CipherForceDecoder ForceDecoder => new VigenereCipherForceDecoder().SetCipher(this);

    public override string Encode(string message)
    {
        var builder = new StringBuilder();

        for (var i = 0; i < message.Length; ++i)
        {
            _currentIndex = i % _key.Length;
            builder.Append(DefineCase(message[i], GetEncodedLetter));
        }

        return builder.ToString();
    }

    public override string Decode(string message)
    {
        var builder = new StringBuilder();

        for (var i = 0; i < message.Length; ++i)
        {
            _currentIndex = i % _key.Length;
            builder.Append(DefineCase(message[i], GetDecodedLetter));
        }

        return builder.ToString();
    }

    public override bool ValidateKey(object key)
    {
        return key is string k && k.Trim().Length > 0;
    }

    protected override char GetEncodedLetter(char letter)
    {
        var indexOfKeyValue = CodingAlphabet[_key[_currentIndex]];
        return CodingAlphabet[CodingAlphabet[letter] + indexOfKeyValue];
    }

    protected override char GetDecodedLetter(char letter)
    {
        var indexOfKeyValue = CodingAlphabet[_key[_currentIndex]];
        return CodingAlphabet[CodingAlphabet[letter] - indexOfKeyValue];
    }

    protected override Cipher SetDefaultKey()
    {
        _key = "default";
        return this;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        _key = (string)key;
        return this;
    }
}
