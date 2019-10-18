using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _004_RLEMethod
{

    class Program
    {
        static List<string> GetEncriptedText(string sourceData)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < sourceData.Length; i++)
            { 
                var count = GetCount(sourceData[i], ref sourceData, i);
                list.Add(sourceData[i] + count.ToString());
            }

            return list;
        }

        private static int GetCount(char v, ref string sourceData, int index)
        {
            int count = 0;
            for (int i = index; i < sourceData.Length; i++)
            {
                if (sourceData[i] == v)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            sourceData = sourceData.Remove(index + 1, count-1);

            return count;
        }

        static string GetDecriptedText(List<string> list)
        {
            char item;
            int count;
            string result = "";

            for (int i = 0; i < list.Count; i++)
            {
                item = list[i][0];
                count = int.Parse(list[i].Remove(0, 1));
                for (int j = 0; j < count; j++)
                {
                    result += item;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            string sourceData = "aa1111bbaa";
            var enc = GetEncriptedText(sourceData);
            var dec = GetDecriptedText(enc);

            foreach (var item in enc)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            Console.WriteLine(dec);

            Console.ReadKey();
        }
    }
}
