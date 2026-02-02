/*
Reference(especially BinaryWriter) :
https://stackoverflow.com/questions/4614318/whats-the-difference-between-a-streamwriter-and-a-binarywriter

*/

namespace HuffmanTree
{
    public class TreeSerializer
    {
        private static readonly byte[] Header = { 0x7B, 0x68, 0x75, 0x7C, 0x6D, 0x7D, 0x66, 0x66 };

        public void WriteTree(Node root, Stream output)
        {
            output.Write(Header, 0, Header.Length);

            using (BinaryWriter writer = new BinaryWriter(output, System.Text.Encoding.Default, true))
            {
                RecursiveNodeWriter(root, writer);
                writer.Write((ulong)0);
            }
        }

        private void RecursiveNodeWriter(Node node, BinaryWriter writer)
        {
            ulong value = 0;
            ulong mask = ((ulong)node.Weight & 0x007FFFFFFFFFFFFF) << 1;

            if (node.IsLeaf)
            {
                var leaf = (LeafNode)node;
                value = 1 | mask | ((ulong)leaf.Symbol << 56);
            }
            else
            {
                value = mask;
            }

            writer.Write(value);

            if (!node.IsLeaf)
            {
                var inner = (InnerNode)node;
                RecursiveNodeWriter(inner.Left, writer);
                RecursiveNodeWriter(inner.Right, writer);
            }
        }
    }
}