using System;
using System.Data;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using NHibernate;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web.Code {
	public static class DatabaseManager {
		public const string NHSessionFactoryKey = "NHSessionFactory";
		public const string BlockchainSchemaKey = "SchemaKey";
		public const string DataCacheKey = "DataCacheKey";
		public const string BlockchainSchemaQuery =

		#region Query

			@"SELECT
    DISTINCT

    T.name AS[Table Name],
    C.name AS[Column Name],
    C.column_id AS[Column Position],
    UPPER(TY.name)AS[Column Type],
    --C.max_length AS Length,
    --TY.precision as Precision,
    --TY.scale as Scale,
    CASE WHEN A.is_primary_key = 1 THEN 'Y' ELSE NULL END AS[Primary Key],
    CASE WHEN A.is_primary_key = 1 OR A.is_unique = 1 OR A.is_unique_constraint = 1 THEN 'Y' ELSE NULL END AS[Unique],
    CASE WHEN(A.is_primary_key = 0 AND A.is_unique = 0 AND A.is_unique_constraint = 0) OR(A.is_primary_key = 1) OR(A.is_primary_key = 1 OR A.is_unique = 1 OR A.is_unique_constraint = 1) THEN 'Y' ELSE NULL END AS[Indexed],
    CASE WHEN C.is_nullable = 1 THEN 'Y' ELSE NULL END AS[Nullable],
    CASE WHEN B.ReferenceTableName IS NOT NULL THEN B.ReferenceTableName++ '.' + B.ReferenceColumnName ELSE NULL END AS[Foreign Key]

FROM

    sys.tables T LEFT JOIN

    sys.columns C ON T.object_id = C.object_id LEFT JOIN

    sys.types TY ON C.user_type_id = TY.user_type_id LEFT JOIN(
        SELECT

            IC.object_id,
            IC.column_id,
            I.name,
            I.is_primary_key,
            I.is_unique,
            I.is_unique_constraint

        FROM  sys.index_columns IC INNER JOIN

              sys.indexes I on IC.object_id = I.object_id AND IC.index_id = I.index_id
        --WHERE
        --  I.is_unique = 1
    ) A ON T.object_id = A.object_id AND C.column_id = A.column_id LEFT JOIN(
        SELECT

            F.parent_object_id as object_id,
            FC.parent_column_id as column_id,
            F.name AS ForeignKeyName,
            F.delete_referential_action,
            F.update_referential_action,
            OBJECT_NAME(F.referenced_object_id) AS ReferenceTableName,
            COL_NAME(FC.referenced_object_id, FC.referenced_column_id) AS ReferenceColumnName

        FROM

            sys.foreign_keys F INNER JOIN

            sys.foreign_key_columns FC  on FC.constraint_object_id = F.object_id
    ) B ON T.object_id = B.object_id AND C.column_id = B.column_id
ORDER BY

    T.Name, C.column_id";

		#endregion

		
		private static bool _webDbInitialized { get; set; }
		private static bool _blockchainDbInitialized { get; set; }

		static DatabaseManager() {
			Settings = GlobalSettings.Get<DatabaseSettings>();
			GlobalSettings.Provider.SaveSetting(Settings);
		}

		public static DatabaseSettings Settings { get; private set; }


		// TODO: refactor this out
		public static SiteOptions Options { get; private set; }

		public static bool IsValid => _webDbInitialized && _blockchainDbInitialized;

		public static bool IsConfigured =>
			!string.IsNullOrEmpty(Settings.BlockchainDatabaseConnectionString) && !string.IsNullOrEmpty(Settings.WebDatabaseConnectionString);

		public static ISessionFactory NhSessionFactory { get; private set; }

		public static DataTable BlockchainSchema { get; private set; }

		public static DataCache DataCache { get; private set; }

		public static void SetWebDatabaseConnectionString(string webConnectionString) {
			Settings.WebDatabaseConnectionString = webConnectionString;
			InitializeWebAppDb();
			GlobalSettings.Provider.SaveSetting(Settings);
		}

		public static void SetBlockchainDatabaseConnectionString(string connectionString) {
			Settings.BlockchainDatabaseConnectionString = connectionString;
			InitializeBlockchainSqlDb();
			GlobalSettings.Provider.SaveSetting(Settings);
		}

		public static void InitializeDatabases() {
			InitializeWebAppDb();
			InitializeBlockchainSqlDb();
		}

		public static DBBlockchainRepository GetBlockchainRepository() {
			if (!IsConfigured)
				throw new InvalidOperationException("Databases are not configured");
			return new DBBlockchainRepository(Settings.BlockchainDatabaseConnectionString);
		}

		//public static DBBlockchainRepository GetWebRepository() {
		//	if (!IsConfigured)
		//		throw new InvalidOperationException("Databases are not configured");
		//	return new DB DBBlockchainRepository(_databaseSettings.WebDatabaseConnectionString);
		//}

		public static bool IsValidBlockchainDatabase(DBMSType dbmsType, string server, string database, string username, string password, int? port, out string connectionString) {
			var databaseGenerator = BlockchainDatabase.NewDatabaseManager(dbmsType);
			connectionString = databaseGenerator.GenerateConnectionString(server, database, username, password, port);
			return databaseGenerator.DatabaseExists(connectionString);
		}

		public static bool IsValidWebDatabase(DBMSType dbmsType, string server, string database, string username, string password, int? port, out string connectionString) {
			var databaseGenerator = WebDatabase.NewDatabaseGenerator(dbmsType);
			connectionString = databaseGenerator.GenerateConnectionString(server, database, username, password, port);
			return databaseGenerator.DatabaseExists(connectionString);
		}

		public static async Task<bool> GenerateWebDatabase(DBMSType dbmsType, string server, string database, string username, string password, int? port) {
			var databaseGenerator = WebDatabase.NewDatabaseGenerator(dbmsType);
			var connectionString = databaseGenerator.GenerateConnectionString(server, database, username, password, port);
			if (await Task.Run(() => databaseGenerator.DatabaseExists(connectionString)))
				return false;
			else {
				await Task.Run(() => databaseGenerator.CreateEmptyDatabase(connectionString));
				await Task.Run(() => databaseGenerator.CreateApplicationDatabase(connectionString, DatabaseGenerationDataPolicy.PrimingData, database));
			}
			return true;
		}

		private static void InitializeBlockchainSqlDb() {
			var dac = new MSSQLDAC(Settings.BlockchainDatabaseConnectionString);
			var schema = dac.ExecuteQuery(BlockchainSchemaQuery);
			_blockchainDbInitialized = true;
			BlockchainSchema = schema;
		}

		private static void InitializeWebAppDb() {
			var dataCache = new DataCache();

			NhSessionFactory = WebDatabase.CreateSessionFactory(DBMSType.SQLServer, Settings.WebDatabaseConnectionString);
			dataCache.Load(NhSessionFactory);
			DataCache = dataCache;
			_webDbInitialized = true;
		}
	}
}
