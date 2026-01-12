using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using Sphere10.Framework.Data;
using Sphere10.Framework;
using System.Threading.Tasks;
using BlockchainSQL.Web.DataAccess;
using System.Data.SqlClient;
using Sphere10.Framework.Data.NHibernate;


namespace BlockchainSQL.Web.DataAccess {

	public class WebDatabaseManagerMSSQL : NHDatabaseManagerBase {

		public WebDatabaseManagerMSSQL()
			: base(new MSSQLDatabaseManager()) {
		}

		protected override IDataGenerator CreateDataGenerator(ISessionFactory sessionFactory, string databaseName, DatabaseGenerationDataPolicy policy) 
			=> policy switch {
			DatabaseGenerationDataPolicy.NoData => new EmptyDataGenerator(),
			DatabaseGenerationDataPolicy.PrimingData => new PrimingDataGenerator(sessionFactory),
			_ => throw new NotSupportedException()
		};

		protected override FluentConfiguration GetFluentConfig(string connectionString)
			=>	Fluently
					.Configure()
					.Database(() => MsSqlConfiguration
						.MsSql2008.Dialect<ExtendedMssqlDialect>()
						.Driver<ExtendedSql2008ClientDriver>()
						.AdoNetBatchSize(1000)
						.ConnectionString(connectionString)
						//.FormatSql()
						//.ShowSql()
						.UseOuterJoin()
						.UseReflectionOptimizer()
					)
					.ConfigureBSQL();


		protected override void SetCreateDatabaseConfiguration(string connectionString, string databaseName, Configuration configuration) {
			var schemaExport = new SchemaExport(configuration);
			//schemaExport.Drop(false, true);
			schemaExport.Create(false, true);
		}



	}
}
