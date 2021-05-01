using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing
{
    public class SequentialBlockStreamParser : BizComponent, IBlockStreamParser {

		public SequentialBlockStreamParser(IBlockStream stream, IBlockLocator locator, IPreProcessor preProcessor, IPostProcessor postProcessor, IBlockStreamPersistor persistor) {
			Stream = stream;
			Locator = locator;
			PreProcessor = preProcessor;
			PostProcessor = postProcessor;
			Persistor = persistor;
		}

		public IBlockStream Stream { get; }
		public IBlockLocator Locator { get; }

		public IPreProcessor PreProcessor { get; }
		public IPostProcessor PostProcessor { get; }

		public IBlockStreamPersistor Persistor { get; }

		public async Task Parse(CancellationToken cancellationToken, Action<int> progressCallback = null, bool deferPostProcessing = false, TimeSpan? pollSleepDuration = null) {
			const long minBufferSizeMb = 128;
			const long maxBufferSizeMb = 64000;

			var loopInfinitely = pollSleepDuration != null;



			progressCallback = progressCallback ?? (x => Tools.Lambda.NoOp());
			var saveScriptData = Settings.StoreScriptData;

			// Determine how to allocate memory to the buffers
			var userPreferredMaxMemory = Settings.MaxMemoryBufferSize.ClipTo(minBufferSizeMb, maxBufferSizeMb);
			var bufferSize = Tools.Memory.ConvertMemoryMetric(
				userPreferredMaxMemory,
				MemoryMetric.Megabyte,
				MemoryMetric.Byte
				);

			var scannedQueueBufferPortion = 0.5;
			var processQueueBufferPortion = 0.5;
			long totalBlocksPersisted = 0;
			var persistBatchSize = (int)Tools.Memory.ConvertMemoryMetric(100, MemoryMetric.Megabyte, MemoryMetric.Byte);
			var blockFetchSize = 10;

			var hasMoreBlocks = true;

			try {
				Log.Info("Contacting block stream");
				using (await Stream.EnterOpenScope()) {
					while (!cancellationToken.IsCancellationRequested) {
						// wait until node has new blocks
						if (await Task.Run(() => !Stream.HasMore, cancellationToken)) {
							if (!loopInfinitely) break;
							Log.Info("Reached tip, sleeping");
							await Task.Delay(pollSleepDuration.Value, cancellationToken);
							continue;
						}


						var blocks = await Task.Run(() => Stream.ReadNextBlocks(cancellationToken));
						if (blocks.BlocksRead.Any()) {
							DateTime start, persistStartTime, peristEndTime, endTime;
							start = DateTime.Now;
							var processed = await Task.Run(() => PreProcessor.PreProcess(blocks.BlocksRead, cancellationToken));
							persistStartTime = DateTime.Now;
							using (var scope = DAC.BeginScope()) {
								scope.BeginTransaction();
								var persistResult = await Task.Run(() => Persistor.Persist(processed, Settings.StoreScriptData, true, cancellationToken));
								peristEndTime = DateTime.Now;
								if (!deferPostProcessing) {
									await Task.Run(() => PostProcessor.PostProcessPartial(persistResult), cancellationToken);
								}
								scope.Commit();
								endTime = DateTime.Now;
							}

							// Calculate performance metrics
							var processingPeriods = processed.Select(i => new { StartProcessingTime = i.StartParallelizedProcessingTime, EndProcessingTime = i.EndParallelizedProcessingTime }).Distinct().ToArray();
							var scanDuration = processed.Aggregate(TimeSpan.Zero, (s, i) => s.Add(i.FinishScanTime - i.StartScanTime));
							var processingDuration =
								processed.Aggregate(TimeSpan.Zero, (s, i) => i.OrganizeDuration) + // syncronous processing duration
									processingPeriods.Aggregate(TimeSpan.Zero, (s, i) => s.Add(i.EndProcessingTime - i.StartProcessingTime)); // parallelized processing time
							var savingDuration = peristEndTime.Subtract(persistStartTime);
							var postProcessDuration = endTime.Subtract(peristEndTime);
							var pipelineDuration = (scanDuration + processingDuration + savingDuration + postProcessDuration);
							var blockPersistedSize = processed.Sum(i => i.Block.Size);
							progressCallback(blocks.PercentageSourceRead);

							// Log details
							Log.Info("Processed {0:##########} blocks ({1:#} MB, TP: {2:0.00} MB/sec)\t Pipeline: (Scan: {3:0.00} sec, Process: {4:0.00} sec, Persist: {5:0.00} sec, Post-Process: {6:0.00})",
								processed.Count(),
								Tools.Memory.ConvertMemoryMetric(blockPersistedSize, MemoryMetric.Byte, MemoryMetric.Megabyte),
								Tools.Memory.ConvertMemoryMetric(blockPersistedSize, MemoryMetric.Byte, MemoryMetric.Megabyte) / pipelineDuration.TotalSeconds.ClipTo(Tools.Maths.EPSILON_D, double.MaxValue),
								scanDuration.TotalSeconds,
								processingDuration.TotalSeconds,
								savingDuration.TotalSeconds,
								postProcessDuration.TotalSeconds
								);

							GC.Collect();
							GC.WaitForPendingFinalizers();
						}
					}
				}
			} catch (Exception error) {
				if (!(error is OperationCanceledException))
					Log.LogException(error);
				throw;
			}
		}
	}
}