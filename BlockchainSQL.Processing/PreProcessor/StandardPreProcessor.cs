// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
	public class StandardPreProcessor : BizComponent, IPreProcessor {
        
        public StandardPreProcessor(bool optimizeForBulkLoading, bool expandScripts) {
            // TODO: load tasks from config
            var seqTasks = new[] {new ActivateMainChainTask(new OptimizedBlockOrganizer(optimizeForBulkLoading))}.AsEnumerable<IPreProcessingTask>();
            var paraTasks = new IPreProcessingTask [] {
                new CalculateDifficultyTask(BizLogicFactory.NewDifficultyCalculator()),
                new CalculateRewardTask()
            }.AsEnumerable();

            if (expandScripts)
                paraTasks = paraTasks.Concat(new ExpandScriptsTask());

            SequentialTasks = seqTasks.OrderBy(t => t.Priority).ToArray();
            ParallelTasks = paraTasks.OrderBy(t => t.Priority).ToArray();
        }

        public IPreProcessingTask[] SequentialTasks { get; }

        public IPreProcessingTask[] ParallelTasks { get; }

        public virtual WipBlock[] PreProcess(IEnumerable<WipBlock> blocks, CancellationToken cancellationToken) {            
            foreach (var sequentialTask in SequentialTasks) {
                blocks = sequentialTask.Process(blocks, cancellationToken);
            }
            var blocksArr = blocks as WipBlock[] ?? blocks.ToArray();
            blocksArr.Update(b => b.StartParallelizedProcessingTime = DateTime.Now);
            Task.WaitAll(ParallelTasks.Select(t => Task.Run(() => t.Process(blocks, cancellationToken), cancellationToken)).ToArray());
            blocksArr.Update(b => b.EndParallelizedProcessingTime = DateTime.Now);
            return blocksArr;
        }


    }
}
