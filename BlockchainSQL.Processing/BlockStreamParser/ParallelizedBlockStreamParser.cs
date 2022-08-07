using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hydrogen;
using Hydrogen.Data;

namespace BlockchainSQL.Processing {
	public class ParallelizedBlockStreamParser : BizComponent, IBlockStreamParser {
        public ParallelizedBlockStreamParser(IBlockStream stream, IBlockLocator locator, IPreProcessor preProcessor,  IPostProcessor postProcessor, IBlockStreamPersistor persistor) {
            Stream = stream;
            Locator = locator;
            PreProcessor = preProcessor;
            PostProcessor = postProcessor;
            Persistor = persistor;
        
        }

        public IBlockStream Stream { get; }

        public IBlockLocator Locator { get; }

        public IPreProcessor PreProcessor { get; }

        public IPostProcessor PostProcessor { get;}

        public IBlockStreamPersistor Persistor { get; }

        public async Task Parse( CancellationToken cancellationToken, Action<int> progressCallback = null, bool deferPostProcessing = true, TimeSpan? pollSleepDuration = null) {
            if (pollSleepDuration != null)
                throw new ArgumentException("This implementation of '{0}' does not support polling of stream, make sure argument is null".FormatWith(nameof(IBlockStreamParser)), nameof(pollSleepDuration));

            const int minBufferSizeMB = 128;
            const int maxBufferSizeMB = 64000;
            progressCallback = progressCallback ?? (x => Tools.Lambda.NoOp());

            // Determine how to allocate memory to the buffers
            var userPreferredMaxMemory = Settings.Get<ServiceScannerSettings>().MaxMemoryBufferSizeMB.ClipTo(minBufferSizeMB, maxBufferSizeMB);
            var bufferSize = Tools.Memory.ConvertMemoryMetric(userPreferredMaxMemory,MemoryMetric.Megabyte,MemoryMetric.Byte);
            var saveScriptData = Settings.Get<ServiceScannerSettings>().StoreScriptData;
            var scannedQueueBufferPortion = 0.5;
            var processQueueBufferPortion = 0.5;

            long totalBlocksPersisted = 0;
            using (var scannedQueue = new ProducerConsumerQueue<WipBlock>(WipBlock.Estimate, (long) (bufferSize*scannedQueueBufferPortion)))
            using (var processedQueue = new ProducerConsumerQueue<WipBlock>(WipBlock.Estimate, (long) (bufferSize*processQueueBufferPortion)))
            using (var pipelineScope = new WipPipelineScope())
            using (await Stream.EnterOpenScope()) {
                var currentChainLocators = await Task.Run( () => Locator.GetBlockLocators());
                Log.Info("Locating blockchain tip");
                var currentPercentage = await Task.Run( () => Stream.SeekToTip(currentChainLocators, cancellationToken));
                Log.Info("Scanning started");
                progressCallback(currentPercentage);

                var scanTask = new Task(() => {
                    #region Scan task impl

                    try {
                        BlockStreamReadResult readResult;
                        do {
                            readResult = Stream.ReadNextBlocks(cancellationToken);
                            pipelineScope.AddBlocksToPipeline(readResult.BlocksRead);
                            scannedQueue.PutMany(readResult.BlocksRead);
                        } while (readResult.HasMoreBlocks && !scannedQueue.HasFinishedProducing && !processedQueue.HasFinishedConsuming && !cancellationToken.IsCancellationRequested);
                    } finally {
                        scannedQueue.FinishedProducing();
                    }

                    #endregion
                });

                var processTask = new Task(() => {
                    #region Process task impl

                    try {
                        while ((!scannedQueue.HasFinishedProducing || scannedQueue.Count > 0) && !processedQueue.HasFinishedProducing && !cancellationToken.IsCancellationRequested) {                                
                            var scannedItems = scannedQueue.TakeAll();
                            var processedItems = PreProcessor.PreProcess(scannedItems, cancellationToken);
                            processedQueue.PutMany(processedItems);
                        }
                        //if (processor.UnprocessedInputCount > 0) {
                        //    throw new LeftOverBlocksException(processor.UnprocessedInputCount);
                        //}
                    } finally {
                        scannedQueue.FinishedConsuming();
                        processedQueue.FinishedProducing();
                    }

                    #endregion
                });

                var persistTask = new Task(() => {
                    #region Persist task impl

                    try {
                        var dac = CreateDAC();
                        Persistor.CustomDAC = dac;
                        var persistBatchSize = (int) Tools.Memory.ConvertMemoryMetric(100, MemoryMetric.Megabyte, MemoryMetric.Byte);
                        var totalPersistedBytes = 0L;
                        using (var scope = dac.BeginScope()) {
                            while ((!processedQueue.HasFinishedProducing || processedQueue.Count > 0) && !cancellationToken.IsCancellationRequested) {
                                if (processedQueue.CurrentSize < persistBatchSize && processedQueue.AvailableCapacity > 0.25 && !processedQueue.HasFinishedProducing) {
                                    // Let persist buffer accumulate for a decent commit
                                    Task.Delay(100).Wait(cancellationToken);
                                    continue;                                    
                                }
                                var itemsToPersist = processedQueue.TakeBySize(persistBatchSize);
                                if (itemsToPersist.Length == 0)
                                    itemsToPersist = new[] {processedQueue.Take()};

                                var batchSize = itemsToPersist.Select(WipBlock.Estimate).Sum();
                                totalPersistedBytes += batchSize;

                                if (itemsToPersist.Length > 0) {
                                    // Persist blocks
                                    var persistStartTime = DateTime.Now;
                                    DateTime persistEndTime;
                                    using (var persistScope = Persistor.CustomDAC.BeginScope()) {
                                        persistScope.BeginTransaction();
                                        var persistResult = Persistor.Persist(itemsToPersist, saveScriptData, false, cancellationToken);                                        
                                        
                                        if (!deferPostProcessing) {
                                             PostProcessor.PostProcessPartial(persistResult);
                                        }
                                        persistEndTime = DateTime.Now;
                                        totalBlocksPersisted += persistResult.Block.Total;
                                        persistScope.Commit();
                                    }

                                    // Calculate performance metrics
                                    var processingPeriods = itemsToPersist.Select(i => new {StartProcessingTime = i.StartParallelizedProcessingTime, EndProcessingTime = i.EndParallelizedProcessingTime}).Distinct().ToArray();
                                    var scanDuration = itemsToPersist.Aggregate(TimeSpan.Zero, (s, i) => s.Add(i.FinishScanTime - i.StartScanTime));
                                    var processingDuration =
                                        itemsToPersist.Aggregate(TimeSpan.Zero, (s, i) => i.OrganizeDuration) + // syncronous processing duration
                                            processingPeriods.Aggregate(TimeSpan.Zero, (s, i) => s.Add(i.EndProcessingTime - i.StartProcessingTime)); // parallelized processing time
                                    var savingDuration = persistEndTime.Subtract(persistStartTime);
                                    var pipelineDuration = (scanDuration + processingDuration + savingDuration);
                                    var blockPersistedSize = itemsToPersist.Sum(i => i.Block.Size);
                                    progressCallback(itemsToPersist.Last().BlockchainSourcePercentage);

                                    // Log details
                                    Log.Info(string.Format("Processed {0:##########} blocks ({1:#} MB, TP: {2:0.00} MB/sec)\t Pipeline: (Scan: {3:0.00} sec, Process: {4:0.00} sec, Persist: {5:0.00} sec)",
                                        itemsToPersist.Count(),
                                        Tools.Memory.ConvertMemoryMetric(blockPersistedSize, MemoryMetric.Byte, MemoryMetric.Megabyte),
                                        Tools.Memory.ConvertMemoryMetric(blockPersistedSize, MemoryMetric.Byte, MemoryMetric.Megabyte)/pipelineDuration.TotalSeconds.ClipTo(Tools.Maths.EPSILON_D, double.MaxValue),
                                        scanDuration.TotalSeconds,
                                        processingDuration.TotalSeconds,
                                        savingDuration.TotalSeconds
                                    ));

                                    // Cleanup heap regularly
                                    GC.Collect();
                                    GC.WaitForPendingFinalizers();
                                }
                            }
                        }
                    } finally {
                        processedQueue.FinishedConsuming();
                        GC.Collect();
                    }

                    #endregion
                });
                using (scanTask)
                using (processTask)
                using (persistTask) {
                    scanTask.Start();
                    processTask.Start();
                    persistTask.Start();
                    await Task.WhenAll(scanTask, processTask, persistTask).WithAggregateException();
                    Log.Info(string.Format("Completed {0} blocks", totalBlocksPersisted));
                }
            }
        }
    }
}