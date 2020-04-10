using System;
using System.Security.Cryptography;
using System.Text;
using Crypto.ClassLib.Crypto.Interfaces;
using Crypto.ClassLib.Crypto.Ops;

namespace Crypto.ClassLib.Crypto.Hash
{
    public class HashSHA256 : IHashing
    {
        public byte[] Hash(string data)
        {
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                IConversion convert =  new Conversion();
                return sha256Hash.ComputeHash(convert.StringToByte(data));  
            }
        }

        public byte[] Hash(byte[] data)
        {
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                IConversion convert =  new Conversion();
                return sha256Hash.ComputeHash(data);  
            }
        }

        public string HexHash(string data)
        {
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                byte[] bytes = sha256Hash.ComputeHash(new Conversion().StringToByte(data));  

                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }
        }

        public string HexHash(byte[] data)
        {
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                byte[] bytes = sha256Hash.ComputeHash(data);  
  
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }
        }
    }
}