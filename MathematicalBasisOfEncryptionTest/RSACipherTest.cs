namespace MathematicalBasisOfEncryptionTest;

[TestClass]
public class RSACipherTest
{
    [TestMethod]
    [DataRow(53, 67, 17)]
    [DataRow(1, 2, 1)]
    [DataRow(5, 11, 7)]
    [DataRow(17, 13, 17)]
    public void RSAValidateKey_PrimeValuesTuple_True(int p, int q, int e)
    {
        var cipher = new RSACipher();

        var isValid = cipher.ValidateKey((p, q, e));

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [DataRow(54, 22, 21)]
    [DataRow(12, 7, 17)]
    [DataRow(4, 12, 6)]
    public void RSAValidateKey_NotPrimeValues_False(int p, int q, int e)
    {
        var cipher = new RSACipher();

        var isValid = cipher.ValidateKey((p, q, e));

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [DataRow("Hello")]
    [DataRow(false)]
    [DataRow('a')]
    public void RSAValidateKey_NotTuple_False(object key)
    {
        var cipher = new RSACipher();

        var isValid = cipher.ValidateKey(key);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [DataRow(3, 14, 6541, 1498)]
    [DataRow(1423, 17, 3551, 3153)]
    [DataRow(1910, 17, 3551, 2335)]
    public void Exponential(int value, int power, int module, int result)
    {
        var pow = value.ExponentPow(power, module);

        Assert.IsTrue(result.Equals(pow),
            $"Should be {result} but {pow}");
    }

    [TestMethod]
    [DataRow(5, 3431, 2745)]
    [DataRow(5, 3432, 1373)]
    [DataRow(17, 3432, 1817)]
    public void Invert(int value, int module, int result)
    {
        var res = value.Invert(module);

        Assert.IsTrue(result.Equals(res),
            $"Should be {result} but {res}");
    }
}
