using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;


// ReSharper disable InconsistentNaming

namespace BlockchainSQL.DataAccess {
    public class MSSQLVendorSpecificImplementation : DBVendorSpecificImplementationBase {

	    public override string GenerateConnectOutpointsQuery() 
		    => "SET QUERY_GOVERNOR_COST_LIMIT 0; " + Environment.NewLine + base.GenerateConnectOutpointsQuery();

	    public override string GenerateConnectOutpointsQuery(long fromTransactionInputID, long toTransactionInputID)
		    => "SET QUERY_GOVERNOR_COST_LIMIT 0; " + Environment.NewLine + base.GenerateConnectOutpointsQuery(fromTransactionInputID, toTransactionInputID);


	    public override string GenerateTotalizeTransactionsQuery()
		    => "SET QUERY_GOVERNOR_COST_LIMIT 0; " + Environment.NewLine + base.GenerateTotalizeTransactionsQuery();

		public override string GenerateTotalizeTransactionsQuery(long fromTransactionInputID, long toTransactionInputID)
			=> "SET QUERY_GOVERNOR_COST_LIMIT 0; " + Environment.NewLine + base.GenerateTotalizeTransactionsQuery(fromTransactionInputID, toTransactionInputID);

		public override string GenerateTotalizeBlocksQuery()
			=> "SET QUERY_GOVERNOR_COST_LIMIT 0; " + Environment.NewLine + base.GenerateTotalizeBlocksQuery();

		public override string GenerateTotalizeBlocksQuery(long fromBlockID, long toBlockID)
			=> "SET QUERY_GOVERNOR_COST_LIMIT 0; " + Environment.NewLine + base.GenerateTotalizeBlocksQuery(fromBlockID, toBlockID);

		public override bool HasDisabledApplicationIndexes(IDAC dac) {
            const string query = "SELECT i.name FROM sys.indexes i JOIN sys.tables t ON i.object_id = t.object_id WHERE i.[type] = 2 and i.is_disabled = 1 and t.name in ('Block', 'Branch', 'Script', 'ScriptInstruction', 'Settings', 'Text', 'Transaction', 'TransactionInput', 'TransactionOutput') ORDER BY i.type_desc, t.name, i.name";
            return (dac.ExecuteQuery(query)).Rows.Count > 0;
        }

        public override void EnableAllApplicationIndexes(IDAC dac) {
            const string query =
				@"SET QUERY_GOVERNOR_COST_LIMIT 0;
-- Enable Indexes
DECLARE @my_sql2 NVARCHAR(200);
DECLARE cur_rebuild CURSOR FOR 
   SELECT 'ALTER INDEX [' +  i.name + '] ON [' + t.name + '] REBUILD WITH (PAD_INDEX = ON, FILLFACTOR=90)' FROM sys.indexes i JOIN sys.tables t ON i.object_id = t.object_id WHERE i.[type] = 2 and i.is_disabled = 1 and t.name in ('Block', 'Branch', 'Script', 'ScriptInstruction', 'Settings', 'Text', 'Transaction', 'TransactionInput', 'TransactionOutput') ORDER BY i.type_desc, t.name, i.name;
OPEN cur_rebuild;
FETCH NEXT FROM cur_rebuild INTO @my_sql2;
WHILE @@FETCH_STATUS = 0
   BEGIN
      EXECUTE sp_executesql  @my_sql2;
      FETCH NEXT FROM cur_rebuild INTO @my_sql2;
   END;
CLOSE cur_rebuild;
DEALLOCATE cur_rebuild;


-- Enable foreign keys
DECLARE @my_sql3 NVARCHAR(200);
DECLARE cur_rebuild2 CURSOR FOR 
    SELECT 'ALTER TABLE [' + s.name + '].[' + o.name + '] WITH CHECK CHECK CONSTRAINT [' + i.name + ']' from sys.foreign_keys i INNER JOIN sys.objects o ON i.parent_object_id = o.object_id INNER JOIN sys.schemas s ON o.schema_id = s.schema_id and (i.is_disabled = 1 or i.is_not_trusted = 1)
OPEN cur_rebuild2;
FETCH NEXT FROM cur_rebuild2 INTO @my_sql3;
WHILE @@FETCH_STATUS = 0
   BEGIN
      EXECUTE sp_executesql  @my_sql3;
      FETCH NEXT FROM cur_rebuild2 INTO @my_sql3;
   END;
CLOSE cur_rebuild2;
DEALLOCATE cur_rebuild2;";
            dac.ExecuteNonQuery(query);
        }

