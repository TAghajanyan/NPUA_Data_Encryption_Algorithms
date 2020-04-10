using System;
using System.Collections.Generic;
using System.Text;

namespace TwoFish
{
    class StringMethods
    {
        public static List<string> StringToList(string dataString, int blockLength)
        {
            List<string> blocks = new List<string>();
            for (int i = 0; i < dataString.Length; i += blockLength)
            {
                var dataBlock = dataString.Substring(i, blockLength);
                blocks.Add(dataBlock);
            }
            return blocks;
        }

        public static string ListToString(List<string> stringMass)
        {
            string gluedNumber = "";
            foreach (string str in stringMass)
            {
                gluedNumber += str;
            }
            return gluedNumber;
        }

        public static string MyConvertToString(int decNum, int toBase, int neededLength)
        {
            var convertedString = Convert.ToString(decNum, toBase);
            while (convertedString.Length != neededLength)
                convertedString = convertedString.Insert(0, "0");
            return convertedString;
        }

        public static string ConvertTextToBinString(string textMessage)
        {
            var bytesData = Encoding.Default.GetBytes(textMessage);
            string openMessageBin = "";
            for (int i = 0; i < bytesData.Length; i++)
            {
                openMessageBin += (StringMethods.MyConvertToString(Convert.ToInt32(bytesData[i]), 2, 8));
            }
            return openMessageBin;
        }

        public static string DataBlockGen(int value)
        {
            string data = "";
            Random rnd = new Random();
            while (data.Length < value)
            {
                int bit = rnd.Next(0, 2);
                data += Convert.ToString(bit, 2);
            }
            return data;
        }

    }
}


