namespace MathematicalBasisOfEncryptionTest;

[TestClass]
public class UkraineCodingAlphabetTesting
{
    private static readonly CodingAlphabet _ukrainianAlphabet = new UkraineCodingAlphabet();

    [TestMethod]
    public void UkraineCoddingAlphabet_AlphabetLenght()
    {
        var alphabetLenght = _ukrainianAlphabet.Power;

        Assert.IsTrue(alphabetLenght == 34);
    }

    [TestMethod]
    [DataRow(-34, ' ')]
    [DataRow(-33, 'а')]
    [DataRow(-6, 'ч')]
    [DataRow(-1, 'я')]
    [DataRow(8, 'є')]
    [DataRow(12, 'і')]
    [DataRow(13, 'ї')]
    [DataRow(14, 'й')]
    [DataRow(0, ' ')]
    [DataRow(33, 'я')]
    [DataRow(34, ' ')]
    [DataRow(35, 'а')]
    public void UkraineCoddingAlphabet_GetLetterByIndex(int index, char letter)
    {
        var letterWithIndexOf = _ukrainianAlphabet[index];

        Assert.IsTrue(letterWithIndexOf.Equals(letter),
            $"Value should be equal {letter} but equals {letterWithIndexOf}");
    }

    [TestMethod]
    [DataRow(' ', 0)]
    [DataRow('ы', 0)]
    [DataRow('а', 1)]
    [DataRow('і', 12)]
    public void UkraineCoddingAlphabet_IndexByLetter(char letter, int index)
    {
        var indexOfLetter = _ukrainianAlphabet[letter];

        Assert.IsTrue(indexOfLetter == index,
            $"Value should be equal {index} but equals {indexOfLetter}");
    }
}