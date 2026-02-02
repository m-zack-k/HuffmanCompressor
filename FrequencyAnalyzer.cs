using System.IO;

namespace HuffmanTree
{
    /// Responsible for analyzing the input file to determine the frequency of each byte.
    public class FrequencyAnalyzer
    {
        // buffer to follow O(1) memory
        private const int BufferSize = 4096;

        public long[] CountFrequencies(string file)
        {
            //Stores the occurrence
            long[] freqCounts = new long[256];

            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                //If file is empty, return the empty frequency array
                if (fs.Length == 0) return freqCounts;

                byte[] buffer = new byte[BufferSize];
                int bytesRead;

                // Read the file
                while ((bytesRead = fs.Read(buffer, 0, BufferSize)) > 0)
                {
                    // Process only the bytes actually read into the buffer
                    for (int i = 0; i < bytesRead; i++)
                    {
                        freqCounts[buffer[i]]++;
                    }
                }
            }

            return freqCounts;
        }
    }
}