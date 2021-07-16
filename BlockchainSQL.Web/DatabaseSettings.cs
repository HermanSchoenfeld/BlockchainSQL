
using Sphere10.Framework.Application;
using System.ComponentModel;

namespace BlockchainSQL.Web {
	public class DatabaseSettings : SettingsObject {

		[DefaultValue("")]
		public string WebDatabaseConnectionString { get; set; }

		[DefaultValue("")]
		public string BlockchainDatabaseConnectionString { get; set; }
	}
}
