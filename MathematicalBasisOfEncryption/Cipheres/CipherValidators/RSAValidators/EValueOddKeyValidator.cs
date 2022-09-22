
using MathematicalBasisOfEncryption.CipherBase;

namespace MathematicalBasisOfEncryption.Cipheres.CipherValidators.RSAValidators;

public class EValueOddKeyValidator : IKeyValidator
{
    public string Reason => "e value should be odd";

    public bool IsValid(object key)
    {
        var (_, _, e) = ((int, int, int))key;
        return e % 2 == 1;
    }
}
