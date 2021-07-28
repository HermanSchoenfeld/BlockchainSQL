using System.Collections.Generic;
using System.Data;
using System.Linq;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess {
	public abstract class DBVendorSpecificImplementationBase : IDBVendorSpecificImplementation {

		public abstract bool HasDisabledApplicationIndexes(IDAC dac);

		public abstract void EnableAllApplicationIndexes(IDAC dac);

		public abstract void DisableAllApplicationIndexes(IDAC dac);

		public abstract void CleanupDatabase(IDAC dac);

		public abstract DataTable ExecuteUserSQL(IDAC dac, string userSql, int page, int pageSize, string orderByHint,
		                                         out int pageCount);

		public abstract IEnumerable<StatementLine> GetStatementLines(IDAC dac, string address);

		public virtual bool IsValidSchema(IDAC dac) 
			=> dac.GetSchema()
				.Tables
				.Select(t => t.Name)
				.ContainsAll(
					"Branch",
					"Block",
					"Transaction",
					"TransactionInput",
					"TransactionOutput",
					"Script",
					"ScriptInstruction",
					"Text"
				);
	}
}

/*


Branch

Block

Transaction

TransactionInput

TransactionOutput

Script

ScriptInstruction

Text

*/