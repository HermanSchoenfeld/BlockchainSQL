using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Sphere10.Framework.Data;
using BlockchainSQL.DataAccess.NHibernate.Mappings;
using Sphere10.Framework.Data.NHibernate;

namespace BlockchainSQL.DataAccess.NHibernate {

	public class BlockhainSQLDatabaseManagerMSSQL : NHibernateDatabaseManagerBase {

		public BlockhainSQLDatabaseManagerMSSQL()
			: base(new MSSQLDatabaseManager()) {
		}

		protected override IDataGenerator CreateDataGenerator(ISessionFactory sessionFactory, string databaseName, DatabaseGenerationDataPolicy policy)
			=> policy switch {
				DatabaseGenerationDataPolicy.NoData => new EmptyDataGenerator(),
				DatabaseGenerationDataPolicy.PrimingData => new PrimingDataGenerator(sessionFactory, databaseName),
				_ => throw new NotSupportedException()
			};

		protected override void OnDatabaseCreated(string connectionString, bool createdEmptyDatabase) {
			if (createdEmptyDatabase) {
				return;
			}

			// MSSQL specific optimization
			var mssqlDAC = new MSSQLDAC(connectionString);

			//Optimize Script.ScriptBytesLE (since removed)
			//mssqlDAC.ExecuteNonQuery("sp_tableoption N'Script', 'large value types out of row', 'ON'");

			// Optomize ScriptInstruction.DataLE
			mssqlDAC.ExecuteNonQuery("sp_tableoption N'ScriptInstruction', 'large value types out of row', 'ON'");

		}

		protected override FluentConfiguration GetFluentConfig(string connectionString)
			=> Fluently
					.Configure()
					.Database(() =>
						MsSqlConfiguration
							.MsSql2008.Dialect<ExtendedMssqlDialect>()
							.Driver<ExtendedSql2008ClientDriver>()
#if OSX
							.AdoNetBatchSize(0) 
#endif
							.AdoNetBatchSize(1000)
							.ConnectionString(connectionString)
							//.FormatSql()
							//.ShowSql()
							.UseOuterJoin()
							.UseReflectionOptimizer()
					)
					.Mappings(c => c.FluentMappings.AddFromAssemblyOf<TextMap>())
					.Mappings(c => c.FluentMappings.Conventions.Add<CoreConventions>())
					//.Mappings(c => c.FluentMappings.Conventions.Add<AnsiStringConvention>())
					.Mappings(c => c.FluentMappings.Conventions.Add<ForeignKeyConventions>())
					.Mappings(c => c.FluentMappings.Conventions.Add<StringColumnLengthConvention>())
					.Mappings(c => c.FluentMappings.Conventions.Add<BinaryColumnLengthConvention>())
					//.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
					//.ExposeConfiguration(c => c.SetInterceptor(new LoggingInterceptor()))
					.ExposeConfiguration(c => c.SetProperty(global::NHibernate.Cfg.Environment.Hbm2ddlKeyWords, "auto-quote"))
					.ExposeConfiguration(SchemaMetadataUpdater.QuoteTableAndColumns);


		protected override void SetCreateDatabaseConfiguration(string connectionString, string databaseName, Configuration configuration) {
			var schemaExport = new SchemaExport(configuration);
			//schemaExport.Drop(false, true);
			schemaExport.Create(false, true);
		}


	}
}
