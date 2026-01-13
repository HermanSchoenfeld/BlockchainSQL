// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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
