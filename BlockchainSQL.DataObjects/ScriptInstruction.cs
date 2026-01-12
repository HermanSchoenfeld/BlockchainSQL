// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.DataObjects
{
    public class ScriptInstruction {

        public ScriptInstruction() {
            RowState = 1;
        }

        public virtual long ID { get; set; }

        public virtual OpCode OpCode { get; set; }

        public virtual Script Script { get; set; }

        public virtual ushort Index { get; set; }

        public virtual bool Valid { get; set; }

        public virtual byte[] DataLE { get; set; }

        public virtual byte RowState { get; set; }

    }
}
