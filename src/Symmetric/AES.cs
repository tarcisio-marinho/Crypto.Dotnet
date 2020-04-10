
using System;
using System.IO;
using System.Security.Cryptography;
using Crypto.ClassLib.Crypto.Interfaces;
using Crypto.ClassLib.Crypto.Ops;

namespace Crypto.ClassLib.Crypto.Symmetric
{
    public class AES: ICrypto
    {
        private byte[] key;
        private byte[] IV;
        public AES(byte[] key, byte[] IV)
        {
            this.key = key;
            this.IV = IV;
        }
        public byte[] Encrypt(byte[] data)
        {
            byte[] encryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = this.key;
                    AES.IV = this.IV;
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        public byte[] Decrypt(byte[] data)
        {
            byte[] decryptedBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = this.key;
                    AES.IV = this.IV;
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }

        public byte[] Encrypt(string data)
        {
            return this.Encrypt(new Conversion().StringToByte(data));
        }

        public byte[] Decrypt(string data)
        {
            return this.Decrypt(new Conversion().StringToByte(data));
        }
    }
}