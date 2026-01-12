using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Web.DataObjects;
using NHibernate;
using Sphere10.Framework.Data.NHibernate;

namespace BlockchainSQL.Web.DataAccess {
    public class PrimingDataGenerator : NHDataGeneratorBase {
        public PrimingDataGenerator(ISessionFactory sessionFactory) : base(sessionFactory) {
        }

        protected override IEnumerable<object> CreateData() {
            return
                CreateQueryCategories();

        }

        protected virtual IEnumerable<QueryCategory> CreateQueryCategories() {
            yield return new QueryCategory {
                Title = "Block",
                Description = "Queries related to blocks",
                AddTemplates = CreateBlockTemplateQueries()
            };

            yield return new QueryCategory {
                Title = "Transaction",
                Description = "Queries related to transactions, inputs and outputs",
                AddTemplates = CreateTransactionTemplateQueries()
            };

            yield return new QueryCategory {
                Title = "Scripts",
                Description = "Queries related to scripts, script instructions",
                AddTemplates = CreateScriptTemplateQueries()
            };

            yield return new QueryCategory
            {
                Title = "Addresses",
                Description = "Queries related to address deposits, withdrawals, balances, etc",
                AddTemplates = CreateAddressTemplateQueries()
            };
	        yield return new QueryCategory {
		        Title = "Interesting",
		        Description = "Interesting and fun queries",
		        AddTemplates = CreateInterestingQueries()
	        };

        }


        protected virtual IEnumerable<TemplateQuery> CreateBlockTemplateQueries() {
            yield return new TemplateQuery {
                Title = "All Blocks",
                Description = "Selects all the blocks from the blockchain",
                MSSQL = "SELECT * FROM Block ORDER BY ID",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Latest Blocks",
                Description = "Selects the most recent blocks from the blockchain",
                MSSQL = "SELECT * FROM Block ORDER BY Height DESC",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Generous Blocks",
                Description = "Selects the blocks with largest fees",
                MSSQL = "SELECT ID, FeesBTC, Hash FROM Block ORDER BY FeesBTC DESC",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Genesis Block",
                Description = "Gets the genesis block, the very first block in the blockchain",
                MSSQL = "SELECT * FROM Block WHERE PreviousBlockHash = 0x0000000000000000000000000000000000000000000000000000000000000000",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Orphan Count",
                Description = "Counts the number of orphaned blocks this node has encountered",
                MSSQL = "SELECT COUNT(*) AS NumOrphans FROM Block where BranchID <> 1",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Orphaned Blocks",
                Description = "Select all the orphaned blocks this node has encountered",
                MSSQL = "SELECT * FROM Block WHERE BranchID <> 1 ",
                Active = true
            };


        }

        protected virtual IEnumerable<TemplateQuery> CreateTransactionTemplateQueries() {
            yield return new TemplateQuery {
                Title = "All Transactions",
                Description = "Selects all the transactions from the blockchain",
                MSSQL = "SELECT * FROM [Transaction]",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Transaction by ID",
                Description = "Select a transaction by its database ID",
                MSSQL = "SELECT * FROM [Transaction] WHERE ID = 343 -- Set ID here",
                Active = true
            };

            yield return new TemplateQuery
            {
                Title = "Block Transactions (by block database ID)",
                Description = "Select the transactions belonging to a block by the block database id",
                MSSQL =
@"SELECT
    T.*
FROM
    [Transaction] T INNER JOIN
    Block B ON T.BlockID = B.ID
WHERE
    B.ID = 1000 -- Block database ID",
                Active = true
            };


            yield return new TemplateQuery {
                Title = "Transaction by TXID",
                Description = "Select a transaction by it's blockchain ID (TXID)",
                MSSQL =
                    @"SELECT * FROM [Transaction]
WHERE TXID = 0xDAC1077FF75C677E3E0E7F9028DA813D63F8A57C938F5A8934BB5E61A320ECDC -- Set TXID here (prefix with 0x)",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Block Transactions (by block height)",
                Description = "Select the transactions belonging to a block by the block's height",
                MSSQL =
                    @"SELECT
    T.*
FROM
    [Transaction] T INNER JOIN
    Block B ON T.BlockID = B.ID
WHERE
    B.BranchID = 1 AND
    B.Height = 100000   -- Bock height",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "Block Transactions (by block hash)",
                Description = "Select the transactions belonging to a block by the block's hash",
                MSSQL =
                    @"SELECT
    T.*
FROM
    [Transaction] T INNER JOIN
    Block B ON T.BlockID = B.ID
WHERE
    B.Hash = 0x00000000000000000570DA410CA91C6E27E92B5E8744C999A93A9663538F312A -- Block hash here (prefix with 0x)",
                Active = true
            };


            yield return new TemplateQuery {
                Title = "Generous Transactions",
                Description = "Select the transactions with highest fee (slow query)",
                MSSQL = @"SELECT * FROM [Transaction] ORDER BY FeeBTC DESC",
                Active = true
            };

        }

