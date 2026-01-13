// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;

namespace BlockchainSQL.Processing.Scanning {
	public class BlockFileScope : IDisposable {
         private readonly IBlockFileReader _reader;
         public BlockFileScope(IBlockFileReader reader, string blockFilePath) {
            _reader = reader;
            //if (Reader.IsOpen)
            //    throw new ApplicationException("BlockFileReader is already open");
            //Reader.Open(blockFilePath);
        }


         public void Dispose() {
             _reader.Dispose();
         }
     }
}
