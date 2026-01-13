// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;

namespace BlockchainSQL.DataObjects {

    public class Branch {

        public Branch() {
            RowState = 1;
        }

        public virtual int ID { get; set; }
        public virtual int ForkHeight { get; set; }
        public virtual uint TimeStampUnix { get; set; }
        public virtual DateTime TimeStampUtc { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
        public virtual Branch ParentBranch { get; set; }
        public virtual byte RowState { get; set; }

    }
}
