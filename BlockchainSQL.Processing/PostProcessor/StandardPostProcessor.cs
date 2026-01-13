// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {

	public class PostProcessor : BizComponent, IPostProcessor {

        public PostProcessor() {
            Tasks = new IPostProcessingTask[] {
                new ConnectOutpointsTask() { Priority = 1 }, 
                new TotalizeTransactionsTask() { Priority = 2 }, 
                new TotalizeBlocksTask() { Priority = 3 }
            };
        }
        public void PostProcessAll() {
            using (var scope = DAC.BeginScope()) {
                scope.BeginTransaction();
                Tasks.ForEach(t => t.ExecuteAll());
                scope.Commit();
            }
        }

        public void PostProcessPartial(PersistResult newPersistSet) {
            using (var scope = DAC.BeginScope()) {
                scope.BeginTransaction();
                Tasks.ForEach(t => t.ExecutePartial(newPersistSet));
                scope.Commit();
            }
        }

        public IPostProcessingTask[] Tasks { get; private set; }
    }
}
