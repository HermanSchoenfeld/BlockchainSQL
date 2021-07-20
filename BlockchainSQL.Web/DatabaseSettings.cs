
using Sphere10.Framework.Application;
using System.ComponentModel;

namespace BlockchainSQL.Web {
	public class DatabaseSettings : SettingsObject {

		public string WebDatabaseConnectionString { get; set; } = string.Empty;

		public string BlockchainDatabaseConnectionString { get; set; } = string.Empty;
	}
}
