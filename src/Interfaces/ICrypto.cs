
namespace Crypto.ClassLib.Crypto.Interfaces
{
    public interface ICrypto
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
        byte[] Encrypt(string data);
        byte[] Decrypt(string data);
    }
}