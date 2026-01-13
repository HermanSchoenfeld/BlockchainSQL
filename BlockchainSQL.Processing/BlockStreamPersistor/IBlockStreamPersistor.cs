// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Threading;

namespace BlockchainSQL.Processing {
	public interface IBlockStreamPersistor : IBizComponent {
        PersistResult Persist(IEnumerable<WipBlock> processedBlocks, bool saveScriptData, bool enforceFK, CancellationToken cancellationToken);

    }
}
