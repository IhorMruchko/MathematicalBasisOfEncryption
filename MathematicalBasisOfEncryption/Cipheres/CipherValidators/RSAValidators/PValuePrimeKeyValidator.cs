using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Extensions;

namespace MathematicalBasisOfEncryption.Cipheres.CipherValidators.RSAValidators;

public class PValuePrimeKeyValidator : IKeyValidator
{
    public string Reason => "p value should be prime";

    public bool IsValid(object key)
    {
        var (p, _, _) = ((int, int, int))key;
        return p.IsPrime();
    }
}
