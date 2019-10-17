using System;

namespace _001_PatternBasedMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceData = null;

            do
            {
                Console.Write("Please input data: ");
                sourceData = Console.ReadLine();
            } while (sourceData == null);

            Transform ob = new Transform();

            string encryptedString = ob.GetEncryptedString(sourceData);
            string decryptedString = ob.GetDecryptedString(encryptedString);

            Console.WriteLine($"Encrypted: {encryptedString} \nDecrypted: {decryptedString}");

            Console.ReadKey();
        }
    }
}