using System.Collections.Generic;
using System.Linq;

namespace HuffmanTree
{
    /// Custom Priority Queue for nodes
    public class NodePriorityQueue
    {
        private readonly List<Node> _nodes;
        private readonly NodeComparer _comparer;

        public NodePriorityQueue()
        {
            _nodes = new List<Node>();
            _comparer = new NodeComparer();
        }

        public int Count => _nodes.Count;
        public bool IsEmpty => _nodes.Count == 0;

        /// Populates the initial forest with Leaf Nodes based on frequencies.
        public void InitializeFromFrequencies(long[] frequencies)
        {
            for (int i = 0; i < frequencies.Length; i++)
            {
                if (frequencies[i] > 0)
                {
                    _nodes.Add(new LeafNode(i, frequencies[i]));
                }
            }
        }

        ///Adds a node back into a forest 
        public void Add(Node node)
        {
            _nodes.Add(node);
        }

        //Finds and removes the node with the highest priority
        public Node ExtractMin()
        {
            _nodes.Sort(_comparer);

            Node min = _nodes[0];
            _nodes.RemoveAt(0);
            return min;
        }




    }
}