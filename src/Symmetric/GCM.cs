
using System;
using System.Security.Cryptography;
using Crypto.ClassLib.Crypto.Ops;
using Crypto.ClassLib.Crypto.Interfaces;

namespace Crypto.ClassLib.Crypto.symmetric
{
    public class GCM : ICrypto
    {
        private byte[] _key;
        private byte[] _nonce;
        private int keySize = 16;
        private int nonceSize = 12;

        public GCM(byte[] key)
        {
            this._key = key;
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                this._key = sha256Hash.ComputeHash(this._key);  
            }  
            this._nonce = new byte[this.nonceSize];
            RandomNumberGenerator.Fill(this._nonce);
        }
        public GCM(string key)
        {
            this._key = new Conversion().StringToByte(key);
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                this._key = sha256Hash.ComputeHash(this._key);  
            }  
            this._nonce = new byte[this.nonceSize];
            RandomNumberGenerator.Fill(this._nonce);
        }

        public byte[] Encrypt(byte[] data)
        {
            byte[] tag = new byte[this.keySize];
            byte[] nonce = new byte[this.nonceSize];
            byte[] cipherText = new byte[data.Length];

            using (var cipher = new AesGcm(this._key))
            {
                cipher.Encrypt(nonce, data, cipherText, tag, null);
                var convert = new Conversion(); 
                return convert.Concat(tag, convert.Concat(nonce, cipherText));
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            var convert = new Conversion();
            byte[] tag = convert.SubArray(data, 0, this.keySize);
            byte[] nonce = convert.SubArray(data, this.keySize, nonceSize);

            byte[] toDecrypt = convert.SubArray(data, this.keySize + this.nonceSize, data.Length - tag.Length - nonce.Length);
            byte[] decryptedData = new byte[toDecrypt.Length];

            using (var cipher = new AesGcm(this._key))
            {
                cipher.Decrypt(nonce, toDecrypt, tag, decryptedData, null);

                return decryptedData;
            }
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