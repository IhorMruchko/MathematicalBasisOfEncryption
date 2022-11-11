using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Math;


namespace MathematicalBasisOfEncryption.Extensions;

public static class StringExtension
{
    public static string FromHexToDec(this string value)
    {
        var builder = new StringBuilder();
        for (var i = 0; i < value.Length; i += 8)
        {
            builder.Append(long.Parse(value.Slice(i, 8), System.Globalization.NumberStyles.HexNumber));
        }
        return builder.ToString();
    }

    public static string Hash(this string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return String.Empty;
        }

        using var sha = new SHA256Managed();
        
        byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(message);
        byte[] hashBytes = sha.ComputeHash(textBytes);

        
        string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        return hash.FromHexToDec();
    }
    public static long ToLong(this string value, long onFail = 0)
    {
        return string.IsNullOrWhiteSpace(value) 
            ? onFail
            : long.Parse(value);
    }

    public static string Join<T>(this string connector, IEnumerable<T> toJoin)
    {
        return string.Join(connector, toJoin);
    }

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
