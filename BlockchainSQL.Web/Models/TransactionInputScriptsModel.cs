using BlockchainSQL.DataObjects;

namespace BlockchainSQL.Web.Models {
	public class TransactionInputScriptModel {

		public ScriptSummary ScriptSig { get; set; }

		public ScriptSummary Witness { get; set; }
	}
}
