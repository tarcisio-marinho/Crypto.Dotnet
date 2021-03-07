using Crypto.Dotnet;
using System;
using System.Text;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var pass = "minha senha";
            var ret = Crypt.Encrypt("meus dados", pass);
            var origin = Encoding.UTF8.GetString(Crypt.Decrypt(ret, pass));

            Console.WriteLine(origin);
        }
    }
}
