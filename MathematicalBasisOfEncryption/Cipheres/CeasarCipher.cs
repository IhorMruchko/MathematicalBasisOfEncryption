using System.Linq;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.CipherForceDecoders;

namespace MathematicalBasisOfEncryption.Cipheres;

public class CeasarCipher : Cipher
{
    private int _key;

    public override CipherForceDecoder ForceDecoder => new CeasarCipherForceDecoder().SetCipher(this);

    public override string Encode(string message)
    {
        return string.Join("", message.Select(c => DefineCase(c, GetEncodedLetter)));
    }

    public override string Decode(string message)
    {
        return string.Join("", message.Select(c => DefineCase(c, GetDecodedLetter)));
    }

    public override bool ValidateKey(object key)
    {
        return key is int;
    }

    protected override char GetEncodedLetter(char letter)
    {
        return CodingAlphabet[CodingAlphabet[letter] + (int)_key];
    }

    protected override char GetDecodedLetter(char letter)
    {
        return CodingAlphabet[CodingAlphabet[letter] - (int)_key];
    }

    protected override Cipher SetDefaultKey()
    {
        _key = 3;
        return this;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        _key = (int)key;
        return this;
    }
}
