using System;
using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.CipherForceDecoders;

namespace MathematicalBasisOfEncryption.Cipheres;

public class GammaCipher : Cipher
{
    public const int ADDITIONAL_SYMBOLS = 10;

    private static readonly Random _generator = new ();
    private int _currentIndex;
    private bool _isNeedToInitGamma;

    public string Gamma { get; private set; }

    public override CipherForceDecoder ForceDecoder => new GammaCipherForceDecoder().SetCipher(this);

    public static string GenerateGamma(int messageLenght)
    {
        var builder = new StringBuilder();

        for (var i = 0; i < messageLenght + ADDITIONAL_SYMBOLS; i++)
        {
            builder.Append((char)_generator.Next(char.MaxValue));
        }

        return builder.ToString();
    }

    public override string Encode(string message)
    {
        if (_isNeedToInitGamma || Gamma == null)
        {
            Gamma = GenerateGamma(message.Length);
        }

        return Encrypt(message);
    }

    public override string Decode(string message)
    {
        return Gamma == null
            ? ""
            : Encrypt(message);
    }

    public override bool ValidateKey(object key)
    {
        return key is bool;
    }

    protected override char GetDecodedLetter(char letter)
    {
        return (char)(Gamma[_currentIndex] ^ letter);
    }

    protected override char GetEncodedLetter(char letter)
    {
        return (char)(Gamma[_currentIndex] ^ letter);
    }

    protected override Cipher SetDefaultKey()
    {
        _isNeedToInitGamma = false;
        return this;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        _isNeedToInitGamma = (bool)key;
        return this;
    }

    private string Encrypt(string message)
    {
        var builder = new StringBuilder();

        for (var i = 0; i < message.Length; i++)
        {
            _currentIndex = i;
            builder.Append(GetEncodedLetter(message[i]));
        }

        return builder.ToString();
    }
}
