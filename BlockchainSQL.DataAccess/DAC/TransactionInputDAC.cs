using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Hydrogen;
using Hydrogen.Data;
using Hydrogen.Data.Exceptions;

namespace BlockchainSQL.DataAccess {
    public partial class ApplicationDAC {
        private readonly string[] TransactionInputColumns = {
            "ID",
            "TransactionID",
            "Index",
            "ScriptID",
            "WitScriptID",
            "OutpointTXID",
            "OutpointIndex",
            "TransactionOutputID",
            "Value",
            "Sequence",
            "RowState",
        };
        public virtual IEnumerable<TransactionInput> GetTransactionInputs(long transactionID) {
            var results = FindTransactionInputs(new[] { new ColumnValue("TransactionID", transactionID) });
            return results;
        }

        public virtual IEnumerable<TransactionInput> GetTransactionInputsByOutput(IEnumerable<long> transactionOutputIDs) {
            if (!transactionOutputIDs.Any())
                return Enumerable.Empty<TransactionInput>();

            return FindTransactionInputs(new[] { new ColumnValue("TransactionOutputID", transactionOutputIDs) });
        }


        public virtual IEnumerable<TransactionInput> GetTransactionInputsByTXID(byte[] txid, bool loadTransaction, bool loadOutpoints) {
            const string query =
@"SELECT
    {0}
FROM
    TransactionInput TXI INNER JOIN
    {1} T ON TXI.TransactionID = T.ID
WHERE
    T.TXID = {2}";


            var inputs = this.ExecuteQuery(
                this.QuickString(
                    query,
                    TransactionInputColumns.Select(c => "TXI." + this.QuickString("{0}", SQLBuilderCommand.ColumnName(c))).ToDelimittedString(", "),
                    SQLBuilderCommand.TableName("Transaction"),
                    SQLBuilderCommand.Literal(txid)
                    )
                )
                .Rows
                .Cast<DataRow>()
                .Select(Hydrators.HydrateTransactionInput)
                .ToArray();

            if (loadTransaction) {
                var tx = GetTransactionByTXID(txid, false);
                inputs.Update(i => i.Transaction = tx);
            }

            if (loadOutpoints && inputs.Any(i => i.TransactionOutput != null)) {
                var outpoints =  
                    FindTransactionOutputs(new[] { new ColumnValue("ID", inputs.Where(i => i.TransactionOutput != null).Select(i => i.TransactionOutput?.ID) )})
                    .ToDictionary(o => o.ID);
                inputs.Where(i => i.TransactionOutput != null).Update(i => i.TransactionOutput = outpoints[i.TransactionOutput.ID]);
            }
            return inputs;
        } 

        public virtual TransactionInput GetTransactionInputByTXID(byte[] txid, uint index) {
            const string query =
@"SELECT
    {0}
FROM
    TransactionInput TXI INNER JOIN
    {1} T ON TXI.TransactionID = T.ID
WHERE
    T.TXID = {2} AND TXI.{3} = {4}";


            var results = this.ExecuteQuery(
                this.QuickString(
                    query,
                    TransactionInputColumns.Select(c => "TXI." + this.QuickString("{0}", SQLBuilderCommand.ColumnName(c))).ToDelimittedString(", "),
                    SQLBuilderCommand.TableName("Transaction"),
                    SQLBuilderCommand.Literal(txid),
                    SQLBuilderCommand.ColumnName("Index"),
                    SQLBuilderCommand.Literal(index))
                )
                .Rows
                .Cast<DataRow>()
                .ToArray();

            if (results.Length != 1)
                throw new NoSingleRecordException("TransactionOutput", results.Length);

            return Hydrators.HydrateTransactionInput(results[0]);
        }

        public virtual IEnumerable<TransactionInput> FindTransactionInputs(IEnumerable<ColumnValue> columnMatches = null, string whereClause = null, string orderByClause = null) {
            return (this.Select(
                "TransactionInput",
                TransactionInputColumns,
                columnMatches: columnMatches,
                whereClause: whereClause,
                orderByClause: orderByClause ?? "[Index] ASC"
                ))
                .Rows
                .Cast<DataRow>()
                .Select(Hydrators.HydrateTransactionInput);
        }

        public virtual TransactionInput GetTransactionInputById(uint id) {
	        const string query =
		        @"SELECT TOP(1)
    {0}
FROM
    TransactionInput
    WHERE ID = {1}";


	        var results = this.ExecuteQuery(
			        this.QuickString(
				        query,
				        TransactionInputColumns.Select(c => this.QuickString("{0}", SQLBuilderCommand.ColumnName(c))).ToDelimittedString(", "),
				        SQLBuilderCommand.Literal(id))
		        )
		        .Rows
		        .Cast<DataRow>()
		        .ToArray();

	        if (results.Length != 1)
		        throw new NoSingleRecordException("TransactionInput", results.Length);

	        return Hydrators.HydrateTransactionInput(results[0]);
        }
    }
}





