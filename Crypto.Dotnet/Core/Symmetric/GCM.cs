using Crypto.Dotnet.Core.Conversion;
using Crypto.Dotnet.Core.Hash;
using Crypto.Dotnet.Core.Random;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Crypto.Dotnet.Core.Symmetric
{
    internal class GCM
    {
        private byte[] _key;
        private byte[] _nonce;
        private int keySize = 16;
        private int nonceSize = 12;

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

        public byte[] Encrypt(byte[] data)
        {
            byte[] tag = new byte[keySize];
            byte[] nonce = new byte[nonceSize];
            byte[] cipherText = new byte[data.Length];

            using (var cipher = new AesGcm(_key))
            {
                cipher.Encrypt(nonce, data, cipherText, tag, null);
                return ByteConversion.Concat(tag, ByteConversion.Concat(nonce, cipherText));
            }
        }

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

        public byte[] Encrypt(string data)
        {
            return Encrypt(ByteConversion.StringToByte(data));
        }

        public byte[] Decrypt(string data)
        {
            return Decrypt(ByteConversion.StringToByte(data));
        }
    }
}
