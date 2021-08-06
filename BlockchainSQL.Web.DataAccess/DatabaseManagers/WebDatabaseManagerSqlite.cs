using BlockchainSQL.DataAccess.NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace BlockchainSQL.Web.DataAccess {
	public class WebDatabaseManagerSqlite : NHibernateDatabaseManagerBase {

		public WebDatabaseManagerSqlite()
			: base(new SqliteDatabaseManager()) {
		}

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
					.ConfigureBSQL();

		protected override IDataGenerator CreateDataGenerator(ISessionFactory sessionFactory, string databaseName, DatabaseGenerationDataPolicy policy)
			=> policy switch {
				DatabaseGenerationDataPolicy.NoData => new EmptyDataGenerator(),
				DatabaseGenerationDataPolicy.PrimingData => new PrimingDataGenerator(sessionFactory),
				_ => throw new NotImplementedException(),
			};

		protected override void SetCreateDatabaseConfiguration(string connectionString, string databaseName, Configuration configuration) {
			string filename, password;
			filename = Tools.Sqlite.GetFilePathFromConnectionString(connectionString, out password);
			if (File.Exists(filename))
				File.Delete(filename);

		}
	}
}
