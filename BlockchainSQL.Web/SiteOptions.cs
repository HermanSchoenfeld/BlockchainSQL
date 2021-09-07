using Sphere10.Framework.Data;

namespace BlockchainSQL.Web {
	public class SiteOptions {

		public bool ShowProductPage { get; set; }

		public bool ShowCompanyAboutPage { get; set; }

		public string AdminUsername { get; set; }

		public string AdminPassword { get; set; }

		public string SMTPServer { get; set; }

		public int SMTPPort { get; set; }

		public string SMTPUsername { get; set; }

		public string SMTPPassword { get; set; }

		public string ContactRecipientEmail { get; set; }

		public string ContactFromEmail { get; set; }

	}
}
