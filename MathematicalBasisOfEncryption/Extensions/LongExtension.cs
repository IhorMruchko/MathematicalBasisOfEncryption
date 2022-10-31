using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace MathematicalBasisOfEncryption.Extensions;

public static class LongExtension
{
    public static void ChangeTo(this ref long value, Func<long, bool> condition, Func<long, long> changer)
    {
        while(condition.Invoke(value) == false)
        {
            value = changer.Invoke(value);
        }
    }
    public static bool IsPrime(this long value)
    {
        for (var i = 2; i < Floor(Sqrt(value)); ++i)
        {
            if (value % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsCoprime(this long value, long another)
    {
        return value.GCD(another) == 1;
    }

    public static long GCD(this long value, long another)
    {
        while (value != 0 && another != 0)
        {
            if (value > another)
            {
                value %= another;
            }
            else
            {
                another %= value;
            }
        }

        return value + another;
    }

    public static long ExponentPow(this long value, long power, long module)
    {
        long result = 1;
        while (power != 0)
        {
            if (power % 2 == 1)
            {
                result = (result * value) % module;
            }

            power /= 2;
            value = (value * value) % module;
        }

        return result;
    }

    public static long Invert(this long value, long modulus)
    {
        long t = 0, r = modulus, newT = 1, newR = value;

        while (newR != 0)
        {
            var q = r / newR;
            (t, newT) = (newT, t - (q * newT));
            (r, newR) = (newR, r - (q * newR));
        }

        return r <= 1
            ? t < 0
                ? t + modulus
                : t
            : 0;
    }

    public static bool HasPrimitiveRoot(this long value, long possibleRoot)
    {
        var reminders = new List<long>((int)value - 1);
        for (var i = 1; i < value; ++i)
        {
            reminders.Add(possibleRoot.ExponentPow(i, value));
        }

        return reminders.Distinct().Count() == value - 1;
    }
}
