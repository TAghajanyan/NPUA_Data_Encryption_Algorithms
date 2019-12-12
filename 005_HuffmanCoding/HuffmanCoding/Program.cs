using System;
using System.Collections.Generic;
using System.Linq;

namespace HuffmanCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "abbnnccc";
            Unnamed unnamed = new Unnamed();
            var dicEnumerable = unnamed.CreateDic(text).OrderBy(t => t.Value);
            Console.WriteLine("Text: " + text);

            List<HuffmanNodeBase> nodes = new List<HuffmanNodeBase>();

            foreach (var item in dicEnumerable)
            {
                nodes.Add(new HuffmanNodeBase(item.Key, item.Value));
                Console.WriteLine(item.Key + " -> " + item.Value);
            }

            unnamed.CreateTree(ref nodes);

            unnamed.Encrypt(nodes[0], "");

            Console.WriteLine(new string('-', 25) + Environment.NewLine + "Result:");
            unnamed.PrintResult(nodes[0]);
        }
    }
}
