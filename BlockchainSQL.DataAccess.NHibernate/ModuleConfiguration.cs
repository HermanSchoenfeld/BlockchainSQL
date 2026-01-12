using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BlockchainSQL.DataAccess.NHibernate {
	public class ModuleConfiguration : ModuleConfigurationBase {
		public override void RegisterComponents(IServiceCollection services) {
			base.RegisterComponents(services);
			services.AddNamedTransient<IDatabaseManager, BlockchainSQLDatabaseManagerMSSQL>(DBMSType.SQLServer.ToString());
			services.AddNamedTransient<IDatabaseManager, BlockchainSQLDatabaseManagerSqlite>(DBMSType.Sqlite.ToString());
		}
	}
}
