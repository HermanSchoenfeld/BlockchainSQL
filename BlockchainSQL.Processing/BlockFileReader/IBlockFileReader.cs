using System;
using System.IO;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Processing {
	public interface IBlockFileReader : IDisposable {
        bool SkipTransactions { get; set; }
        bool ReadMagicID { get; set; }
        bool ReadSize { get; set; }
        string Path { get; }
        Block ReadNextBlock();
        BlockFileReaderPeekResult PeekNextBlock();
        void Seek(int offset, SeekOrigin origin);
        bool HasMoreBlocks { get; }
        long TotalBytesProcessed { get; }
        long TotalBlocksRead { get; }
    }
}
