using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Crypto.Dotnet.Core.Random
{
    internal static class RandomBytes
    {
        public static byte[] Generate(int bytesCount)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[bytesCount];
                rng.GetBytes(randomBytes);
                return randomBytes;
            }
        }
    }
}
