using BlockchainSQL.Web.Code;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using BlockchainSQL.Processing;
using Hydrogen.Application;

namespace BlockchainSQL.Web.Models {
	public class ConfigureBlockchainDbFormModel {

		public ConfigureBlockchainDbFormModel() {
			string blockchainDbConnectionString = GlobalSettings.Get<WebSettings>().BlockchainDatabaseConnectionString;

			if (!string.IsNullOrEmpty(blockchainDbConnectionString)) {
				var connString = new SqlConnectionStringBuilder(blockchainDbConnectionString);

				var source = connString.DataSource.Split(',');
				
				Server = source[0];
				Database = connString.InitialCatalog;
				Username = connString.UserID;
				Password = connString.Password;
				Port = source.Length > 1 ? int.Parse(source[1]) : 1433;
			}
		}

		[Required]
		[DisplayName("Server")]
		public string Server { get; set; }

		[Required]
		[DisplayName("Database")]
		public string Database { get; set; }

		[Required]
		[DisplayName("Username")]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		[DisplayName("Password")]
		public string Password { get; set; }

		[DisplayName("Port")] public int? Port { get; set; } = 1433;
	}
}
