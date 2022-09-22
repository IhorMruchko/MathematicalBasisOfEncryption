namespace MathematicalBasisOfEncryption.Cipheres.CipheresValidators.TrithemiusCipherKeyConveters;

public interface IKeyToIntConverter
{
    bool IsAbleToConvert(object key);

    int GetShift(int positionInMessage);
}
