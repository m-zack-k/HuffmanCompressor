using System.Collections.Generic;

namespace HuffmanTree
{
    //Implements the specific priority logic
    public class NodeComparer : IComparer<Node>
    {
        /// <summary>
        /// Compares two nodes to determine their order.
        ///Returns -1 if 'a' should come before 'b' (higher priority).
        /// Returns 1 if 'a' should come after 'b' (lower priority).
        /// </summary>
        public int Compare(Node a, Node b)
        {
            //ower weight has higher priority.
            int weightCompareTo = a.Weight.CompareTo(b.Weight);
            if (weightCompareTo != 0)
            {
                return weightCompareTo;
            }
            //If weights are equal, Leaf nodes have priority over Inner nodes.
            if (a.IsLeaf && !b.IsLeaf)
            {
                return -1;
            }
            if (!a.IsLeaf && b.IsLeaf)
            {
                return 1;
            }
            //The one with the lower symbol value is priotized
            if (a.IsLeaf && b.IsLeaf)
            {
                var aLeaf = (LeafNode)a;
                var bLeaf = (LeafNode)b;
                return aLeaf.Symbol.CompareTo(bLeaf.Symbol);
            }

            // The one created earlier in the algorithm is priotized
            if (!a.IsLeaf && !b.IsLeaf)
            {
                var aInner = (InnerNode)a;
                var bInner = (InnerNode)b;
                return aInner.CreationOrder.CompareTo(bInner.CreationOrder);
            }

            return 0;


        }
    }

}