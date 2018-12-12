using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using NBitcoin.BouncyCastle.Math;
using Sphere10.Framework;

namespace BlockchainSQL.Processing {
    public interface IBlockStream : IBizComponent, IDisposable {

        bool IsOpen { get; }

        void Open();

        void Close();

        bool HasMore { get; }
        
        int SeekToTip(BlockLocators blockLocators, CancellationToken cancellationToken);

        BlockStreamReadResult ReadNextBlocks(CancellationToken cancellationToken);

    }
}
