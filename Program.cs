namespace HuffmanTree
{
    //the main entry point 
    class Program
    {
        static void Main(string[] args)
        {
            //Argument Validation
            if (args.Length != 1)
            {
                Console.WriteLine("Argument Error");
                return;
            }

            string inputFile = args[0];

            try
            {
                //Use FrequencyAnalyzer to count how many times each byte occurs.
                FrequencyAnalyzer analyzer = new FrequencyAnalyzer();
                long[] freqCounts = analyzer.CountFrequencies(inputFile);
                // Use HuffmanBuilder to build the Huffman tree structure.
                HuffmanBuilder builder = new HuffmanBuilder();
                Node root = builder.Build(freqCounts);

                //If the file is not empty, print the tree in Prefix notation.
                if (root != null)
                {
                    string outputFile = inputFile + ".huff";
                    using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                    using (Stream output = new BufferedStream(fs, 65536))
                    {
                        HuffmanEncoder encoder = new HuffmanEncoder();
                        encoder.Encode(inputFile, root, output);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    Console.WriteLine("Argument Error");
                }
                else
                {
                    Console.WriteLine("File Error");
                }
            }
        }
    }
}