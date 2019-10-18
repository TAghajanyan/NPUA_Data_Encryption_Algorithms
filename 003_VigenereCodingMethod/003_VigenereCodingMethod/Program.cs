using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _003_VigenereCodingMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Transform transform = new Transform("LEMON");
            string enc = transform.Encrypting("ATTACKATDAWN");
            string dec = transform.Decrypting(enc);
            Console.WriteLine("Encrypted text: " + enc);
            Console.WriteLine("Decrypted text: " + dec);
        }
    }
}
