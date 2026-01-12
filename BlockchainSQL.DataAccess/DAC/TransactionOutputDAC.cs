// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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
        private readonly string[] TransactionOutputColumns = {
            "ID",
            "TransactionID",
            "Index",
            "ToAddressType",
            "ToAddress",
            "Value",
            "ScriptID",
            "RowState"
        };

        public virtual IEnumerable<TransactionOutput> GetTransactionOutputs(long transactionID) {
            var results = FindTransactionOutputs(new[] { new ColumnValue("TransactionID", transactionID) }, null, "[Index] ASC");
            return results;
        }

        public virtual IEnumerable<TransactionOutput> GetTransactionOutputsByAddress(string address) {
            return FindTransactionOutputs(new[] {new ColumnValue("ToAddress", address)});
        }

        public virtual IEnumerable<TransactionOutput> GetTransactionOutputsByTXID(byte[] txid, bool loadTransaction) {
            const string query =
@"SELECT
    {0}
FROM
    TransactionOutput TXO INNER JOIN
    {1} T ON TXO.TransactionID = T.ID
WHERE
    T.TXID = {2}";


            var outputs = this.ExecuteQuery(
                this.QuickString(
                    query,
                    TransactionOutputColumns.Select(c => "TXO." + this.QuickString("{0}", SQLBuilderCommand.ColumnName(c))).ToDelimittedString(", "),
                    SQLBuilderCommand.TableName("Transaction"),
                    SQLBuilderCommand.Literal(txid)
                    )
                )
                .Rows
                .Cast<DataRow>()
                .Select(Hydrators.HydrateTransactionOutput)
                .ToArray();

            if (loadTransaction) {
                var tx = GetTransactionByTXID(txid, false);
                outputs.Update(i => i.Transaction = tx);
            }
            return outputs;
        }

        public virtual TransactionOutput GetTransactionOutputByTXID(byte[] txid, uint index) {
            const string query =
@"SELECT
    {0}
FROM
    TransactionOutput TXO INNER JOIN
    {1} T ON TXO.TransactionID = T.ID
WHERE
    T.TXID = {2} AND TXO.{3} = {4}";


            var results = this.ExecuteQuery(
                this.QuickString(
                    query,
                    TransactionOutputColumns.Select(c => "TXO." + this.QuickString("{0}", SQLBuilderCommand.ColumnName(c))).ToDelimittedString(", "),
                    SQLBuilderCommand.TableName("Transaction"),
                    SQLBuilderCommand.Literal(txid),
                    SQLBuilderCommand.ColumnName("Index"),
                    SQLBuilderCommand.Literal(index))
                )
                .Rows
                .Cast<DataRow>()
                .ToArray();

            if (results.Length != 1)
                throw new NoSingleRecordException("TransactionInput", results.Length);

            return Hydrators.HydrateTransactionOutput(results[0]);
        }


        

        public virtual IEnumerable<TransactionOutput> FindTransactionOutputs(IEnumerable<ColumnValue> columnMatches = null, string whereClause = null, string orderByClause = null) {
            return (this.Select(
                "TransactionOutput",
                TransactionOutputColumns,
                columnMatches: columnMatches,
                whereClause: whereClause,
                orderByClause: orderByClause ?? "[Index] ASC"
				))
                .Rows
                .Cast<DataRow>()
                .Select(Hydrators.HydrateTransactionOutput);
        }


    }
}


