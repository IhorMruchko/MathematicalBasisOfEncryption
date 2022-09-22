namespace MathematicalBasisOfEncryptionTest;

[TestClass]
public class TritemiusCipherTest
{
    [TestMethod]
    public void TritemiusCiphet_ValidateKey_2DIntVector_TrueValue()
    {
        var isValid = new TrithemiusCipher().ValidateKey((5, -1));

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void TritemiusCiphet_ValidateKey_3DIntVector_TrueValue()
    {
        var isValid = new TrithemiusCipher().ValidateKey((-5, 1, 22));

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void TritemiusCiphet_ValidateKey_String_TrueValue()
    {
        var isValid = new TrithemiusCipher().ValidateKey("Hello");

        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void TritemiusCiphet_ValidateKey_WrongType_FlaseValue()
    {
        var isValid = new TrithemiusCipher().ValidateKey(1);

        Assert.IsFalse(isValid);
    }

    [TestMethod]
    [DataRow("  ")]
    [DataRow("Ігор")]
    [DataRow("ааИИо")]
    [DataRow("Привіт!")]
    [DataRow(" Як справи?")]
    public void TritemiusCiphet_Key2_5_UkraineAlphabet(string message)
    {
        var cipher = new TrithemiusCipher().SetKey((2, 5)).SetAlphabet(new UkraineCodingAlphabet());

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("  ")]
    [DataRow("Ігор")]
    [DataRow("ааИИо")]
    [DataRow("Привіт!")]
    [DataRow(" Як справи?")]
    public void TritemiusCiphet_Key3_2_5_UkraineAlphabet(string message)
    {
        var cipher = new TrithemiusCipher().SetKey((-3, 2, 5)).SetAlphabet(new UkraineCodingAlphabet());

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("  ")]
    [DataRow("Ігор")]
    [DataRow("ааИИо")]
    [DataRow("Привіт!")]
    [DataRow(" Як справи?")]
    public void TritemiusCiphet_KeyString_UkraineAlphabet(string message)
    {
        var cipher = new TrithemiusCipher().SetAlphabet(new UkraineCodingAlphabet()).SetKey("Привіт");

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("  ")]
    [DataRow("Ігор")]
    [DataRow("ааИИо")]
    [DataRow("Привіт!")]
    [DataRow(" Як справи?")]
    public void TritemiusCiphet_Key2_5_EnglishAlphabet(string message)
    {
        var cipher = new TrithemiusCipher().SetAlphabet(new EnglishCodingAlphabet()).SetKey("Привіт");

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("  ")]
    [DataRow("Hello")]
    [DataRow("AllPhabbet")]
    [DataRow("There is some text")]
    [DataRow("How's you douing, pal?")]
    public void TritemiusCiphet_Key3_2_5_EnglishAlphabet(string message)
    {
        var cipher = new TrithemiusCipher().SetAlphabet(new UkraineCodingAlphabet()).SetKey((3, 2, 5));

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("  ")]
    [DataRow("Hello")]
    [DataRow("AllPhabbet")]
    [DataRow("There is some text")]
    [DataRow("How's you douing, pal?")]
    public void TrithemiusCipher_KeyString_EnglishAlphabet(string message)
    {
        var cipher = new TrithemiusCipher().SetAlphabet(new UkraineCodingAlphabet()).SetKey("Hello");

        var encodedMessage = cipher.Encode(message);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage),
            $"Should be {message} but {decodedMessage}");
    }

    [TestMethod]
    [DataRow(2, 5)]
    [DataRow(-2, 5)]
    [DataRow(2, -5)]
    [DataRow(-2, -5)]
    [DataRow(154, 201)]
    [DataRow(154, -201)]
    [DataRow(-154, 201)]
    [DataRow(-154, -201)]
    public void TrithemiusCipher_UkraineAlphabet_ForceDecode_2DVector(int a, int b)
    {
        var message = "Привіт!";
        var cipher = new CeasarCipher().SetAlphabet(new UkraineCodingAlphabet()).SetKey((a, b));
        var encodedMessage = cipher.Encode(message);
        var forceDecodedValue = cipher.ForceDecoder.GetKey(message, encodedMessage);

        cipher.SetKey(forceDecodedValue);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage, System.StringComparison.InvariantCultureIgnoreCase),
            $"Shoudl be {message}, but {decodedMessage}");
    }

    [TestMethod]
    [DataRow(1, 3, 9)]
    [DataRow(1, 3, -9)]
    [DataRow(1, -3, 9)]
    [DataRow(1, -3, -9)]
    [DataRow(-1, 3, 9)]
    [DataRow(-1, 3, -9)]
    [DataRow(-1, -3, 9)]
    [DataRow(-1, -3, -9)]
    public void TrithemiusCipher_UkraineAlphabet_ForceDecode_3DVector(int a, int b, int c)
    {
        var message = "Привіт!";
        var cipher = new CeasarCipher().SetAlphabet(new UkraineCodingAlphabet()).SetKey((a, b, c));
        var encodedMessage = cipher.Encode(message);
        var forceDecodedValue = cipher.ForceDecoder.GetKey(message, encodedMessage);

        cipher.SetKey(forceDecodedValue);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage, System.StringComparison.InvariantCultureIgnoreCase),
            $"Shoudl be {message}, but {decodedMessage}");
    }

    [TestMethod]
    [DataRow("Стіна")]
    [DataRow("Зошит")]
    [DataRow("Парта")]
    public void TrithemiusCipher_UkraineAlphabet_ForceDecode_String(string key)
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
    [DataRow(2, 5)]
    [DataRow(-2, 5)]
    [DataRow(2, -5)]
    [DataRow(-2, -5)]
    [DataRow(154, 201)]
    [DataRow(154, -201)]
    [DataRow(-154, 201)]
    [DataRow(-154, -201)]
    public void TrithemiusCipher_EnglishAlphabet_ForceDecode_2DVector(int a, int b)
    {
        var message = "Hello!";
        var cipher = new TrithemiusCipher().SetAlphabet(new EnglishCodingAlphabet()).SetKey((a, b));
        var encodedMessage = cipher.Encode(message);
        var forceDecodedValue = cipher.ForceDecoder.GetKey(message, encodedMessage);

        cipher.SetKey(forceDecodedValue);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage, System.StringComparison.InvariantCultureIgnoreCase),
            $"Shoudl be {message}, but {decodedMessage}");
    }

    [TestMethod]
    [DataRow(1, 3, 9)]
    [DataRow(1, 3, -9)]
    [DataRow(1, -3, 9)]
    [DataRow(1, -3, -9)]
    [DataRow(-1, 3, 9)]
    [DataRow(-1, 3, -9)]
    [DataRow(-1, -3, 9)]
    [DataRow(-1, -3, -9)]
    public void TrithemiusCipher_EnglishAlphabet_ForceDecode_3DVector(int a, int b, int c)
    {
        var message = "Hello!";
        var cipher = new TrithemiusCipher().SetAlphabet(new EnglishCodingAlphabet()).SetKey((a, b, c));
        var encodedMessage = cipher.Encode(message);
        var forceDecodedValue = cipher.ForceDecoder.GetKey(message, encodedMessage);

        cipher.SetKey(forceDecodedValue);
        var decodedMessage = cipher.Decode(encodedMessage);

        Assert.IsTrue(message.Equals(decodedMessage, System.StringComparison.InvariantCultureIgnoreCase),
            $"Shoudl be {message}, but {decodedMessage}");
    }
}
