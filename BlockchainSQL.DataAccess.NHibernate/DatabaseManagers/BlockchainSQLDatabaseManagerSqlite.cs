using BlockchainSQL.DataAccess.NHibernate.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.NHibernate;
using Sphere10.Framework.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlockchainSQL.DataAccess.NHibernate {
	public class BlockchainSQLDatabaseManagerSqlite : NHDatabaseManagerBase {

		public BlockchainSQLDatabaseManagerSqlite()
			: base(new SqliteDatabaseManager()) {
		}

		protected override IDataGenerator CreateDataGenerator(ISessionFactory sessionFactory, string databaseName, DatabaseGenerationDataPolicy policy)
			=> policy switch {
				DatabaseGenerationDataPolicy.NoData => new EmptyDataGenerator(),
				_ => throw new NotSupportedException()
			};

		protected override FluentConfiguration GetFluentConfig(string connectionString)
			=> Fluently
					.Configure()
					.Database(() => {
						string filename, password;
						filename = Tools.Sqlite.GetFilePathFromConnectionString(connectionString, out password);
						if (!string.IsNullOrEmpty(password)) {
							return SQLiteConfiguration.Standard.UsingFileWithPassword(filename, password);
						}
						return SQLiteConfiguration.Standard.UsingFile(filename);
					})
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
			string filename, password;
			filename = Tools.Sqlite.GetFilePathFromConnectionString(connectionString, out password);
			if (File.Exists(filename))
				File.Delete(filename);
		}
	}
}
