# Huffman File Compressor

A C# implementation of the Huffman coding algorithm capable of compressing files of arbitrary size (tested up to 1TB) with O(1) memory usage.

## 🚀 Features

- **Efficient Compression:** Implements frequency analysis and canonical Huffman tree construction.
- **Low Memory Footprint:** Designed to handle massive files (1TiB+) using strictly O(1) memory (streaming data rather than loading into RAM).
- **Binary Manipulation:** Handles bit-level operations for compact file output (Little Endian formatting).
- **Custom Data Structures:** Includes custom implementations of Priority Queue and Binary Tree nodes.

## 🛠 Technical Details

### Input/Output Format
- **Header:** Writes a specific 8-byte header (`0x7B 0x68 ...`) to identify compressed files.
- **Tree Serialization:** The Huffman tree is serialized in prefix notation before the compressed data.
- **Bitwise Encoding:** Compresses data into a continuous bit stream, padding with zeros to align to the nearest byte.

### Algorithm Strategy
1. **Frequency Analysis:** Reads the file to count byte occurrences.
2. **Tree Construction:** Uses a Priority Queue to build the Huffman Tree based on weight and specific tie-breaking rules (Leaf < Inner, ASCII value priority, etc.).
3. **Encoding:** Generates a `.huff` file containing the header, the serialized tree, and the encoded bitstream.

## 💻 How to Run

1. Clone the repository:
   ```bash
   git clone [https://github.com/m-zack-k/HuffmanCompressor.git](https://github.com/m-zack-k/HuffmanCompressor.git)
2. Navigate to the directory
   ```bash
   cd HuffmanCompressor
3. Run the compressor on a file:
   ```bash
   dotnet run -- path/to/your/input.txt
   
## 📂 Project Structure & Implementation Details

### Core Logic
* **`Program.cs`**: The entry point of the application. It handles command-line argument validation and coordinates the compression workflow. It implements robust error handling to catch file access issues or invalid arguments, outputting standardized error messages ("Argument Error", "File Error") as per requirements.
* **`HuffmanBuilder.cs`**: Implements the tree-construction algorithm. It takes raw frequency data and iteratively merges the two lowest-priority nodes until a single root node remains. It ensures the resulting tree defines optimal prefix codes for compression.
* **`HuffmanEncoder.cs`**: Orchestrates the encoding process. It first generates the bit-codes for every byte by traversing the tree. Then, it performs a second pass over the input file to stream encoded bits to the output, ensuring the file is processed in segments to adhere to memory constraints.

### Data Structures & Analysis
* **`NodePriorityQueue.cs`**: A custom priority queue implementation used to manage Huffman tree nodes. It manages the "forest" of trees during construction, ensuring that nodes are extracted in the exact order defined by the assignment's complex priority rules.
* **`NodeComparer.cs`**: Defines the strict ordering logic for the priority queue. It implements the specific tie-breaking rules required for deterministic tree building: comparing by weight first, then prioritizing leaf nodes over inner nodes, then by symbol value (for leaves), and finally by creation order (for inner nodes).
* **`FrequencyAnalyzer.cs`**: Responsible for the initial pass over the data. It reads the input file in small 4KB chunks (preventing high RAM usage) to calculate the frequency of every byte (0-255), which serves as the statistical basis for the Huffman tree.
* **`Nodes.cs`**: Defines the polymorphic node structure (`Node`, `LeafNode`, `InnerNode`). These classes store weight data and references to children, and include logic for identifying node types during the serialization process.

### Low-Level I/O (Binary Manipulation)
* **`BitWriter.cs`**: A critical component for memory efficiency. Instead of holding the entire compressed string in memory, this class accumulates bits into a single byte. Once a byte is full, it pushes it into a 64KB buffer, which is flushed to the disk only when necessary. This allows the program to process files of any size (TB+) with constant O(1) memory usage.
* **`TreeSerializer.cs`**: Handles the binary formatting of the output file header and the Huffman tree itself. It writes the required 8-byte magic signature and serializes the tree structure using a 64-bit Little Endian format, packing node metadata (weight, symbol, isLeaf flag) into 8-byte blocks as specified by the assignment.


