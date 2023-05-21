using Hydrogen;
using Hydrogen.Application;
using Hydrogen.Data;
using System.ComponentModel;

namespace BlockchainSQL.Processing {
	public class WebSettings : SettingsObject {
		public bool Enabled { get; set; } = true;

		public int Port { get; set; } = 5000;

		public DBMSType WebDBMSType { get; set; } = DBMSType.SQLServer;

		[EncryptedString]
		public string WebDatabaseConnectionString { get; set; } = "";


		public DBMSType BlockchainDBMSType { get; set; } = DBMSType.SQLServer;

		[EncryptedString]
		public string BlockchainDatabaseConnectionString { get; set; } = "";

		public bool SaveQueries { get; set; } = true;

	}
}


