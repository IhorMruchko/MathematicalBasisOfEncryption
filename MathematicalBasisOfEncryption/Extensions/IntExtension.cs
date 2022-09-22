namespace MathematicalBasisOfEncryption.Extensions
{
    using static System.Math;

    public static class IntExtension
    {
        public static bool IsPrime(this int value)
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

        public static bool IsCoprime(this int value, int another)
        {
            return value.GCD(another) == 1;
        }

        public static int GCD(this int value, int another)
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

        public static int ExponentPow(this int value, int power, int module)
        {
            var result = 1;
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

        public static int Invert(this int value, int modulus)
        {
            int t = 0, r = modulus, newT = 1, newR = value;

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
    }
}
