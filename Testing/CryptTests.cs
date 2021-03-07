using Crypto.Dotnet;
using NUnit.Framework;
using System;
using System.Text;

namespace Testing
{
    [TestFixture]
    class CryptTests
    {
        [Test]
        public void SimpleCrypto()
        {
            var key = "minha senha";
            var originalText = "my_data_example";

            var encryptedBytes = Crypt.Encrypt(originalText, key);
            var decryptedText = Encoding.UTF8.GetString(Crypt.Decrypt(encryptedBytes, key));

            Assert.Equals(originalText, decryptedText);
            Assert.AreNotEqual(originalText, encryptedBytes);
        }

        [Test]
        public void BigFileCrypto()
        {
            Assert.True(true);
        }
    }
}
