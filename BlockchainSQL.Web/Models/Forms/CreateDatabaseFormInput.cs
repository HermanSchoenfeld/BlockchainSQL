using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using BlockchainSQL.Web.DataAccess;

namespace BlockchainSQL.Web.Models {

	public class CreateDatabaseFormInput : FormModelBase {

		public CreateDatabaseFormInput() {
			string webAppConfig = AppConfig.WebConnectionString;

			if (!string.IsNullOrEmpty(webAppConfig)) {

				var connString = new SqlConnectionStringBuilder(webAppConfig);
				Server = connString.DataSource;
				Database = connString.InitialCatalog;
				Username = connString.UserID;
				Password = connString.Password;
				Port = 1433;
			}
		}

		public override string FormName => "CreateDatabase";

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

		[DisplayName("Port")]
		public int? Port { get; set; }

		[Required]
		[DisplayName("Overwrite Policy")]

		public DatabaseGenerationAlreadyExistsPolicy OverwritePolicy { get; set; }

		[DataType(DataType.Password)]
		[DisplayName("Config Password")]
		public string ConfigPassword { get; set; }

		public bool Editable { get; private set; }
	}
}