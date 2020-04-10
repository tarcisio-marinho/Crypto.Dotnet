namespace Crypto.ClassLib.Crypto.Interfaces
{
    public interface IConversion{
        string ByteToString(byte[] data);
        byte[] StringToByte(string data);
        byte[] Concat(byte[] firstArray, byte[] secondArray);
        byte[] SubArray(byte[] data, int start, int length);

    }
}
