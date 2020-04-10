
using System;
using System.Text;
using Crypto.ClassLib.Crypto.Interfaces;

namespace Crypto.ClassLib.Crypto.Ops
{
    public class Conversion : IConversion
    {
        public string ByteToString(byte[] data) => Encoding.UTF8.GetString(data);

        public byte[] StringToByte(string data) => Encoding.ASCII.GetBytes(data);
        public byte[] Concat(byte[] firstArray, byte[] secondArray)
        {
            byte[] output = new byte[firstArray.Length + secondArray.Length];
            System.Buffer.BlockCopy(firstArray, 0, output, 0, firstArray.Length);
            System.Buffer.BlockCopy(secondArray, 0, output, firstArray.Length, secondArray.Length);
            return output;
        }

        public byte[] SubArray(byte[] data, int start, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, start, result, 0, length);
            return result;
        }
    }
}
