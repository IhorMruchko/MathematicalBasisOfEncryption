namespace MathematicalBasisOfEncryption.CipherBase
{
    public interface IKeyValidator
    {
        string Reason { get; }

        bool IsValid(object key);
    }
}