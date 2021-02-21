using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Dotnet.Core.Conversion
{
    internal static class ByteConversion
    {
        public static string ByteToString(byte[] data) => Encoding.UTF8.GetString(data);

        public static byte[] StringToByte(string data) => Encoding.ASCII.GetBytes(data);

        public static byte[] Concat(byte[] firstArray, byte[] secondArray)
        {
            byte[] output = new byte[firstArray.Length + secondArray.Length];
            System.Buffer.BlockCopy(firstArray, 0, output, 0, firstArray.Length);
            System.Buffer.BlockCopy(secondArray, 0, output, firstArray.Length, secondArray.Length);
            return output;
        }

        public static byte[] SubArray(byte[] data, int start, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, start, result, 0, length);
            return result;
        }
    }
}
