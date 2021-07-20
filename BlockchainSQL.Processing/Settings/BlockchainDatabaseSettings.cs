using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using System.ComponentModel;

namespace BlockchainSQL.Processing {
	public class BlockchainDatabaseSettings : SettingsObject {

		public DBMSType DBMSType { get; set; } = DBMSType.SQLServer;

		[Encrypted]
		public string ConnectionString { get; set; } = string.Empty;
	}
}