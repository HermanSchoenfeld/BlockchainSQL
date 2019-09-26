using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing.BusinessObjects;
using Sphere10.Framework;
using Sphere10.Framework.Collections;
using Sphere10.Framework.Data;
using Tools;

namespace BlockchainSQL.Processing {
    public class UnindexedFilesBlockStream : FilesBlockStreamBase {
        public event EventHandlerEx FinishedFile;
        public event EventHandlerEx FinishedAll;

        public UnindexedFilesBlockStream(string directoryPath) : base(directoryPath) {
        }
        

        public static DiskPos SearchTipSequentually(
            BlockFile[] blockFiles,
            BlockLocators blockLocators,
            CancellationToken cancellationToken) {
            var lastMatchBlockFileIndex = -1;
            var lastMatchBlockFileOffset = -1L;

            var locators = new SynchronizedList<byte[]>();
            locators.AddRange(blockLocators.Locations.Select(l => l.Hash));
            lastMatchBlockFileIndex = -1;
            lastMatchBlockFileOffset = -1L;
            var foundTip = false;

            // Search all the files simultaneously
            for (var i = 0; i < blockFiles.Length; i++) {
                SearchTipInBlockFile(
                    blockFiles[i], 
                    i, 
                    locators,
                    ref foundTip,
                    ref lastMatchBlockFileIndex, 
                    ref lastMatchBlockFileOffset, 
                    cancellationToken
                );
            }

            cancellationToken.ThrowIfCancellationRequested();

            return 
                foundTip ? 
                new DiskPos(blockFiles[lastMatchBlockFileIndex].FileNumber, (uint)lastMatchBlockFileOffset) :
                DiskPos.None;
        }

        public static DiskPos SearchTipSimultaneously(
            BlockFile[] blockFiles,
            BlockLocators blockLocators,
            CancellationToken cancellationToken) {
            var lastMatchBlockFileIndex = -1;
            var lastMatchBlockFileOffset = -1L;

            var locators = new SynchronizedList<byte[]>();
            locators.AddRange(blockLocators.Locations.Select(l => l.Hash));
            lastMatchBlockFileIndex = -1;
            lastMatchBlockFileOffset = -1L;
            var foundTip = false;

            // Search all the files simultaneously
            Parallel.ForEach(
                Enumerable.Range(0, blockFiles.Length),
                (i) => {
                    var file = blockFiles[i];
                    SearchTipInBlockFile(
                        file,
                        i, 
                        locators, 
                        ref foundTip,
                        ref lastMatchBlockFileIndex, 
                        ref lastMatchBlockFileOffset, 
                        cancellationToken
                    );
                });

            cancellationToken.ThrowIfCancellationRequested();

            return
                foundTip ?
                new DiskPos(blockFiles[lastMatchBlockFileIndex].FileNumber, (uint)lastMatchBlockFileOffset) :
                DiskPos.None;
        }

        protected override DiskPos SearchTip(
            BlockFile[] blockFiles,
            BlockLocators blockLocators,
            CancellationToken cancellationToken) {
            // TODO: detect if SSD, use simultaneously -- sequentually optimized for HDD
            return SearchTipSequentually(blockFiles, blockLocators, cancellationToken);
        }


        protected override BlockStreamReadResult ReadNextBlocksInternal(bool skipTransactions, CancellationToken cancellationToken) {
            var startTime = DateTime.Now;
            if (!CurrentBlockFileReader.HasMoreBlocks) {
                if (!BlockFiles.ContainsKey(CurrentBlockFile.FileNumber + 1)) {
                    FinishedAll?.Invoke();
                    return BlockStreamReadResult.EndOfStream;
                }
                AdvanceBlockFileReader();
                return ReadNextBlocksInternal(skipTransactions, cancellationToken);
            }
            
            var nextBlock = CurrentBlockFileReader.ReadNextBlock();
            var pc = CalculatePercentageProcessed();
            return new BlockStreamReadResult {
                HasMoreBlocks = true,
                BlocksRead = new[] { new WipBlock(nextBlock, startTime, DateTime.Now, pc )},
                PercentageSourceRead = pc
            };
        }

        private void AdvanceBlockFileReader() {
            if (CurrentBlockFileReader != null) {
                CurrentBlockFileReader.Dispose();
                FinishedFile?.Invoke();
            }
            if (BlockFiles.ContainsKey(CurrentBlockFile.FileNumber + 1)) {
                SeekToDiskPos(new DiskPos(CurrentBlockFile.FileNumber + 1, 0));
            } else {
                SeekToDiskPos(DiskPos.None);
            }
        }

        private static void SearchTipInBlockFile(BlockFile blockFile, int blockFileIndex, SynchronizedList<byte[]> locators, ref bool foundTip, ref int lastMatchBlockFileIndex, ref long lastMatchBlockFileOffset, CancellationToken cancellationToken) {
            var blockFilePath = blockFile.Path;
            var reader = BizLogicFactory.NewBlockFileReader(blockFilePath);
            reader.SkipTransactions = true;
            var comparer = ByteArrayEqualityComparer.Instance;
            using (reader.OpenFileScope(blockFilePath)) {
                while (!foundTip && reader.HasMoreBlocks && !cancellationToken.IsCancellationRequested) {
                    var peekResult = reader.PeekNextBlock();
                    bool matchedLocator;
                    using (locators.EnterReadScope())
                        matchedLocator = locators.ContainsAny<byte[]>(comparer, peekResult.NextBlockPrevBlockHash);
                    if (matchedLocator) {
                        using (locators.EnterWriteScope()) {
                            var locatorIndex = locators.IndexOf(peekResult.NextBlockPrevBlockHash, comparer);
                            if (locatorIndex >= 0) {
                                lastMatchBlockFileIndex = blockFileIndex;
                                lastMatchBlockFileOffset = reader.TotalBytesProcessed;
                                if (locators.Count == 1 || locatorIndex == 0) {
                                    foundTip = true;
                                    break;
                                }
                                // Found something near the tip, trim everything after from locators
                                Debug.Assert(locatorIndex < locators.Count);
                                locators.RemoveRange(locatorIndex, locators.Count - locatorIndex);
                            }
                        }
                    }
                    reader.Seek((int)peekResult.NextBlockSize + (int)peekResult.AdditionalOffset, SeekOrigin.Current);
                }
            }
        }
    }
}