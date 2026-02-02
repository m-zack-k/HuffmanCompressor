
namespace HuffmanTree
{
    /// Implements the core Huffman algorithm
    public class HuffmanBuilder
    {
        /// Builds the Huffman tree from an array of byte frequencies.
        public Node Build(long[] freqCounts)
        {
            NodePriorityQueue forest = new NodePriorityQueue();

            forest.InitializeFromFrequencies(freqCounts);

            if (forest.IsEmpty) return null;

            int creationCounter = 0;
            while (forest.Count > 1)
            {
                // Remove the two nodes with the lowest priority
                Node left = forest.ExtractMin();
                Node right = forest.ExtractMin();

                // Create a new Inner Node merging their weights.
                InnerNode parent = new InnerNode(left, right, creationCounter++);

                forest.Add(parent);
            }

            return forest.ExtractMin();
        }
    }
}