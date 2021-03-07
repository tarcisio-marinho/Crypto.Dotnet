using Crypto.Dotnet.Core.Conversion;
using Crypto.Dotnet.Core.Symmetric;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Dotnet
{
    public static class Crypt
    {
        public static byte[] Encrypt(byte[] data, string key)
        {
            return GCM.Encrypt(data, ByteConversion.StringToByte(key));
        }

        public static byte[] Encrypt(string data, string key)
        {
            return GCM.Encrypt(ByteConversion.StringToByte(data), ByteConversion.StringToByte(key));
        }

        public static byte[] Encrypt(string data, byte[] key)
        {
            return GCM.Encrypt(ByteConversion.StringToByte(data), key);
        }

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            return GCM.Encrypt(data, key);
        }

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            return GCM.Decrypt(data, key);
        }

        public static byte[] Decrypt(byte[] data, string key)
        {
            return GCM.Decrypt(data, ByteConversion.StringToByte(key));
        }
    }
}
