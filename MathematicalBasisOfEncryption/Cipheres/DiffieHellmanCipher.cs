using System;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Extensions;

namespace MathematicalBasisOfEncryption.Cipheres;

public class DiffieHellmanCipher : Cipher
{
    public long PValue { get; private set; }

    public long QValue { get; private set; }

    public long AValue { get; private set; }

    public long BValue { get; private set; }

    public long SecretKey { get; private set; }

    public override CipherForceDecoder ForceDecoder => null;

    public override string Decode(string message)
    {
        return message;
    }

    public override string Encode(string message)
    {
        return message;
    }

    public override bool ValidateKey(object key)
    {
        return key is(long, long, long, long) && ValidatePrimitives(key);
    }

    protected override char GetDecodedLetter(char letter)
    {
        return letter;
    }

    protected override char GetEncodedLetter(char letter)
    {
        return letter;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        (PValue, QValue, AValue, BValue) = ((long, long, long, long))key;
        CalculateSecretKey();
        return this;
    }

    protected override Cipher SetDefaultKey()
    {
        (PValue, QValue, AValue, BValue) = (23, 14, 22, 6);
        CalculateSecretKey();
        return this;
    }

    private static bool ValidatePrimitives(object key)
    {
        var (p, q, a, b) = ((long, long, long, long))key;
        return p.IsPrime() && p.HasPrimitiveRoot(q) && a < p && b < p;
    }

    private void CalculateSecretKey()
    {
        var aPublicKeyExponet = QValue.ExponentPow(AValue, PValue);

        var bPublicKeyExponet = QValue.ExponentPow(BValue, PValue);

        var commonSecretKeyFromA = bPublicKeyExponet.ExponentPow(AValue, PValue);

        var commonSecretKeyFromB = aPublicKeyExponet.ExponentPow(BValue, PValue);

        SecretKey = commonSecretKeyFromA == commonSecretKeyFromB ? commonSecretKeyFromA : -1;
    }
}