        protected virtual IEnumerable<TemplateQuery> CreateScriptTemplateQueries() {
            yield return new TemplateQuery {
                Title = "View Script",
                Description = "Views a script by the script database ID",
                MSSQL =
                    @"SELECT
    O.[Name] Name,
    SI.[DataLE] Data,
    TXT.[en] Txt
FROM
    ScriptInstruction SI INNER JOIN
    OpCode O ON SI.OpCode = O.ID INNER JOIN
    [Text] TXT ON O.[Description] = TXT.Name
WHERE
    SI.ScriptID = 243021301 -- Script ID (database)",
                Active = true
            };


            yield return new TemplateQuery {
                Title = "View Input Script",
                Description = "Views a transactions input script by TXID and input index",
                MSSQL =
                    @"SELECT
    O.[Name] Name,
    SI.[DataLE]
    Data,
    TXT.[en] Txt
FROM
    [Transaction] T INNER JOIN
    TransactionInput TXI ON T.ID = TXI.TransactionID INNER JOIN
    Script S ON S.ID = TXI.ScriptID INNER JOIN
    ScriptInstruction SI ON S.ID = SI.ScriptID INNER JOIN
    OpCode O ON SI.OpCode = O.ID INNER JOIN
    [Text] TXT ON O.[Description] = TXT.Name
WHERE
    T.TXID = 0x67F222EF921D8021EB740A87AE0A52637568E96D7FCC2DA78EE6FB167B08FCF9 AND -- TXID (prefix with 0x)
    TXI.[Index] = 0     -- Input index",
                Active = true
            };

            yield return new TemplateQuery {
                Title = "View Input Script",
                Description = "Views a transactions input script by block height, transaction index and input index",
                MSSQL =
                    @"SELECT
    O.[Name] Name,
    SI.[DataLE] Data,
    TXT.[en] Txt
FROM
    [Transaction] T INNER JOIN
    TransactionInput TXI ON T.ID = TXI.TransactionID INNER JOIN
    Script S ON S.ID = TXI.ScriptID INNER JOIN
    ScriptInstruction SI ON S.ID = SI.ScriptID INNER JOIN
    OpCode O ON SI.OpCode = O.ID INNER JOIN
    [Text] TXT ON O.[Description] = TXT.Name INNER JOIN
    Block B ON T.BlockID = B.ID
WHERE
    B.Height = 416079 AND -- Block height
    T.[Index] = 0 AND     -- Transaction index
    TXI.[Index] = 0       -- Input index",
                Active = true,
            };




            yield return new TemplateQuery {
                Title = "View Output Script",
                Description = "Views a transactions output script by transaction database id and output index",
                MSSQL =
                    @"SELECT
    O.[Name] Name,
    SI.[DataLE] Data,
    TXT.[en] Txt
FROM
    [Transaction] T INNER JOIN
    TransactionOutput TXO ON T.ID = TXO.TransactionID INNER JOIN
    Script S ON S.ID = TXO.ScriptID INNER JOIN
    ScriptInstruction SI ON S.ID = SI.ScriptID INNER JOIN
    OpCode O ON SI.OpCode = O.ID INNER JOIN
    [Text] TXT ON O.[Description] = TXT.Name
WHERE
    T.ID = 135510789 AND -- Transaction ID (database)
    TXO.[Index] = 0     -- Output index",
                Active = true,
            };


            yield return new TemplateQuery {
                Title = "View Output Script",
                Description = "Views a transactions output script by transaction TXID and output index",
                MSSQL =
                    @"SELECT
    O.[Name] Name,
    SI.[DataLE]
    Data,
    TXT.[en] Txt
FROM
    [Transaction] T INNER JOIN
    TransactionOutput TXO ON T.ID = TXO.TransactionID INNER JOIN
    Script S ON S.ID = TXO.ScriptID INNER JOIN
    ScriptInstruction SI ON S.ID = SI.ScriptID INNER JOIN
    OpCode O ON SI.OpCode = O.ID INNER JOIN
    [Text] TXT ON O.[Description] = TXT.Name
WHERE
    T.TXID = 0x67F222EF921D8021EB740A87AE0A52637568E96D7FCC2DA78EE6FB167B08FCF9 AND -- TXID (prefix with 0x)
    TXO.[Index] = 0     -- Output index",
                Active = true,
            };


            yield return new TemplateQuery {
                Title = "View Output Script",
                Description = "Views a transactions output script by block height, transaction index and output index",
                MSSQL =
                    @"SELECT
    O.[Name] Name,
    SI.[DataLE] Data,
    TXT.[en] Txt
FROM
    [Transaction] T INNER JOIN
    TransactionOutput TXO ON T.ID = TXO.TransactionID INNER JOIN
    Script S ON S.ID = TXO.ScriptID INNER JOIN
    ScriptInstruction SI ON S.ID = SI.ScriptID INNER JOIN
    OpCode O ON SI.OpCode = O.ID INNER JOIN
    [Text] TXT ON O.[Description] = TXT.Name INNER JOIN
    Block B ON T.BlockID = B.ID
WHERE
    B.Height = 416079 AND   -- Block height
    T.[Index] = 0 AND       -- Transaction index
    TXO.[Index] = 0         -- Output index",
                Active = true,
            };

        }
        protected virtual IEnumerable<TemplateQuery> CreateAddressTemplateQueries()
        {
            yield return new TemplateQuery
            {
                Title = "Deposits",
                Description = "Views all deposits to an address",
                MSSQL =
@"SELECT		
	B.TimeStampUtc AS [DateTime],		
	TXO.Value / 100000000.0 AS AmountBTC,	
	B.ID AS BlockID,
	B.Height AS BlockHeight,
	TX.ID AS TransactionID,
	TX.TXID AS TransactionTXID,
	TXO.[Index] AS OutputIndex,
	TXO.ID AS OutputID
FROM
	TransactionOutput TXO INNER JOIN
	[Transaction] TX ON TXO.TransactionID = TX.ID INNER JOIN
	Block B ON TX.BlockID = B.ID 
WHERE
	TXO.ToAddress = '1ByuFFiYvgKFkAFuY1bxHTj8tqSsCBaF2B' AND   -- Address deposited into
	B.BranchID = 1
",
                Active = true
            };


            yield return new TemplateQuery
            {
                Title = "Withdrawals",
                Description = "Views all withdrawals from an address",
                MSSQL =
@"SELECT
	B.TimeStampUtc AS [DateTime],
	TXI.Value / 100000000.0 AS AmountBTC,
	B.ID AS BlockID,
	B.Height AS BlockHeight,
	TX.ID AS TransactionID,
	TX.TXID AS TransactionTXID,
	TXI.[Index] AS InputIndex
FROM
	TransactionInput TXI INNER JOIN
	TransactionOutput TXO ON TXI.TransactionOutputID = TXO.ID INNER JOIN
	[Transaction] TX ON TXI.TransactionID = TX.ID INNER JOIN
	Block B ON TX.BlockID = B.ID
WHERE
	TXO.ToAddress = '1ByuFFiYvgKFkAFuY1bxHTj8tqSsCBaF2B' AND -- Address withdrawn from
	B.BranchID = 1 ",
                Active = true
            };


        }

