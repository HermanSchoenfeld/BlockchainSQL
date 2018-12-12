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
        private readonly string[] TransactionColumns = new[] {
            "ID",
            "TXID",
            "Size",
            "Version",
            "InputCount",
            "OutputCount",
            "InputsBTC",
            "OutputsBTC",
            "FeeBTC",
            "LockTime",
            "Index",
            "BlockID",
            "RowState"
        };

        public virtual Transaction GetTransaction(long id, bool loadItems) {
            var results = (FindTransactions(new[] { new ColumnValue("ID", id) })).ToArray();
            if (results.Length != 1)
                throw new NoSingleRecordException("Transaction", id, results.Length);
            var tx = results[0];
            if (loadItems) {
                tx.Inputs = GetTransactionInputs(tx.ID).ToArray();
                tx.Outputs = GetTransactionOutputs(tx.ID).ToArray();
            }
            return tx;
        }

        public virtual Transaction GetTransactionByTXID(byte[] txid, bool loadBlock = false) {
            // change this to byte
            var results = (FindTransactions(new[] { new ColumnValue("TXID", txid) })).ToArray();
            if (results.Length != 1)
                throw new NoSingleRecordException("Transaction", txid, results.Length);
            var txn = results[0];
            if (loadBlock) {
                var block = GetBlockByID(txn.Block.ID);
                txn.Block = block;
            }
            return txn;
        }

        public virtual IEnumerable<Transaction> GetTransactionsByBlockID(int blockID, int? limit = null, int? offset = null, SortOption[] sortOptions = null) {
            if (sortOptions == null)
                sortOptions = new SortOption[0];

            if (sortOptions.Select(so => so.Name.ToUpperInvariant()).Any(n => !n.IsIn(TransactionColumns.Select(s => s.ToUpperInvariant()))))
                throw new ArgumentOutOfRangeException("Allowed sort columns are: " + TransactionColumns.ToDelimittedString(","));
            var builder = CreateSQLBuilder();
            builder.EmitOrderByExpression(sortOptions);
            var orderByClause = builder.ToString().Trim();
            return FindTransactions(new[] {new ColumnValue("BlockID", blockID)}, limit, offset, orderByClause: orderByClause);
        }

        public virtual IEnumerable<Transaction> GetTransactionsByBlockHash(byte[] blockHash, int limit, int offset, SortOption[] sortOptions) {
            var builder = CreateSQLBuilder();
            builder.Emit("SELECT ");
            TransactionColumns
                .WithDescriptions()
                .ForEach(tc => {
                    if (!tc.Description.HasFlag(EnumeratedItemDescription.First))
                        builder.Emit(", ");
                    builder.Emit("T.");
                    builder.ColumnName(tc.Item);
                });
            builder.Emit(" FROM {0} T INNER JOIN {1} B ON T.{2} = B.{3} WHERE B.{4} = {5} ", SQLBuilderCommand.TableName("Transaction"), SQLBuilderCommand.TableName("Block"), SQLBuilderCommand.ColumnName("BlockID"), SQLBuilderCommand.ColumnName("ID"), SQLBuilderCommand.ColumnName("Hash"), SQLBuilderCommand.Literal(blockHash));

#warning CHEAP PAGING HERE -- NEEDS TO BE DONE IN SQL
            var rows = 
                ExecuteQuery(builder.ToString())
                .Rows
                .Cast<DataRow>();
            foreach (var option in sortOptions) {
                switch (option.Direction) {
                    case SortDirection.Ascending:
                        rows = rows.OrderBy(r => r[option.Name]);
                        break;
                    case SortDirection.Descending:
                        rows = rows.OrderByDescending(r => r[option.Name]);
                        break;
                }
            }
            return rows
                .Skip(offset)
                .Take(limit)
                 .Select(Hydrators.HydrateTransaction);
        }

        public virtual IEnumerable<Transaction> FindTransactions(IEnumerable<ColumnValue> columnMatches = null, int? limit = null, int? offset = null, string whereClause = null, string orderByClause = null) {
            return (this.Select(
                "Transaction",
                TransactionColumns,
                columnMatches: columnMatches,
                whereClause: whereClause,
                limit: limit,
                offset: offset,
                orderByClause: orderByClause
            ))
            .Rows
            .Cast<DataRow>()
            .Select(Hydrators.HydrateTransaction);
        }

        public virtual bool TransactionExists(byte[] txid) {
            return this.Count("Transaction", new[] {new ColumnValue("TXID", txid)}) > 0;
        }
    }
}


