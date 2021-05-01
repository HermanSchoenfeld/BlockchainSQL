using System;
using System.Threading;

namespace BlockchainSQL.Processing
{
    public interface IBlockStream : IBizComponent, IDisposable {

        bool IsOpen { get; }

        void Open();

        void Close();

        bool HasMore { get; }
        
        int SeekToTip(BlockLocators blockLocators, CancellationToken cancellationToken);

        BlockStreamReadResult ReadNextBlocks(CancellationToken cancellationToken);

    }
}