        public override void DisableAllApplicationIndexes(IDAC dac) {
            const string query =
				@"SET QUERY_GOVERNOR_COST_LIMIT 0;
DECLARE @my_sql2 NVARCHAR(200);
DECLARE cur_rebuild CURSOR FOR 
   SELECT 'ALTER INDEX [' +  i.name + '] ON [' + t.name + '] DISABLE' FROM sys.indexes i JOIN sys.tables t ON i.object_id = t.object_id WHERE i.[type] = 2 and i.is_disabled = 0 and t.name in ('Block', 'Branch', 'Script', 'ScriptInstruction', 'Settings', 'Text', 'Transaction', 'TransactionInput', 'TransactionOutput') ORDER BY i.type_desc, t.name, i.name;
OPEN cur_rebuild;
FETCH NEXT FROM cur_rebuild INTO @my_sql2;
WHILE @@FETCH_STATUS = 0
   BEGIN
      EXECUTE sp_executesql  @my_sql2;
      FETCH NEXT FROM cur_rebuild INTO @my_sql2;
   END;
CLOSE cur_rebuild;
DEALLOCATE cur_rebuild";
            dac.ExecuteNonQuery(query);
        }

        public override void CleanupDatabase(IDAC dac) {
            const string query1 = @"SELECT CONVERT(NVARCHAR(MAX), Name) FROM sys.master_files WHERE database_id = db_id('{0}')  AND [Type] = 1";
            const string query2 = @"DBCC SHRINKFILE('{0}', 1)";
            var mssqlDAC = (MSSQLDAC) dac;
            if (!mssqlDAC.IsAzure()) {
                var dbName = Tools.MSSQL.GetDatabaseNameFromConnectionString(dac.ConnectionString).Trim().TrimStart("[").TrimEnd("]");
                var logName = dac.ExecuteScalar<string>(query1.FormatWith(dbName));
                dac.ExecuteNonQuery(query2.FormatWith(logName));
            }
        }

        public override DataTable ExecuteUserSQL(IDAC dac, string userSql, int page, int pageSize, string orderByHint, out int pageCount) {
            const string pagedQuery =
                @"WITH __UNPAGED_RESULT AS (	
    {0}
), __PAGED_RESULT AS (
    SELECT *, ROW_NUMBER() OVER ({3}) AS __ROWNUM FROM __UNPAGED_RESULT
)
SELECT * FROM __PAGED_RESULT WHERE __ROWNUM > {2} AND __ROWNUM <= ({2} + {1})";
            pageCount = -1;

            var table = dac.ExecuteQuery(pagedQuery.FormatWith(userSql, pageSize, page*pageSize, orderByHint ?? "ORDER BY @@IDENTITY"));
            table.Columns.Remove("__ROWNUM");
            return table;
        }

        public override IEnumerable<StatementLine> GetStatementLines(IDAC dac, string address) {
            const string query =
                #region Query

                @"WITH Credits AS (
	SELECT		
		B.TimeStampUtc AS TXDate,		
		TXO.Value / 100000000.0 AS TXAmount,	
		B.Height AS Block,
		TX.TXID AS TXID,
		TXO.[Index] AS TXID_IX,
		TXO.ID AS TXOID
	FROM
		TransactionOutput TXO INNER JOIN
		[Transaction] TX ON TXO.TransactionID = TX.ID INNER JOIN
		Block B ON TX.BlockID = B.ID 
	WHERE
		TXO.ToAddress = '{0}' AND
		B.BranchID = 1
), Debits AS (
	SELECT
		B.TimeStampUtc AS TXDate,
		TXI.OutpointTXID AS OutpointTXID,
		TXI.OutpointIndex AS OutpointIndex,
		TXI.Value / 100000000.0 AS TXAmount,	
		B.Height AS Block,
		TX.TXID AS TXID,
		TXI.[Index] AS TXID_IX
	FROM
		TransactionInput TXI INNER JOIN
		Credits C ON TXI.TransactionOutputID = C.TXOID INNER JOIN
		[Transaction] TX ON TXI.TransactionID = TX.ID INNER JOIN
		Block B ON TX.BlockID = B.ID
	WHERE
		B.BranchID = 1
)
SELECT TXDate, 'C' AS TXType, TXID, TXID_IX, TXAmount, Block FROM Credits
UNION ALL
SELECT TXDate, 'D' AS TXType, TXID, TXID_IX, TXAmount, Block FROM Debits
ORDER BY Block ASC, TXDate ASC";

            #endregion

			// Note: we rely on query governor to stop extremely large account statements. See Notion for ideas to solve this short and long-term.
            return dac.ExecuteQuery(/*"SET QUERY_GOVERNOR_COST_LIMIT 0; "+*/ query.FormatWith(address)).Rows.Cast<DataRow>().Select(Hydrators.HydrateStatementLine);
        }

    }
}
