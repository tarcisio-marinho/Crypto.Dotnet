using Crypto.Dotnet.Core.Conversion;
using Crypto.Dotnet.Core.Hash;
using Crypto.Dotnet.Core.Random;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Crypto.Dotnet.Core.Symmetric
{
    internal class GCM : IDisposable
    {
        private byte[] _key;
        private byte[] _nonce;
        private int keySize = 32;
        private int nonceSize = 12;

        public GCM()
        {

        }

        public GCM(byte[] key)
        {
            _key = key;
            _key = SHA256Hash.Hash(_key);
            _nonce = new byte[nonceSize];
            _nonce = RandomBytes.Generate(nonceSize);
        }

        public GCM(string key)
        {
            _key = ByteConversion.StringToByte(key);
            _key = SHA256Hash.Hash(_key);
            _nonce = new byte[nonceSize];
            _nonce = RandomBytes.Generate(nonceSize);
        }

        // TODO: validate
        public byte[] Encrypt(byte[] data)
        {
            byte[] tag = new byte[keySize];
            byte[] cipherText = new byte[data.Length];

            using (var cipher = new AesGcm(_key))
            {
                cipher.Encrypt(_nonce, data, cipherText, tag, null);
                return ByteConversion.Concat(tag, ByteConversion.Concat(_nonce, cipherText));
            }
        }

        // TODO: validate
        public byte[] Decrypt(byte[] data)
        {
            byte[] tag = ByteConversion.SubArray(data, 0, keySize);
            byte[] nonce = ByteConversion.SubArray(data, keySize, nonceSize);

            byte[] toDecrypt = ByteConversion.SubArray(data, keySize + nonceSize, data.Length - tag.Length - nonce.Length);
            byte[] decryptedData = new byte[toDecrypt.Length];

            using (var cipher = new AesGcm(_key))
            {
                cipher.Decrypt(nonce, toDecrypt, tag, decryptedData, null);

                return decryptedData;
            }
        }

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            var nonceSize = AesGcm.NonceByteSizes.MinSize;
            var tagSize = AesGcm.TagByteSizes.MinSize;

            byte[] tag = new byte[tagSize]; // TODO: Generate random bytes to tag ?
            byte[] cipherText = new byte[data.Length];
            byte[] _nonce = RandomBytes.Generate(nonceSize);

            key = SHA256Hash.Hash(key);

            using (var cipher = new AesGcm(key))
            {
                cipher.Encrypt(_nonce, data, cipherText, tag, null);
                return ByteConversion.Concat(tag, ByteConversion.Concat(_nonce, cipherText));
            }
        }

        // TODO: validate
        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            var tagSize = AesGcm.TagByteSizes.MinSize;
            var nonceSize = AesGcm.NonceByteSizes.MinSize;
            byte[] tag = ByteConversion.SubArray(data, 0, tagSize);
            byte[] nonce = ByteConversion.SubArray(data, tagSize, nonceSize);
            key = SHA256Hash.Hash(key);

            byte[] toDecrypt = ByteConversion.SubArray(data, tagSize + nonceSize, data.Length - tag.Length - nonce.Length);
            byte[] decryptedData = new byte[toDecrypt.Length];

            using (var cipher = new AesGcm(key))
            {
                cipher.Decrypt(nonce, toDecrypt, tag, decryptedData, null);

                return decryptedData;
            }
        }

        public void Dispose()
        {

        }
    }
}
