namespace MathematicalBasisOfEncryption.Cipheres.CipherValidators.RSAValidators;

using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Extensions;

public class EValueIsCoprimeKeyValidator : IKeyValidator
{
    public string Reason => "e should be co-prime with p-1 * q - 1";

    public bool IsValid(object key)
    {
        var (p, q, e) = ((int, int, int))key;
        return e.IsCoprime((p - 1) * (q - 1));
    }
}
