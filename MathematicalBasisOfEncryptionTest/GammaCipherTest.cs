namespace MathematicalBasisOfEncryptionTest
{
    using MathematicalBasisOfEncryption.Cipheres;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GammaCipherTest
    {
        [TestMethod]
        public void GammaCiper_ValidateKey_BoolValue_True()
        {
            var cipher = new GammaCipher();

            var isValid = cipher.ValidateKey(false);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void GammaCiper_ValidateKey_IntValue_False()
        {
            var cipher = new GammaCipher();

            var isValid = cipher.ValidateKey(0);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        [DataRow("Hello")]
        [DataRow("Привіт")]
        [DataRow("How are you today?")]
        public void GammaCipher_GenerateGamma_ProperLenght(string message)
        {
            var gamma = GammaCipher.GenerateGamma(message.Length);

            Assert.AreEqual(message.Length + GammaCipher.ADDITIONAL_SYMBOLS, gamma.Length);
        }

        [TestMethod]
        [DataRow("Hello")]
        [DataRow("Привіт")]
        [DataRow("How are you today?")]
        public void GammaCipher_Encrypting(string message)
        {
            var cipher = new GammaCipher().SetKey(true);

            var encodedMessage = cipher.Encode(message);
            var decodedMessage = cipher.Decode(encodedMessage);

            Assert.IsTrue(message.Equals(decodedMessage,
                System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
