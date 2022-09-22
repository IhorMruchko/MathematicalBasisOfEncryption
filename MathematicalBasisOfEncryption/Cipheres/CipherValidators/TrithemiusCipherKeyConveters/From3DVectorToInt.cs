using System;
using static System.Math;

namespace MathematicalBasisOfEncryption.Cipheres.CipheresValidators.TrithemiusCipherKeyConveters;

public class From3DVectorToInt : IKeyToIntConverter
{
    private int _a;
    private int _b;
    private int _c;

    public int GetShift(int positionInMessage)
    {
        return (_a * (int)Pow(positionInMessage, 2))
            + (_b * positionInMessage)
            + _c;
    }

    public bool IsAbleToConvert(object key)
    {
        if (key is(int, int, int))
        {
            (_a, _b, _c) = ((int, int, int))key;
            return true;
        }

        return false;
    }
}
