using System;

namespace MathematicalBasisOfEncryption.Cipheres.CipheresValidators.TrithemiusCipherKeyConveters;

public class From2DVectorToInt : IKeyToIntConverter
{
    private int _a;
    private int _b;

    public int GetShift(int positionInMessage)
    {
        return (_a * positionInMessage) + _b;
    }

    public bool IsAbleToConvert(object key)
    {
        if (key is(int, int))
        {
            (_a, _b) = ((int, int))key;
            return true;
        }

        return false;
    }
}
