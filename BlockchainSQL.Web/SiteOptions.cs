using Sphere10.Framework.Data;

namespace BlockchainSQL.Web {
	public class SiteOptions {

		public bool HasWebDBMS { get; set; }

		public DBMSType WebDBMSType { get; set; }

		public DBMSType BlockchainDBMSType { get; set; }

		public string SMTPServer { get; set; }

		public int SMTPPort { get; set; }

		public string SMTPUsername { get; set; }

		public string SMTPPassword { get; set; }

		public string ContactRecipientEmail { get; set; }
		
		public string ConfigPassword { get; set; }

		public bool HideServer { get; set; } = true;
	}
}
