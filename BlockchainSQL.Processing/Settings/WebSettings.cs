using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using System.ComponentModel;

namespace BlockchainSQL.Processing {
	public class WebSettings : SettingsObject {
		public bool Enabled { get; set; } = true;

		public int Port { get; set; } = 5000;

		public DBMSType DBMSType { get; set; } = DBMSType.Sqlite;

		[Encrypted]
		public string DatabaseConnectionString { get; set; } = "";
	}

}