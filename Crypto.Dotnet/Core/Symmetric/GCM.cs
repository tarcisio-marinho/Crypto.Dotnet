using Crypto.Dotnet.Core.Conversion;
using Crypto.Dotnet.Core.Hash;
using Crypto.Dotnet.Core.Random;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Crypto.Dotnet.Core.Symmetric
{
    internal static class GCM
    {
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
    }
}
