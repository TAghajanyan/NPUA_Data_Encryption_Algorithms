using System;

namespace _007_RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA a = new RSA();

            Console.WriteLine("Type an integer message < 100");
            int message = int.Parse(Console.ReadLine());
            int chiphertext = a.ComputeChipherText(message);
            Console.WriteLine("Encoded -> " + chiphertext);
            int plaintext = a.ComputePlainText(chiphertext);
            Console.WriteLine("Decoded -> " + plaintext);
        }
    }
}



