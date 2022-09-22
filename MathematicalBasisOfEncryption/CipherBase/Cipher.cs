using System;

namespace MathematicalBasisOfEncryption.CipherBase;

public abstract class Cipher
{
    public Cipher() { }

    protected Cipher(object key)
    {
        SetKey(key);
    }

    public CodingAlphabet CodingAlphabet { get; protected set; }

    public abstract CipherForceDecoder ForceDecoder { get; }

    public virtual Cipher SetKey(object key)
    {
        return ValidateKey(key)
           ? SetCurrentKey(key)
           : SetDefaultKey();
    }

    public virtual Cipher SetAlphabet(CodingAlphabet alphabet)
    {
        CodingAlphabet = alphabet;
        return this;
    }

    public abstract string Encode(string message);

    public abstract string Decode(string message);

    public abstract bool ValidateKey(object key);

    protected abstract char GetEncodedLetter(char letter);

    protected abstract char GetDecodedLetter(char letter);

    protected abstract Cipher SetDefaultKey();

    protected abstract Cipher SetCurrentKey(object key);

    protected char DefineCase(char c, Func<char, char> encryptor)
    {
        if (CodingAlphabet.IsAlphabetContains(c))
        {
            return c;
        }

        var encodedLetter = encryptor(c);

        return char.IsUpper(c)
            ? char.ToUpper(encodedLetter)
            : encodedLetter;
    }
}
