using MathematicalBasisOfEncryption.CipherBase;

namespace MathematicalBasisOfEncryption.Cipheres.CipherValidators.RSAValidators;

public class IntTupleKeyValidator : IKeyValidator
{
    public string Reason => "Key should be three integers tuple";

    public bool IsValid(object key)
    {
        return key is(int, int, int);
    }
}
