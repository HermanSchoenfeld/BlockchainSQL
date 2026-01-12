using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.Exceptions;

namespace BlockchainSQL.DataAccess {
    public partial class ApplicationDAC {
        
        public virtual int CountBlocksFromHeight(int branchID, int height) {
            return (int)this.Count("Block", whereClause: "Height >= {0} AND BranchID = {1}".FormatWith(height, branchID));
        }


        public virtual int CountBlocks(int branchID) {
            return (int)this.Count("Block", whereClause: "BranchID = {0}".FormatWith(branchID));
        }

        public virtual Block GetBlockByHash(byte[] hash) {
            var results = (FindBlocks(new[] {new ColumnValue("Hash", hash)})).ToArray();
            if (results.Length != 1)
                throw new NoSingleRecordException("Block", hash, results.Length);
            return results[0];
        }
		public virtual Block GetActiveBlockByHeight(long height) { 
			var results = (FindBlocks(new[] { new ColumnValue("Height", height), new ColumnValue("BranchID", 1) })).ToArray();
			if (results.Length != 1)
				throw new NoSingleRecordException("Block", height, results.Length);
			return results[0];
		}


		public virtual IEnumerable<Block> GetBlocksByHash(IEnumerable<byte[]> hashes) {
            return FindBlocks(new[] { new ColumnValue("Hash", hashes) });
        }



        public virtual Block GetBlockByID(int id) {
            var results = (FindBlocks(new[] { new ColumnValue("ID", id) })).ToArray();
            if (results.Length != 1)
                throw new NoSingleRecordException("Block", id, results.Length);
            return results[0];
        }

        public virtual IEnumerable<Block> GetBlocks(int limit, int offset, SortOption[] sortOptions) {
            if (sortOptions == null)
                sortOptions = new SortOption[0];

            if (sortOptions.Select(so => so.Name.ToUpperInvariant()).Any(n => !n.IsIn("ID", "HEIGHT", "TIMESTAMPUNIX", "TIMESTAMPUTC", "SIZE", "TRANSACTIONCOUNT", "OUTPUTSBTC", "FEESBTC", "NONCE")))
                throw new ArgumentOutOfRangeException("Allowed sort columns are ID, Height, TimeStampUnix or Size");

            var builder = CreateSQLBuilder();
            builder.EmitOrderByExpression(sortOptions);
            var orderByClause = builder.ToString().Trim();
            return FindBlocks(
                new [] { new ColumnValue("BranchID", (int)KnownBranches.MainChain) },
                limit: limit, 
                offset: offset, 
                orderByClause: orderByClause
            );
        } 

        public virtual long GetMaxBlockHeight() {
            var height = this.ExecuteScalar<long?>(this.QuickString("SELECT MAX({0}) FROM {1} WHERE {2}={3}", SQLBuilderCommand.ColumnName("Height"), SQLBuilderCommand.TableName("Block"), SQLBuilderCommand.ColumnName("BranchID"), SQLBuilderCommand.Literal((int)KnownBranches.MainChain)));
            return height ?? -1;
        }

        public virtual IEnumerable<BlockLocation> GetBlockLocators(IEnumerable<long> heightIndices) {
            return (this.ExecuteQuery(
                this.QuickString("SELECT {0}, {2} FROM {1} WHERE {2} IN ({3}) AND {4} = {5} ORDER BY {2} DESC",
                    SQLBuilderCommand.ColumnName("Hash"),
                    SQLBuilderCommand.TableName("Block"),
                    SQLBuilderCommand.ColumnName("Height"),
                    SQLBuilderCommand.CommaSeparatedValues(heightIndices),
                    SQLBuilderCommand.ColumnName("BranchID"),
                    SQLBuilderCommand.Literal((int) KnownBranches.MainChain)
                    )
                ))
                .Rows
                .Cast<DataRow>()
                .Select(Hydrators.HydrateBlockLocation);
        }

        public virtual bool BlockExists(byte[] hash) {
            return this.Count("Block", new[] { new ColumnValue("Hash", hash) }) > 0;
        }

        public virtual IEnumerable<Block> FindBlocks(IEnumerable<ColumnValue> columnMatches = null, string whereClause = null, int? limit = null, int? offset = null, string orderByClause = null)
        {
            return (this.Select(
                "Block",
                new[] {
                    "ID",
                    "Height",
                    "PreviousBlockHash",
                    "Hash",
                    "BranchID",
                    "Size",
                    "Nonce",
                    "TimeStampUnix",
                    "TimeStampUtc",
                    "MerkleRoot",
                    "Bits",
                    "Difficulty",
                    "Version",
                    "TransactionCount",
                    "OutputsBTC",
                    "RewardBTC",
                    "FeesBTC",
                    "RowState"
                },
                columnMatches: columnMatches,
                whereClause: whereClause,
                limit: limit,
                offset: offset,
                orderByClause: orderByClause
                ))
                .Rows
                .Cast<DataRow>()
                .Select(Hydrators.HydrateBlock);
        }

    }
}


