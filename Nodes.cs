using System;

namespace HuffmanTree
{
    //base class for all nodes in the Huffman Tree.
    public abstract class Node
    {
        //The frequency count of the character
        public long Weight { get; protected set; }
        //to distinguish node types
        public abstract bool IsLeaf { get; }
        //Recursive method to print the tree in Prefix notation
        public abstract void Print();
    }

    //a leaf node in the Huffman Tree
    public class LeafNode : Node
    {
        public int Symbol { get; } // byte value (0-255)

        public LeafNode(int symbol, long weight)
        {
            Symbol = symbol;
            Weight = weight;
        }

        public override bool IsLeaf => true;
        //Prints the leaf in the format: *SYMBOL:WEIGHT
        public override void Print()
        {
            Console.Write($"*{Symbol}:{Weight}");
        }
    }

    //Inner node in the Huffman tree 
    public class InnerNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }
        // this is for "created sooner has priority"
        public int CreationOrder { get; }

        public InnerNode(Node left, Node right, int creationOrder)
        {
            Left = left;
            Right = right;
            Weight = left.Weight + right.Weight;
            CreationOrder = creationOrder;
        }

        public override bool IsLeaf => false;

        //Prints the inner node recursively in Prefix notation: WEIGHT LEFT RIGHT
        public override void Print()
        {
            Console.Write($"{Weight} ");
            Left.Print();
            Console.Write(" ");
            Right.Print();
        }
    }


}