using Hydrogen.Web.AspNetCore;

namespace BlockchainSQL.Web.Models {
	public class ConfigureDatabaseFormInput : FormModelBase {
		public override string FormName => "ConfigureDatabases";

		public ConfigureBlockchainDbFormModel BlockchainDbModel { get; init; } = new ();

		public ConfigureWebDatabaseFormModel WebDbModel { get; init; } = new();
	}
}