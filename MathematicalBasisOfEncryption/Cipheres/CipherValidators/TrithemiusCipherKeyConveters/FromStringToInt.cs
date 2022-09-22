using MathematicalBasisOfEncryption.CipherBase;

namespace MathematicalBasisOfEncryption.Cipheres.CipheresValidators.TrithemiusCipherKeyConveters;

internal class FromStringToInt : IKeyToIntConverter
{
    private readonly Cipher _cipher;
    private string _motto;

    public FromStringToInt(Cipher cipher)
    {
        _cipher = cipher;
    }

    public int GetShift(int positionInMessage)
    {
        return positionInMessage +
            _cipher.CodingAlphabet[_motto[positionInMessage % _motto.Length]];
    }

    public bool IsAbleToConvert(object key)
    {
        if (key is string value)
        {
            _motto = value;
            return true;
        }

        return false;
    }
}
