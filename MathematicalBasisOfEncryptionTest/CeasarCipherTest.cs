namespace MathematicalBasisOfEncryptionTest;

[TestClass]
public class CeasarCipherTest
{
    [TestMethod]
    [DataRow("абв")]
    [DataRow("яюб")]
    [DataRow("ьщш")]
    [DataRow("привіт")]
    public void CaesarCipher_KeyNegagtive13_UkraineAlphabet(string message)
    {
        var cipher = new CeasarCipher().SetKey(-13).SetAlphabet(new UkraineCodingAlphabet());

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("абв")]
    [DataRow("яюб")]
    [DataRow("ьщш")]
    [DataRow("привіт")]
    public void CaesarCipher_Key3_UkraineAlphabet(string message)
    {
        var cipher = new CeasarCipher().SetKey(3).SetAlphabet(new UkraineCodingAlphabet());

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("абв")]
    [DataRow("яюб")]
    [DataRow("ьщш")]
    [DataRow("привіт")]
    public void CaesarCipher_Key52_UkraineAlphabet(string message)
    {
        var cipher = new CeasarCipher().SetKey(52).SetAlphabet(new UkraineCodingAlphabet());

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("Hello")]
    [DataRow("xmas")]
    [DataRow("AbCdEEfxzZx")]
    [DataRow("Language")]
    public void CaesarCipher_Key3_EnglishAlphabet(string message)
    {
        var cipher = new CeasarCipher().SetKey(3).SetAlphabet(new EnglishCodingAlphabet());

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("Hello")]
    [DataRow("xmas")]
    [DataRow("AbCdEEfxzZx")]
    [DataRow("Language")]
    public void CaesarCipher_Key52_EnglishAlphabet(string message)
    {
        var cipher = new CeasarCipher().SetKey(52).SetAlphabet(new EnglishCodingAlphabet());

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(-1)]
    [DataRow(654)]
    [DataRow(-281)]
    [DataRow(512)]
    public void CeasarCipehr_UkraineAlphabet_ForceDecode(int key)
    {
        var message = "Привіт!";
        var cipher = new CeasarCipher().SetAlphabet(new UkraineCodingAlphabet()).SetKey(key);
        var encodedMessage = cipher.Encode(message);
        var forceDecodedValue = cipher.ForceDecoder.GetKey(message, encodedMessage);

        cipher.SetKey(forceDecodedValue);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage, System.StringComparison.InvariantCultureIgnoreCase),
            $"Shoudl be {message}, but {decodedMessage}");
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(-1)]
    [DataRow(654)]
    [DataRow(-281)]
    [DataRow(512)]
    public void CeasarCipehr_EnglishAlphabet_ForceDecode(int key)
    {
        var message = "Hello!";
        var cipher = new CeasarCipher().SetAlphabet(new EnglishCodingAlphabet()).SetKey(key);
        var encodedMessage = cipher.Encode(message);

        var forceDecodedValue = cipher.ForceDecoder.GetKey(message, encodedMessage);
        cipher.SetKey(forceDecodedValue);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage, System.StringComparison.InvariantCultureIgnoreCase),
            $"Shoudl be {message}, but {decodedMessage}");
    }
}