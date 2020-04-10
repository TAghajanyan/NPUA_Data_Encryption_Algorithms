using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TwoFish
{
    class Program
    {
        public static string inputFileName = @"D:\TwoFish\input.txt";
        public static string outputFileName = @"D:\TwoFish\output.txt";
        public static string keyFileName = @"D:\TwoFish\key.txt";

        private static void Main(string[] args)
        {
            var dir = TwofishMode.Decrypt;

            for (var i = 0; i < args.Length; i++)
                if (args[i] == "--encrypt") dir = TwofishMode.Encrypt;
                else if (args[i] == "--decrypt") dir = TwofishMode.Decrypt;
                else if (args[i] == "--input") inputFileName = args[++i];
                else if (args[i] == "--output") outputFileName = args[++i];

            switch (dir)
            {
                case TwofishMode.Encrypt:
                    {
                        Encrypt();
                        break;
                    }
                case TwofishMode.Decrypt:
                    {
                        Decrypt();
                        break;
                    }
                default:
                    break;
            }
        }

        private static void Encrypt()
        {
            //var len = TwofishAlgo.testKey.Length;
            var encryptingText = File.ReadAllText(inputFileName);
            var encryptingTextBin = StringMethods.ConvertTextToBinString(encryptingText);
            List<string> encodedBlocks = new List<string>();
            List<byte> intViewOfBytes = new List<byte>();
            string result = "";
            //byte[] bytesResult = new byte[];
            if (encryptingTextBin.Length % 128 != 0)
            {
                for (int i = encryptingTextBin.Length % 128; i < 128; i++)
                    encryptingTextBin += "0";
            }
            var blocks128bit = StringMethods.StringToList(encryptingTextBin, 128);
            foreach (string block in blocks128bit)
            {
                encodedBlocks.Add(TwofishAlgo.Cyphering(block, TwofishAlgo.testKey));
            }
            var binaryResultString = StringMethods.ListToString(encodedBlocks);
            var bytesResultHashset = StringMethods.StringToList(binaryResultString, 8);
            foreach (string resultByte in bytesResultHashset)
            {
                intViewOfBytes.Add(Convert.ToByte(Convert.ToInt32(resultByte, 2)));
            }

            var arrayOfBytes = intViewOfBytes.ToArray();
            Encoding encoding = Encoding.GetEncoding(0);
            for (int i = 0; i < arrayOfBytes.Count(); i++)
            {
                byte[] oneByte = new byte[1];
                oneByte[0] = arrayOfBytes[i];
                result += encoding.GetString(oneByte);
            }
            File.WriteAllText(outputFileName, result, Encoding.UTF8);
            File.WriteAllText(keyFileName, TwofishAlgo.testKey);
        }

        private static void Decrypt()
        {
            var closedMessage = File.ReadAllText(outputFileName);
            var key = File.ReadAllText(keyFileName);
            var closedMessageBin = StringMethods.ConvertTextToBinString(closedMessage);
            List<string> decodedBlocks = new List<string>();
            List<byte> intViewOfBytes = new List<byte>();
            string result = "";

            if (closedMessageBin.Length % 128 != 0)
            {
                for (int i = closedMessageBin.Length % 128; i < 128; i++)
                    closedMessageBin += "0";
            }
            var blocks128bit = StringMethods.StringToList(closedMessageBin, 128);
            foreach (string block in blocks128bit)
            {
                decodedBlocks.Add(TwofishAlgo.Decyphering(block, key));
            }
            var binaryResultString = StringMethods.ListToString(decodedBlocks);
            var bytesResultHashset = StringMethods.StringToList(binaryResultString, 8);
            foreach (string resultByte in bytesResultHashset)
            {
                intViewOfBytes.Add(Convert.ToByte(Convert.ToInt32(resultByte, 2)));
            }

            var arrayOfBytes = intViewOfBytes.ToArray();
            Encoding encoding = Encoding.GetEncoding(0);
            for (int i = 0; i < arrayOfBytes.Count(); i++)
            {
                byte[] oneByte = new byte[1];
                oneByte[0] = arrayOfBytes[i];
                result += encoding.GetString(oneByte);
            }
            File.WriteAllText(outputFileName, result, Encoding.UTF8);
        }
    }
}


