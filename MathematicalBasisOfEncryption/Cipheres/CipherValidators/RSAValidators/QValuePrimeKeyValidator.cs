using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.Extensions;

namespace MathematicalBasisOfEncryption.Cipheres.CipherValidators.RSAValidators;

public class QValuePrimeKeyValidator : IKeyValidator
{
    public string Reason => "q value should be prime";

    public bool IsValid(object key)
    {
        var (_, q, _) = ((int, int, int))key;
        return q.IsPrime();
    }
}
