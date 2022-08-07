using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using BlockchainSQL.Processing.BusinessObjects;
using Hydrogen;

namespace BlockchainSQL.Processing {
	public abstract class FilesBlockStreamBase : BlockStreamBase {

        private readonly string _directoryPath;
        protected IBlockFileReader CurrentBlockFileReader;
        protected BlockFile CurrentBlockFile ;
        private long _totalBytes;

        public FilesBlockStreamBase(string directoryPath) {
            if (string.IsNullOrWhiteSpace(directoryPath)) throw new ArgumentException("Null or whitespace", nameof(directoryPath));
            ProcessingTierHelper.ValidateBlocksDirectory(directoryPath);
            _directoryPath = directoryPath;
            CurrentBlockFile = null;
            BlockFiles = new Dictionary<int, BlockFile>();
        }

        public override bool HasMore => CurrentBlockFileReader.HasMoreBlocks || CurrentBlockFile.FileNumber < BlockFiles.Values.Max(b => b.FileNumber);

        public IDictionary<int, BlockFile> BlockFiles { get; private set; }


        protected override void OpenInternal() {
            // load the block files 
            BlockFiles =
                ProcessingTierHelper
                    .GetBlockFiles(_directoryPath)
                    .ToDictionary(b => b.FileNumber);
            _totalBytes = BlockFiles.Values.Sum(b => b.FileSize);
            if (BlockFiles.Count > 0) {
                SeekToDiskPos(new DiskPos(BlockFiles[0].FileNumber, 0));
            }
        }

        protected override void CloseInternal() {
            SeekToDiskPos(DiskPos.None);
            BlockFiles.Clear();
            CurrentBlockFile = null;
            _totalBytes = 0;
        }

        protected virtual IBlockFileReader NewBlockFileReader(string blockFilePath) {
            var reader = BizLogicFactory.NewBlockFileReader(blockFilePath);
            reader.SkipTransactions = false;
            return reader;
        }
        protected override int SeekToTipInternal(BlockLocators blockLocators, CancellationToken cancellationToken) {
            SeekToDiskPos(DiskPos.None); // unlock any files

            var tipLocation = SearchTip(
                BlockFiles
                .Values
                .OrderBy(b => b.FileNumber)
                .ToArray(),
                blockLocators,
                cancellationToken
            );

            if (tipLocation.Equals(DiskPos.None)) {
                throw new SoftwareException("Unable to find blockchain tip (old or forked source)");
            }
            SeekToDiskPos(tipLocation);
            return CalculatePercentageProcessed();
        }

        protected abstract DiskPos SearchTip(BlockFile[] blockFiles,BlockLocators blockLocators, CancellationToken cancellationToken);


        protected virtual void SeekToDiskPos(DiskPos pos) {
            if (!pos.Equals(DiskPos.None)) {
                if (!BlockFiles.ContainsKey(pos.FileIndex))
                    throw new SoftwareException("Block file '{0}' not found in '{1}'", pos.FileIndex, _directoryPath);

                var targetBlockFile = BlockFiles[pos.FileIndex];
                if (CurrentBlockFile != null && CurrentBlockFile.FileNumber != targetBlockFile.FileNumber) {
                    CurrentBlockFileReader?.Dispose();
                    CurrentBlockFileReader = null;
                }

                if (CurrentBlockFileReader == null) {
                    CurrentBlockFileReader = NewBlockFileReader(targetBlockFile.Path);
                }

                CurrentBlockFileReader.Seek((int) pos.FileOffset, SeekOrigin.Begin);
                CurrentBlockFile = targetBlockFile;

            } else {
                CurrentBlockFileReader?.Dispose();
                CurrentBlockFileReader = null;
            }
        }

        protected int CalculatePercentageProcessed() {
            if (CurrentBlockFile == null)
                throw new SoftwareException("Not opened");
            return (int)Math.Floor((double)(CurrentBlockFile.AccumulatedFileSize + CurrentBlockFileReader.TotalBytesProcessed) / _totalBytes * 100D);
        }


    }
}