namespace MathematicalBasisOfEncryptionTest;

[TestClass]
public class DeffieHelmanCipherTesting
{
    [TestMethod]
    [DataRow(7, 3)]
    [DataRow(23, 5)]
    [DataRow(107, 5)]
    [DataRow(911, 17)]
    public void PrimeRootCheck_RightResult(long baseValue, long primitiveRoot) 
    {
        var result = baseValue.HasPrimitiveRoot(primitiveRoot);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow(7, 6)]
    [DataRow(23, 12)]
    [DataRow(107, 53)]
    [DataRow(911, 107)]
    public void PrimeRootCheck_WrongResult(long baseValue, long primitiveRoot)
    {
        var result = baseValue.HasPrimitiveRoot(primitiveRoot);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(23, 5, 4, 3, 18)]
    [DataRow(23, 5, 6, 15, 2)]
    public void DiffiHelmanCipher_SecretKeyGeneration_Valid(long p, long q, long a, long b, long res)
    {
        var diffiHellmanCipher = new DiffieHellmanCipher();

        diffiHellmanCipher.SetKey((p, q, a, b));

        Assert.AreEqual(res, diffiHellmanCipher.SecretKey);
    }
}
