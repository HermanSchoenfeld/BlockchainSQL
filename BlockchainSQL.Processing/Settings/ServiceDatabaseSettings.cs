using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
	public class ServiceDatabaseSettings : SettingsObject {

		public DBMSType DBMSType { get; set; } = DBMSType.SQLServer;

		[EncryptedString]
		public string ConnectionString { get; set; } = string.Empty;
	}
}