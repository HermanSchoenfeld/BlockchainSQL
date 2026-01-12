// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.IO;
using System.IO.MemoryMappedFiles;

namespace BlockchainSQL.Processing.Scanning {
	public class MemoryMappedBlockFileReader : BlockFileReader {
         private MemoryMappedFile _mmf;

         public MemoryMappedBlockFileReader(string blockFilePath) : base(blockFilePath) {             
         }

         protected override Stream OpenStream(string blockFilePath) {
             _mmf = MemoryMappedFile.CreateFromFile(blockFilePath, FileMode.Open, "BlockchainSQL", 0, MemoryMappedFileAccess.Read);
             return _mmf.CreateViewStream(0, 0, MemoryMappedFileAccess.Read);             
         }


         protected override void CloseStream() {
             base.CloseStream();
             _mmf.Dispose();
         }
     }
}
