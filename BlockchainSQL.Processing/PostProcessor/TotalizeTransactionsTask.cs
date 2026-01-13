// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.Processing {


	public class TotalizeTransactionsTask : BizComponent, IPostProcessingTask {
        public void ExecuteAll() {
            DAC.TotalizeTransactions();
        }

        public void ExecutePartial(PersistResult newPersistSet) {
            DAC.TotalizeTransactions(newPersistSet.Transaction.From, newPersistSet.Transaction.To);
        }

        public int Priority { get; set; }
    }
}