	    protected virtual IEnumerable<TemplateQuery> CreateInterestingQueries() {
			yield return new TemplateQuery {
				Title = "Time Differences",
				Description = "View the time differences between blocks in order",
				MSSQL =
@"-- Returns the duration between consequtive blocks in descending order
-- INTERESTING: when ordering by ascending, it can be seen future blocks were created BEFORE past blocks
SELECT 
	B1.Height AS FromBlock, 
	B2.Height AS ToBlock, 
	B1.TimeStampUtc AS FromTime,
	B2.TimeStampUtc AS ToTime,
	IIF(B2.TimeStampUTC < B1.TimeStampUTC, '-', '') +  RIGHT('00' + CONVERT(varchar, (DATEDIFF(SECOND, B1.TimeStampUTC, B2.TimeStampUTC) / 86400)), 2) + ':' + CONVERT(varchar, DATEADD(ss, DATEDIFF(SECOND, IIF(B1.TimeStampUTC < B2.TimeStampUTC, B1.TimeStampUTC, B2.TimeStampUTC), IIF(B2.TimeStampUTC > B1.TimeStampUTC, B2.TimeStampUTC, B1.TimeStampUTC)), 0), 108) AS Duration_DDHHMMSS,
	DATEDIFF(SECOND, B1.TimeStampUTC, B2.TimeStampUTC) as DurationSeconds
FROM 
	Block B1 INNER JOIN
	Block B2 ON B1.Height = B2.Height - 1
WHERE
	B1.BranchID = 1 AND  -- Ignore orphaned blocks
	B2.BranchID = 1      -- Ignore orphaned blocks
ORDER BY
	DurationSeconds DESC  -- Change between 'ASC' to 'DESC' to order differently
",
				Active = true
			};
		}

    }
}