using System;
using System.Collections.Generic;
using System.Linq;

namespace HuffmanCod
{
    class Unnamed
    {
        public Dictionary<char, int> CreateDic(string data)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();
            IEnumerable<IGrouping<char, char>> enumerable = data.Select(x => x).GroupBy(x => x).ToList().AsEnumerable();

            foreach (var item in enumerable)
            {
                dic.Add(item.Key, item.Count());
            }
            return dic;
        }

        public void CreateTree(ref List<HuffmanNodeBase> nodes)
        {
            int len = nodes.Count;

            for (int i = 0; i < len - 1; i++)
            {
                HuffmanNodeBase node1 = nodes[0];
                nodes.Remove(nodes[0]);
                HuffmanNodeBase node2 = nodes[0];
                nodes.Remove(nodes[0]);
                nodes.Add(new HuffmanNodeBase(node1, node2));
                nodes = nodes.Select(x => x).OrderBy(x => x.Count).ToList();
            }
        }

        public void Encrypt(HuffmanNodeBase node, string enc)
        {
            if (node.LeftNode == null && node.RightNode == null)
            {
                node.Encrypted = enc;
                return;
            }
            Encrypt(node.LeftNode, enc + "0");
            Encrypt(node.RightNode, enc + "1");
        }

        public void PrintResult(HuffmanNodeBase node)
        {
            if (node.LeftNode == null && node.RightNode == null)
            {
                Console.WriteLine(node.Character + " -> " + node.Encrypted);
                return;
            }
            PrintResult(node.LeftNode);
            PrintResult(node.RightNode);
        }
    }
}
