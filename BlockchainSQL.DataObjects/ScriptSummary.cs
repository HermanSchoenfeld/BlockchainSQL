// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.DataObjects
{
    public class ScriptSummary {
        public string Type { get; set; }

        public string Class { get; set; }

        public int Size { get; set; }

        public ScriptInstructionSummary[] Instructions { get; set; }
    }
}
