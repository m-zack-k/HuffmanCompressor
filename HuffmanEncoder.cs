

namespace HuffmanTree
{
    public class HuffmanCode
    {
        public bool[] Bits { get; set; }
        public int Length { get; set; }
    }
    public class HuffmanEncoder
    {
        private const int BufferSize = 65536;

        public void Encode(string inputFile, Node root, Stream output)
        {
            HuffmanCode[] assignedCodes = new HuffmanCode[256];
            AssignCodes(root, new List<bool>(), assignedCodes);

            TreeSerializer serializer = new TreeSerializer();
            serializer.WriteTree(root, output);

            BitWriter bitWriter = new BitWriter(output);

            using (FileStream fs = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[BufferSize];
                int bytesRead;

                while ((bytesRead = fs.Read(buffer, 0, BufferSize)) > 0)
                {
                    for (int i = 0; i < bytesRead; ++i)
                    {
                        byte symbol = buffer[i];
                        HuffmanCode code = assignedCodes[symbol];

                        for (int b = 0; b < code.Length; ++b)
                        {
                            bitWriter.WriteBit(code.Bits[b]);
                        }
                    }
                }
            }

            bitWriter.Flusher();
        }

        private void AssignCodes(Node node, List<bool> currentPath, HuffmanCode[] assignedCodes)
        {
            if (node.IsLeaf)
            {
                var leaf = (LeafNode)node;
                assignedCodes[leaf.Symbol] = new HuffmanCode
                {
                    Bits = currentPath.ToArray(),
                    Length = currentPath.Count
                };
            }
            else
            {
                var inner = (InnerNode)node;
                currentPath.Add(false);
                AssignCodes(inner.Left, currentPath, assignedCodes);
                currentPath.RemoveAt(currentPath.Count - 1);

                currentPath.Add(true);
                AssignCodes(inner.Right, currentPath, assignedCodes);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }


}