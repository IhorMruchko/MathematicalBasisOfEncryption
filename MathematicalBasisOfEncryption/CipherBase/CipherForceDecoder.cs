namespace MathematicalBasisOfEncryption.CipherBase
{
    using System;

    public abstract class CipherForceDecoder
    {
        protected const int FIRST_SYMBOLS_AMOUNT = 10;

        protected const string CANNOT_DECODE_RESULT = "Can not decode message";

        protected Cipher Cipher { get; private set; }

        public abstract object GetKey(string message, string encodedMessage);

        public string ForceDecode(string message, string encodedMessage)
        {
            var probableKey = GetKey(message, encodedMessage);

            return probableKey == null
                ? CANNOT_DECODE_RESULT
                : FormatKey(probableKey);
        }

        public CipherForceDecoder SetCipher(Cipher cipherWasUse)
        {
            Cipher = cipherWasUse;
            return this;
        }

        protected static bool IsForceDecoded(string firstSymbolsOfMessage, string tryToDecodeMessage)
        {
            return firstSymbolsOfMessage.Equals(tryToDecodeMessage,
                StringComparison.InvariantCultureIgnoreCase);
        }

        protected abstract string FormatKey(object probableKey);
    }
}