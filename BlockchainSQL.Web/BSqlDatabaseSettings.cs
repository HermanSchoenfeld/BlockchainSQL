using Sphere10.Framework.Application;

namespace BlockchainSQL.Web {
	public class BSqlDatabaseSettings : SettingsObject {

		public string WebDatabaseConnectionString { get; set; }

		public string BlockchainDatabaseConnectionString { get; set; } = "Data Source=localhost;Initial Catalog=BlockchainSQL2;User ID=sa;Password=Hamish123!";
	}
}
