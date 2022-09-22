namespace MathematicalBasisOfEncryption.Extensions
{
    using System.Linq;
    using System.Text;
    using static System.Math;

    public static class StringExtension
    {
        public static string Repeat(this object value, int times)
        {
            return string.Concat(Enumerable.Repeat(value, times));
        }

        public static string Slice(this string value, int startFrom, int blockSize)
        {
            return string.Join(string.Empty, value.Skip(startFrom).Take(blockSize));
        }

        public static string NormalizeStart(this string value, string filler, int amount)
        {
            return filler.Repeat(Abs(value.Length - amount)) + value;
        }

        public static string NormalizeEnd(this string value, string filler, int amount)
        {
            return value + filler.Repeat(amount);
        }

        public static long TryParse(this string value)
        {
            var isParsed = long.TryParse(value, out long result);
            return isParsed ? result : 0;
        }
    }
}
