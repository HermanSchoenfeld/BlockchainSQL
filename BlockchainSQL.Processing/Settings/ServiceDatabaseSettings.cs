using Hydrogen;
using Hydrogen.Application;
using Hydrogen.Data;

namespace BlockchainSQL.Processing {
	public class ServiceDatabaseSettings : SettingsObject {

		public DBMSType DBMSType { get; set; } = DBMSType.SQLServer;

		[Encrypted]
		public string ConnectionString { get; set; } = string.Empty;
	}
}