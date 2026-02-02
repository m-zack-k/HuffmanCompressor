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
