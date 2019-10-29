namespace HuffmanCod
{
    class HuffmanNodeBase
    {
        public string Character { get; private set; }
        public int Count { get; private set; }
        public string Encrypted { get; set; }
        public HuffmanNodeBase LeftNode { get; private set; }
        public HuffmanNodeBase RightNode { get; private set; }

        /// <summary>
        /// For adding items
        /// </summary>
        public HuffmanNodeBase(char symbol, int count)
        {
            Character = symbol.ToString();
            Count = count;
        }

        /// <summary>
        /// For adding nodes
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        public HuffmanNodeBase(HuffmanNodeBase node1, HuffmanNodeBase node2)
        {
            if (node1.Count >= node2.Count)
            {
                Character = node1.Character + node2.Character;
                Count = node1.Count + node2.Count;
                RightNode = node1;
                LeftNode = node2;
                Encrypted = string.Empty;
            }
            else
            {
                Character = node2.Character + node1.Character;
                Count = node1.Count + node2.Count;
                LeftNode = node1;
                RightNode = node2;
                Encrypted = string.Empty;
            }
        }
    }
}
