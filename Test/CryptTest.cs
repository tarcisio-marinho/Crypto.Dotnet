using Crypto.Dotnet;
using NUnit.Framework;
using System.Text;

namespace Test
{
    public class CryptTest
    {
        [Test]
        public void SimpleCrypto()
        {
            var key = "minha senha";
            var originalText = "my_data_example";

            var encryptedBytes = Crypt.Encrypt(originalText, key);
            var decryptedText = Encoding.UTF8.GetString(Crypt.Decrypt(encryptedBytes, key));

            Assert.AreEqual(originalText, decryptedText);
            Assert.AreNotEqual(originalText, encryptedBytes);
        }

        [Test]
        public void EncryptBytes()
        {
            var key = "minha senha";
            var originalText = new byte[5] { 123, 255, 234, 22 , 213};

            var encryptedBytes = Crypt.Encrypt(originalText, key);
            var decryptedText = Crypt.Decrypt(encryptedBytes, key);

            Assert.AreEqual(originalText, decryptedText);
            Assert.AreNotEqual(originalText, encryptedBytes);
        }

        [Test]
        public void EncryptStringWithByteKey()
        {
            var originalText = "minha senha";
            var key = new byte[5] { 123, 255, 234, 22, 213 };

            var encryptedBytes = Crypt.Encrypt(originalText, key);
            var decryptedText = Encoding.UTF8.GetString(Crypt.Decrypt(encryptedBytes, key));

            Assert.AreEqual(originalText, decryptedText);
            Assert.AreNotEqual(originalText, encryptedBytes);
        }

        [Test]
        public void EncryptBytesWithByteKey()
        {
            var originalText = new byte[5] { 234, 155, 32, 99, 215 };
            var key = new byte[5] { 123, 255, 234, 22, 213 };

            var encryptedBytes = Crypt.Encrypt(originalText, key);
            var decryptedText = Crypt.Decrypt(encryptedBytes, key);

            Assert.AreEqual(originalText, decryptedText);
            Assert.AreNotEqual(originalText, encryptedBytes);
        }

    }
}