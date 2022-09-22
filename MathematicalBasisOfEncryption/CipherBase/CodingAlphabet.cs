namespace MathematicalBasisOfEncryption.CipherBase
{
    using System;

    public class CodingAlphabet
    {
        public string Alphabet { get; protected set; }

        public int Power => Alphabet.Length;

        public char this[int index]
        {
            get => GetLetterByIndex(index);
        }

        public int this[char letter]
        {
            get => IndexOfLetterInAlphabet(char.ToLower(letter));
        }

        public int IndexOfLetterInAlphabet(char letter)
        {
            return Math.Max(Alphabet.IndexOf(letter), 0);
        }

        public char GetLetterByIndex(int index)
        {
            var normalizedIndex = index < 0
                ? (index % Alphabet.Length) + Alphabet.Length
                : index >= Alphabet.Length
                    ? index % Alphabet.Length
                    : index;

            return Alphabet[normalizedIndex % Alphabet.Length];
        }

        public bool IsAlphabetContains(char letter)
        {
            return !Alphabet.Contains(char.ToLower(letter));
        }
    }
}