namespace MathematicalBasisOfEncryptionTest;

[TestClass]
public class VigenereCipherTest
{
    [TestMethod]
    public void VigenereCipher_ValidateKey_StringKey_True()
    {
        var cipher = new VigenеreCipher();

        var isValid = cipher.ValidateKey("My name is ihor");

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("    ")]
    public void VigenereCipher_ValidateKey_StringKeyEmpty_False(string key)
    {
        var cipher = new VigenеreCipher();

        var isValid = cipher.ValidateKey(key);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void VigenereCipher_ValidateKey_BoolKey_False()
    {
        var cipher = new VigenеreCipher();

        var isValid = cipher.ValidateKey(true);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    public void VigenereCipher_ValidateKey_IntKey_False()
    {
        var cipher = new VigenеreCipher();

        var isValid = cipher.ValidateKey(342022);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [DataRow("Hello", "nice")]
    [DataRow("How are you?", "hello")]
    [DataRow("I'm fine, thank you", "lemon")]
    public void VigenereCipher_Encrypting_EnglishCodingAlphabet(string message, string key)
    {
        var cipher = new VigenеreCipher().SetKey(key).SetAlphabet(new EnglishCodingAlphabet());
        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage,
            StringComparison.InvariantCultureIgnoreCase));
    }

    [TestMethod]
    [DataRow("Привіт", "класно")]
    [DataRow("Як справи?", "привіт")]
    [DataRow("Я в порядку, дякую", "люся")]
    public void VigenereCipher_Encrypting_UrkaineCodingAlphabet(string message, string key)
    {
        var cipher = new VigenеreCipher().SetKey(key).SetAlphabet(new UkraineCodingAlphabet());
        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage,
            StringComparison.InvariantCultureIgnoreCase));
    }
}
