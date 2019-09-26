using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.LevelDB;

namespace BlockchainSQL.Processing {
    public static class ProcessingTierHelper {

        public static void ExpandBlockScripts(Block[] blocks) {
            Parallel.ForEach(
                blocks
                    .SelectMany(b => b.Transactions.SelectMany(t => t.Inputs))
                    .Cast<TransactionItem>()
                    .Concat(
                        blocks.SelectMany(b => b.Transactions.SelectMany(t => t.Outputs))
                    ).ToArray(),
                   BitcoinProtocolParser.ExpandTransactionItemScript
            );
        }

        public static void ExpandTransactionScripts(Transaction[] transactions) {
            Parallel.ForEach(
                transactions
                    .SelectMany(t => t.Inputs)
                    .Concat(transactions.SelectMany(t => t.Outputs).Cast<TransactionItem>())
                    .ToArray(),
                    BitcoinProtocolParser.ExpandTransactionItemScript
                );
        }

        public static Result ValidateBlocksDirectory(string directoryPath) {
            var result = Result.Default;
            if (!Directory.Exists(directoryPath)) {
                result.AddError("Folder '{0}' does not exist", directoryPath);
                return result;
            }
            var blkFiles = GetBlockFileInfos(directoryPath);
            if (!blkFiles.Any()) {
                result.AddError("Directory '{0}' does not contain BLKXXX.dat files", directoryPath);
                return result;
            }

            var minFileNo = 0;
            var maxFileNo = -1;
            foreach (var file in blkFiles) {
                int fileNo;
                if (!int.TryParse(Path.GetFileNameWithoutExtension(file.FullName.ToLowerInvariant()).TrimStart("blk"), out fileNo)) {
                    result.AddError("Filename '{0}' is malformed", file);
                    continue;
                }
                if (fileNo < minFileNo) {
                    minFileNo = fileNo;
                }
                if (fileNo > maxFileNo) {
                    maxFileNo = fileNo;
                }
            }

            if (!result.Success)
                return result;

            if ((maxFileNo - minFileNo) + 1 != blkFiles.Length)
                result.AddError("Bulk data files are not in sequence.");

            return result;
        }

        public static Result ValidateLevelDBDirectory(string indexLevelDBPath) {           
            var result = Result.Default;
            if (Directory.Exists(indexLevelDBPath)) {
                try {
                    // DO NOT OPEN index path since it changes it and might corrupt it for Bitcoin Core! 
                    //using (new DB(indexLevelDBPath, new Options {CreateIfMissing = false, ErrorIfExists = false})) ;
                    if (!Tools.FileSystem.DirectoryContainsFiles(indexLevelDBPath, "CURRENT", "LOCK", "LOG"))
                        result.AddError("Index path does not appear to be a level-db directory");
                } catch (LevelDBException) {
                    result.AddError("LevelDB is missing");
                }
            } else {
                result.AddError("Directory '{0}' does not exist", indexLevelDBPath);
            }
            return result;
        }

        public static FileInfo[] GetBlockFileInfos(string directoryPath) {
            return new DirectoryInfo(directoryPath).EnumerateFiles("blk*.dat").ToArray();
        }


        public static BlockFile[] GetBlockFiles(string directoryPath) {
            var blockFiles =
                GetBlockFileInfos(directoryPath)
                    .Select(
                        f => new BlockFile {
                            FileNumber = BlockFileNumber(f.Name),
                            Path = f.FullName,
                            FileSize = f.Length,
                            AccumulatedFileSize = -1,
                        }
                    )
                    .OrderBy(b => b.FileNumber)
                    .ToArray();

            if (blockFiles.Length > 0) {
                blockFiles[0].AccumulatedFileSize = 0;
                for (int i = 1; i < blockFiles.Length; i++) {
                    blockFiles[i].AccumulatedFileSize = blockFiles[i - 1].AccumulatedFileSize + blockFiles[i - 1].FileSize;
                }
            }
            return blockFiles;
        }

        public static int BlockFileNumber(string filePath) {
            return int.Parse(Path.GetFileNameWithoutExtension(filePath).ToUpper().TrimStart("BLK"));
        }
    }
}
