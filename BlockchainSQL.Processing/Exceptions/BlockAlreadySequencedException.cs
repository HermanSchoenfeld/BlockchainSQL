// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.Processing {
	public class BlockAlreadySequencedException : BlockchainSQLException {
        public BlockAlreadySequencedException(byte[] blockHash) 
            : base("Block '{0}' has already been sequenced", BitcoinProtocolHelper.BytesToString(blockHash)) {            
        }
    }
}
