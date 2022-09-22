namespace MathematicalBasisOfEncryptionTest;

[TestClass]
public class EnglishCodingAlphabetTesting
{
    private static readonly CodingAlphabet _englishAlphabet = new EnglishCodingAlphabet();

    [TestMethod]
    public void EnglishCodingAlphabet_AlphabetLenght()
    {
        var alphabetLenght = _englishAlphabet.Power;

        Assert.IsTrue(alphabetLenght == 27);
    }

    [TestMethod]
    [DataRow(-54, ' ')]
    [DataRow(-53, 'a')]
    [DataRow(-25, 'b')]
    [DataRow(-10, 'q')]
    [DataRow(-1, 'z')]
    [DataRow(0, ' ')]
    [DataRow(1, 'a')]
    [DataRow(1, 'a')]
    [DataRow(26, 'z')]
    [DataRow(27, ' ')]
    [DataRow(30, 'c')]
    public void EnglishCodingAlphabet_GetLetterByIndex(int index, char letter)
    {
        var englishLetter = _englishAlphabet[index];

        Assert.IsTrue(englishLetter.Equals(letter), $"Letter should be {letter} but {englishLetter}");
    }

    [TestMethod]
    [DataRow(' ', 0)]
    [DataRow('ы', 0)]
    [DataRow('a', 1)]
    [DataRow('i', 9)]
    [DataRow('z', 26)]
    [DataRow('Z', 26)]
    public void EnglishCoddingAlphabet_IndexByLetter(char letter, int index)
    {
        var indexOfLetter = _englishAlphabet[letter];

        Assert.IsTrue(indexOfLetter == index,
            $"Value should be equal {index} but equals {indexOfLetter}");
    }
}