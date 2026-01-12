// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.Processing.BusinessObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
	public class UnindexedFilesBlockStream : FilesBlockStreamBase {
        public event EventHandlerEx FinishedFile;
        public event EventHandlerEx FinishedAll;

        public UnindexedFilesBlockStream(string directoryPath) : base(directoryPath) {
        }
        

        public static DiskPos SearchTipSerially(
            BlockFile[] blockFiles,
            BlockLocators blockLocators,
            CancellationToken cancellationToken) {
            var lastMatchBlockFileIndex = -1;
            var lastMatchBlockFileOffset = -1L;

            var locators = new SynchronizedList<byte[]>();
            locators.AddRangeSequentially(blockLocators.Locations.Select(l => l.Hash));
            lastMatchBlockFileIndex = -1;
            lastMatchBlockFileOffset = -1L;
            var foundTip = false;

            // Search all the files serially
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

        public static DiskPos SearchTipInParallel(
            BlockFile[] blockFiles,
            BlockLocators blockLocators,
            CancellationToken cancellationToken) {
            var lastMatchBlockFileIndex = -1;
            var lastMatchBlockFileOffset = -1L;

            var locators = new SynchronizedList<byte[]>();
            locators.AddRangeSequentially(blockLocators.Locations.Select(l => l.Hash));
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
            return SearchTipSerially(blockFiles, blockLocators, cancellationToken);
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
                        matchedLocator = locators.ContainsAny(comparer, peekResult.NextBlockPrevBlockHash);
                    if (matchedLocator) {
                        using (locators.EnterWriteScope()) {
                            var locatorIndex = locators.IndexOf(peekResult.NextBlockPrevBlockHash);
                            if (locatorIndex >= 0) {
                                lastMatchBlockFileIndex = blockFileIndex;
                                lastMatchBlockFileOffset = reader.TotalBytesProcessed;
                                if (locators.Count == 1 || locatorIndex == 0) {
                                    foundTip = true;
                                    break;
                                }
                                // Found something near the tip, trim everything after from locators
                                Debug.Assert(locatorIndex < locators.Count);
                                locators.RemoveRangeSequentially(locatorIndex, locators.Count - locatorIndex);
                            }
                        }
                    }
                    reader.Seek((int)peekResult.NextBlockSize + (int)peekResult.AdditionalOffset, SeekOrigin.Current);
                }
            }
        }
    }
}