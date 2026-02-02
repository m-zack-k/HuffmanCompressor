
namespace HuffmanTree
{
    public class BitWriter
    {
        private readonly Stream _output;
        private byte _currentByte;
        private int _bitIdx;

        private readonly byte[] _buffer;
        private int _bufferIdx;
        private const int BufferSize = 65536;

        public BitWriter(Stream output)
        {
            _output = output;
            _currentByte = 0;
            _bitIdx = 0;
            _buffer = new byte[BufferSize];
            _bufferIdx = 0;
        }

        public void WriteBit(bool bit)
        {
            if (bit)
            {
                _currentByte |= (byte)(1 << _bitIdx);
            }

            _bitIdx++;

            if (_bitIdx == 8)
            {
                _buffer[_bufferIdx++] = _currentByte;

                _currentByte = 0;
                _bitIdx = 0;

                if (_bufferIdx == BufferSize)
                {
                    _output.Write(_buffer, 0, BufferSize);
                    _bufferIdx = 0;
                }
            }
        }

        public void Flusher()
        {
            if (_bitIdx > 0)
            {
                _buffer[_bufferIdx++] = _currentByte;
                _currentByte = 0;
                _bitIdx = 0;
            }

            if (_bufferIdx > 0)
            {
                _output.Write(_buffer, 0, _bufferIdx);
                _bufferIdx = 0;
            }
        }
    }
}