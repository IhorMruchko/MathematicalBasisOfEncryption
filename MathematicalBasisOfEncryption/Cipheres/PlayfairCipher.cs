using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathematicalBasisOfEncryption.CipherBase;
using MathematicalBasisOfEncryption.CipherBase.Alphabets;

namespace MathematicalBasisOfEncryption.Cipheres;

public class PlayfairCipher : Cipher
{
    protected class Position
    {
        public int WidthIndex;
        public int HeightIndex;

        public void ChangePosition(Position secondChangedPosition, int step=1)
        {
            if (IsOnSameWidth(secondChangedPosition))
            {
                WidthIndex = WidthIndex + step;
                secondChangedPosition.WidthIndex = WidthIndex;
                return;
            }

            if (IsOnSameHeight(secondChangedPosition))
            {
                HeightIndex = HeightIndex + step;
                secondChangedPosition.HeightIndex = HeightIndex;
                return;
            }

            (WidthIndex, secondChangedPosition.WidthIndex) = (secondChangedPosition.WidthIndex, WidthIndex);
        }
        public override string ToString()
        {
            return $"(W={WidthIndex}; H={HeightIndex})";
        }


        private bool IsOnSameHeight(Position secondChangedPosition)
        {
            return HeightIndex == secondChangedPosition.HeightIndex;
        }

        private bool IsOnSameWidth(Position secondChangedPosition)
        {
            return WidthIndex == secondChangedPosition.WidthIndex;
        }

        internal bool IsValid()
        {
            return WidthIndex >= 0 && HeightIndex >= 0;
        }
    }

    private const string DEFAULT_KEY = "default";
    private string _key;
    private char[,] _encodingTable;

    public override CipherForceDecoder ForceDecoder => null;

    public override string Decode(string message)
    {
        CreateTable(_key);
        var bigrams = CreateBigrams(message);
        var result = string.Join("", bigrams.Select(p => GetPair(p)));
        return message.Length % 2 == 0
            ? result
            : result.Remove(result.Length - 1);
    }

    public override string Encode(string message)
    {
        CreateTable(_key);
        var bigrams = CreateBigrams(message);
        var result = string.Join("", bigrams.Select(p => GetPair(p)));
        return message.Length % 2 == 0 
            ? result
            : result.Remove(result.Length - 1);
    }

    private string GetPair((char first, char second) p, int step = 1)
    {
        var (isFirstCapital, isSecondCapital) = (char.IsUpper(p.first), char.IsUpper(p.second));
        var (first, second) = (char.ToLower(p.first), char.ToLower(p.second));
        if (CodingAlphabet.IsAlphabetContains(first) || CodingAlphabet.IsAlphabetContains(second))
        {
            return $"{p.first}{p.second}";
        }
        var (firstPosition, secondPosition) = (GetPosition(first), GetPosition(second));
        if ((firstPosition.IsValid() && secondPosition.IsValid()) == false)
        {
            return $"{p.first}{p.second}";
        }
        firstPosition.ChangePosition(secondPosition, step);
        var (firstChar, secondChar) = (GetChar(firstPosition), GetChar(secondPosition));
        (firstChar, secondChar) = (isFirstCapital ? char.ToUpper(firstChar) : firstChar, isSecondCapital ? char.ToUpper(secondChar) : secondChar);
        return $"{firstChar}{secondChar}";
    }

    private List<(char first, char second)> CreateBigrams(string message)
    {
        var signToReplaceWith = CodingAlphabet is EnglishCodingAlphabet
            ? 'x'
            : 'ь';
        var bigrams = new List<(char, char)>(message.Length / 2);

        for (var i = 0; i < message.Length; i += 2)
        {
            if (i + 1 >= message.Length)
            {
                bigrams.Add((message[i], signToReplaceWith));
                continue;
            }

            bigrams.Add((message[i], message[i + 1]));
        }

        return bigrams;
    }

    private char GetChar(Position position)
    {
        try
        {
            return _encodingTable[
                (position.WidthIndex + _encodingTable.GetLength(0)) % _encodingTable.GetLength(0), 
                (position.HeightIndex + _encodingTable.GetLength(1)) % _encodingTable.GetLength(1)];
        }
        catch (Exception)
        {
            return ' ';
        }
    }

    private Position GetPosition(char value)
    {
        for(var i = 0; i < _encodingTable.GetLength(0); ++i)
        {
            for(var j = 0; j < _encodingTable.GetLength(1); ++j)
            {
                if (_encodingTable[i, j] == value)
                {
                    return new Position() { WidthIndex = i, HeightIndex = j };
                }
            }
        }
        return new Position() { HeightIndex = -1, WidthIndex = -1 };
    }

    public override bool ValidateKey(object key)
    {
        return key is string;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var i = 0; i < _encodingTable.GetLength(0); ++i)
        {
            for (var j = 0; j < _encodingTable.GetLength(1); ++j)
            {
                builder.Append(_encodingTable[i, j] == '\0'
                               ? "X" 
                               : _encodingTable[i, j]).Append(" ");
            }
            builder.AppendLine();
        }
        return builder.ToString();
    }

    protected override char GetDecodedLetter(char letter)
    {
        return char.MinValue;           
    }

    protected override char GetEncodedLetter(char letter)
    {
        return char.MaxValue;
    }

    protected override Cipher SetCurrentKey(object key)
    {
        if (key is string stringKey)
        {
            _key = stringKey;
            return this;
        }
        throw new ArgumentOutOfRangeException(nameof(key), "Key must be string object");
    }

    protected override Cipher SetDefaultKey()
    {
        _key = DEFAULT_KEY;
        return this;
    }

    private void CreateTable(string key)
    {           
        var (width, height, replaceTo, replaceWith) = CodingAlphabet is EnglishCodingAlphabet
               ? (5, 5, "j", "i")
               : (4, 8, "ґ", "г");
        
        var keyWithNoDuplicates 
            = (key + CodingAlphabet.Alphabet).ToLower()
                                             .Replace(" ", "")
                                             .Replace(replaceTo, replaceWith)
                                             .Distinct()
                                             .Where(c => CodingAlphabet.Alphabet.Contains(c))
                                             .ToList();

        _encodingTable = new char[width, height];

        for (var i = 0; i < _encodingTable.GetLength(0); ++i)
        {
            for (var j = 0; j < _encodingTable.GetLength(1); ++j)
            {
                _encodingTable[i, j] = keyWithNoDuplicates[i * height + j];
            }
        }
    }
}
