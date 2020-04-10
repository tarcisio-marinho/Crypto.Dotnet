namespace Crypto.ClassLib.Crypto.Interfaces
{
    public interface IHashing
    {
        byte[] Hash(string data);
        byte[] Hash(byte[] data);
        string HexHash(string data);
        string HexHash(byte[] data);
    }
}
