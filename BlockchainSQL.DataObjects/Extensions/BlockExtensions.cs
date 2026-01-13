// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Linq;

namespace BlockchainSQL.DataObjects
{
    public static class BlockExtensions {

        public static bool IsGenesisBlock(this Block block) {
            return block.PreviousBlockHash != null &&  block.PreviousBlockHash.Length == 32 && block.PreviousBlockHash.All(c => c == 0);
        }
    }
}
