using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using NUnit.Framework;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.NUnit {

    [TestFixture]
    public class BlockchainOrganizerTests : BaseTestFixture {

        protected object[] DBMS = {
            new object[] {DBMSType.SQLServer},
        };

        protected object[] DBMS_BulkLoadOptimized = {
            new object[] {DBMSType.SQLServer, false},
            new object[] {DBMSType.SQLServer, true},
        };

        protected object[] DBMS_BatchSize_BulkLoadOptimized = {
            new object[] {DBMSType.SQLServer, 1, false},
            new object[] {DBMSType.SQLServer, 2, false},
            new object[] {DBMSType.SQLServer, 4, false},
            new object[] {DBMSType.SQLServer, 10, false},
            new object[] {DBMSType.SQLServer, 20, false},
            new object[] {DBMSType.SQLServer, 1, true},
            new object[] {DBMSType.SQLServer, 2, true},
            new object[] {DBMSType.SQLServer, 4, true},
            new object[] {DBMSType.SQLServer, 10, true},
            new object[] {DBMSType.SQLServer, 20, true},
        };


        [Test]
        [TestCaseSource("DBMS_BatchSize_BulkLoadOptimized")]
        public void ReOrg_Basic_1(DBMSType dbmsType, int processBatchSize, bool bulkLoadOptimized) {

            using (var scope = EnterTestScope(dbmsType)) {
                var blocks = new[] {
                    scope.GenBlock("1", null, 0),
                    scope.GenBlock("2", "1", 0),
                    scope.GenBlock("2a", "1", 0),
                    scope.GenBlock("3", "2a", 0),
                };

                foreach (var partition in blocks.Partition(processBatchSize)) {
                    var blockSet = partition.ToArray();
                    var organizer = new OptimizedBlockOrganizer(bulkLoadOptimized);
                    var persistSet = organizer.Organize(blockSet);
                    Assert.AreEqual(blockSet.Length, persistSet.Length);
                    scope.DAC.BatchInsertBlocks(persistSet.Select(x => x.Block), true, true);
                }

                AssertBlockchain(scope.DAC, "1", "2a", "3");
            }
        }


		[Test]
        [TestCaseSource("DBMS_BatchSize_BulkLoadOptimized")]
        public void OutOfSequence_Basic(DBMSType dbmsType, int processBatchSize, bool bulkLoadOptimized) {

            using (var scope = EnterTestScope(dbmsType)) {
                var blocks = new[] {
                    scope.GenBlock("2", "1", 0),
                    scope.GenBlock("4", "3", 0),
                    scope.GenBlock("3", "2", 0),                    
                    scope.GenBlock("1", null, 0),
                };
                var unprocessedBlocks = new List<WipBlock>();
                var organizer = new OptimizedBlockOrganizer(bulkLoadOptimized);
                foreach (var partition in blocks.Partition(processBatchSize)) {
                    var blockSet = partition.Concat(unprocessedBlocks).ToArray();
                    unprocessedBlocks.Clear();
                    WipBlock[] danglingBlocks;
                    var persistSet = organizer.Organize(blockSet, out danglingBlocks);
                    unprocessedBlocks.AddRange(danglingBlocks);
                    scope.DAC.BatchInsertBlocks(persistSet.Select(x => x.Block), true, true);
                }
                AssertBlockchain(scope.DAC, "1", "2", "3", "4");
            }
        }


        [Test]
        [TestCaseSource("DBMS_BatchSize_BulkLoadOptimized")]
        public void ReOrg_Complex(DBMSType dbmsType, int processBatchSize, bool bulkLoadOptimized) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("0a", null, 0),
                    scope.GenBlock("1", "0a", 1),
                    scope.GenBlock("2", "1", 2),
                    scope.GenBlock("3", "2", 3),
                    scope.GenBlock("4", "3", 4),
                    scope.GenBlock("4a", "3", 5),
                    scope.GenBlock("4b", "3", 6),
                    scope.GenBlock("5", "4", 7),
                    scope.GenBlock("5a", "4", 8),
                    scope.GenBlock("5b", "4b", 9),
                    scope.GenBlock("5c", "4b", 10),
                    scope.GenBlock("5d", "4b", 11),
                    scope.GenBlock("6c", "5c", 12),
                    scope.GenBlock("6", "5", 13),
                    scope.GenBlock("7", "6", 14),
                };
                var organizer = new OptimizedBlockOrganizer(bulkLoadOptimized);
                foreach (var partition in blocks.Partition(processBatchSize)) {
                    var blockSet = partition.ToArray();
                    var persistSet = organizer.Organize(blockSet);
                    Assert.AreEqual(blockSet.Length, persistSet.Length);
                    dac.BatchInsertBlocks(persistSet.Select(x => x.Block), true, true);
                }
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5", "6", "7");
            }
        }

        [Test]
        [TestCaseSource("DBMS_BatchSize_BulkLoadOptimized")]
        public void ReOrg_Complex_OutOfSequence(DBMSType dbmsType, int processBatchSize, bool bulkLoadOptimized) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("7", "6", 14),
                    scope.GenBlock("1", "0a", 1),
                    scope.GenBlock("4a", "3", 5),
                    scope.GenBlock("2", "1", 2),
                    scope.GenBlock("3", "2", 3),
                    scope.GenBlock("4", "3", 4),
                    scope.GenBlock("5d", "4b", 11),
                    scope.GenBlock("0a", null, 0),
                    scope.GenBlock("5", "4", 7),
                    scope.GenBlock("4b", "3", 6),
                    scope.GenBlock("5a", "4", 8),
                    scope.GenBlock("5c", "4b", 10),
                    scope.GenBlock("5b", "4b", 9),
                    scope.GenBlock("6", "5", 13),
                    scope.GenBlock("6c", "5c", 12),
                };
                var unprocessedBlocks = new List<WipBlock>();
                var organizer = new OptimizedBlockOrganizer(bulkLoadOptimized);
                foreach (var partition in blocks.Partition(processBatchSize)) {
                    var blockSet = partition.Concat(unprocessedBlocks).ToArray();
                    unprocessedBlocks.Clear();
                    WipBlock[] danglingBlocks;
                    var persistSet = organizer.Organize(blockSet, out danglingBlocks);
                    unprocessedBlocks.AddRange(danglingBlocks);
                    dac.BatchInsertBlocks(persistSet.Select(x => x.Block), true, true);
                }
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5", "6", "7");
            }
        }


        [Test]
        [TestCaseSource("DBMS_BulkLoadOptimized")]
        public void ReOrg_Complex_StepByStep_NoCache(DBMSType dbmsType, bool bulkLoadOptimized) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("0a", null, 0),
                    scope.GenBlock("1", "0a", 1),
                    scope.GenBlock("2", "1", 2),
                    scope.GenBlock("3", "2", 3),
                    scope.GenBlock("4", "3", 4),
                    scope.GenBlock("4a", "3", 5),
                    scope.GenBlock("4b", "3", 6),
                    scope.GenBlock("5", "4", 7),
                    scope.GenBlock("5a", "4", 8),
                    scope.GenBlock("5b", "4b", 9),
                    scope.GenBlock("5c", "4b", 10),
                    scope.GenBlock("5d", "4b", 11),
                    scope.GenBlock("6c", "5c", 12),
                    scope.GenBlock("6", "5", 13),
                    scope.GenBlock("7", "6", 14),

                };
                Action<WipBlock> saveBlock = block => {
                    var organizer = new OptimizedBlockOrganizer(bulkLoadOptimized);
                    var persistSet = organizer.Organize(new[] {block});
                    Assert.AreEqual(1, persistSet.Length);
                    dac.BatchInsertBlocks(persistSet.Select(x => x.Block), true, true);
                };

                saveBlock(blocks[0]);
                AssertBlockchain(dac, "0a"); // 0

                saveBlock(blocks[1]);
                AssertBlockchain(dac, "0a", "1"); // 1

                saveBlock(blocks[2]);
                AssertBlockchain(dac, "0a", "1", "2"); // 2

                saveBlock(blocks[3]);
                AssertBlockchain(dac, "0a", "1", "2", "3"); // 3

                saveBlock(blocks[4]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4"); // 4

                saveBlock(blocks[5]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4"); // 4a

                saveBlock(blocks[6]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4"); // 4b

                saveBlock(blocks[7]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5

                saveBlock(blocks[8]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5a

                saveBlock(blocks[9]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5b

                saveBlock(blocks[10]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5c

                saveBlock(blocks[11]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5d

                saveBlock(blocks[12]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4b", "5c", "6c"); // 6c

                saveBlock(blocks[13]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4b", "5c", "6c"); // 6

                saveBlock(blocks[14]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5", "6", "7"); // 7

            }
        }

        [Test]
        [TestCaseSource("DBMS_BulkLoadOptimized")]
        public void ReOrg_Complex_StepByStep_Cache(DBMSType dbmsType, bool bulkLoadOptimized) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("0a", null, 0),
                    scope.GenBlock("1", "0a", 1),
                    scope.GenBlock("2", "1", 2),
                    scope.GenBlock("3", "2", 3),
                    scope.GenBlock("4", "3", 4),
                    scope.GenBlock("4a", "3", 5),
                    scope.GenBlock("4b", "3", 6),
                    scope.GenBlock("5", "4", 7),
                    scope.GenBlock("5a", "4", 8),
                    scope.GenBlock("5b", "4b", 9),
                    scope.GenBlock("5c", "4b", 10),
                    scope.GenBlock("5d", "4b", 11),
                    scope.GenBlock("6c", "5c", 12),
                    scope.GenBlock("6", "5", 13),
                    scope.GenBlock("7", "6", 14),
                };
                var organizer = new OptimizedBlockOrganizer(bulkLoadOptimized);
                Action<WipBlock> saveBlock = block => {                    
                    var persistSet = organizer.Organize(new[] { block });
                    Assert.AreEqual(1, persistSet.Length);
                    dac.BatchInsertBlocks(persistSet.Select(x => x.Block), true, true);
                };

                saveBlock(blocks[0]);
                AssertBlockchain(dac, "0a"); // 0

                saveBlock(blocks[1]);
                AssertBlockchain(dac, "0a", "1"); // 1

                saveBlock(blocks[2]);
                AssertBlockchain(dac, "0a", "1", "2"); // 2

                saveBlock(blocks[3]);
                AssertBlockchain(dac, "0a", "1", "2", "3"); // 3

                saveBlock(blocks[4]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4"); // 4

                saveBlock(blocks[5]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4"); // 4a

                saveBlock(blocks[6]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4"); // 4b

                saveBlock(blocks[7]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5

                saveBlock(blocks[8]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5a

                saveBlock(blocks[9]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5b

                saveBlock(blocks[10]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5c

                saveBlock(blocks[11]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5"); // 5d

                saveBlock(blocks[12]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4b", "5c", "6c"); // 6c

                saveBlock(blocks[13]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4b", "5c", "6c"); // 6

                saveBlock(blocks[14]);
                AssertBlockchain(dac, "0a", "1", "2", "3", "4", "5", "6", "7"); // 7
            }
        }



        [Test]
        [TestCaseSource("DBMS")]
        public void Dangling_0(DBMSType dbmsType) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("1", "0a", 1),
                    scope.GenBlock("4", "3", 4),
                    scope.GenBlock("0a", null, 0),
                };
                WipBlock[] unprocessedBlocks;
                var organizer = new OptimizedBlockOrganizer(false);

                var organizedBlocks = organizer.Organize(blocks, out unprocessedBlocks);
                Assert.AreEqual(organizedBlocks.Length, 2);
                Assert.AreEqual(organizedBlocks[0].Block.Hash, TestHelper.ToHashBytes("0a"));
                Assert.AreEqual(organizedBlocks[1].Block.Hash, TestHelper.ToHashBytes("1"));

                Assert.AreEqual(unprocessedBlocks.Length, 1);
                Assert.AreEqual(unprocessedBlocks[0].Block.Hash, TestHelper.ToHashBytes("4"));
            }
        }

        [Test]
        [TestCaseSource("DBMS")]
        public void Dangling_1(DBMSType dbmsType) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("2", "1", 2),
                    scope.GenBlock("1", "0a", 1),                    
                    scope.GenBlock("4", "3", 4),
                    scope.GenBlock("0a", null, 0),
                };
                WipBlock[] unprocessedBlocks;
                var organizer = new OptimizedBlockOrganizer(false);

                var organizedBlocks = organizer.Organize(blocks, out unprocessedBlocks);
                Assert.AreEqual(organizedBlocks.Length, 3);
                Assert.AreEqual(organizedBlocks[0].Block.Hash, TestHelper.ToHashBytes("0a"));
                Assert.AreEqual(organizedBlocks[1].Block.Hash, TestHelper.ToHashBytes("1"));
                Assert.AreEqual(organizedBlocks[2].Block.Hash, TestHelper.ToHashBytes("2"));

                Assert.AreEqual(unprocessedBlocks.Length, 1);
                Assert.AreEqual(unprocessedBlocks[0].Block.Hash, TestHelper.ToHashBytes("4"));
            }
        }

        [Test]
        [TestCaseSource("DBMS")]
        public void Dangling_2(DBMSType dbmsType) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("0a", null, 0),
                    scope.GenBlock("1", "0a", 1),
                    scope.GenBlock("2", "1", 2),
                    //scope.GenBlock("3", "2", 3),
                    scope.GenBlock("4", "3", 4),
                };
                WipBlock[] unprocessedBlocks;
                var organizer = new OptimizedBlockOrganizer(false);

                var organizedBlocks = organizer.Organize(blocks, out unprocessedBlocks);
                Assert.AreEqual(organizedBlocks.Length, 3);
                Assert.AreEqual(organizedBlocks[0].Block.Hash, TestHelper.ToHashBytes("0a"));
                Assert.AreEqual(organizedBlocks[1].Block.Hash, TestHelper.ToHashBytes("1"));
                Assert.AreEqual(organizedBlocks[2].Block.Hash, TestHelper.ToHashBytes("2"));

                Assert.AreEqual(unprocessedBlocks.Length, 1);
                Assert.AreEqual(unprocessedBlocks[0].Block.Hash, TestHelper.ToHashBytes("4"));
            }
        }

        [Test]
        [TestCaseSource("DBMS")]
        public void Dangling_3(DBMSType dbmsType) {
            using (var scope = EnterTestScope(dbmsType)) {
                var dac = scope.DAC;
                var blocks = new[] {
                    scope.GenBlock("4", "3", 4),
                    scope.GenBlock("0a", null, 0),
                    scope.GenBlock("1", "0a", 1),
                    scope.GenBlock("2", "1", 2),                    
                };
                WipBlock[] unprocessedBlocks;
                var organizer = new OptimizedBlockOrganizer(false);

                var organizedBlocks = organizer.Organize(blocks, out unprocessedBlocks);
                Assert.AreEqual(organizedBlocks.Length, 3);
                Assert.AreEqual(organizedBlocks[0].Block.Hash, TestHelper.ToHashBytes("0a"));
                Assert.AreEqual(organizedBlocks[1].Block.Hash, TestHelper.ToHashBytes("1"));
                Assert.AreEqual(organizedBlocks[2].Block.Hash, TestHelper.ToHashBytes("2"));

                Assert.AreEqual(unprocessedBlocks.Length, 1);
                Assert.AreEqual(unprocessedBlocks[0].Block.Hash, TestHelper.ToHashBytes("4"));
            }
        }


		[Test]
		[TestCaseSource("DBMS_BatchSize_BulkLoadOptimized")]
		public void FromOrphanToMain(DBMSType dbmsType, int processBatchSize, bool bulkLoadOptimize) {
			using (var scope = EnterTestScope(dbmsType)) {
				var dac = scope.DAC;
				var blocks = new List<WipBlock>() {
					scope.GenBlock("1", null, 0),
					scope.GenBlock("2", "1", 1),
					scope.GenBlock("2a", "1", 2),
					scope.GenBlock("3a", "2a", 3),
					scope.GenBlock("3", "2", 4),
					scope.GenBlock("4", "3", 5),
				};

				var unprocessedBlocks = new List<WipBlock>();
				var organizer = new OptimizedBlockOrganizer(bulkLoadOptimize);
				foreach (var partition in blocks.Partition(processBatchSize)) {
					var blockSet = partition.Concat(unprocessedBlocks).ToArray();
					unprocessedBlocks.Clear();
					WipBlock[] danglingBlocks;
					var persistSet = organizer.Organize(blockSet, out danglingBlocks);
					unprocessedBlocks.AddRange(danglingBlocks);
					dac.BatchInsertBlocks(persistSet.Select(x => x.Block), true, true);
				}
				AssertBlockchain(dac, "1", "2", "3", "4");
			}
		}
	}
}
