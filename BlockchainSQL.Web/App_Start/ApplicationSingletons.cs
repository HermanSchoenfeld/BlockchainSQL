using System;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using BlockchainSQL.Web.DataAccess;
using NHibernate;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web {
    public static class ApplicationSingletons {
        public const string WebConfigKey = "Config";
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

        private static HttpApplicationState _applicationState;

        public static void Register(HttpApplicationState application) {
            _applicationState = application;
            Config = new WebConfig();
            if (Config.HasWebDBMS) {
                try {
                    NhSessionFactory = WebDatabase.CreateSessionFactory(DBMSType.SQLServer, Config.WebConnectionString);
                    DataCache = new DataCache();
                    DataCache.Load(NhSessionFactory);
                } catch (Exception error) {
                    // log error
                    Config.HasValidWebDBMS = false;
                }

            }
            BlockchainSchema = new MSSQLDAC(Config.BlockchainConnectionString).ExecuteQuery(BlockchainSchemaQuery);
        }

        public static WebConfig Config
        {
            get { return GetVariable<WebConfig>(WebConfigKey); }
            private set { SetVariable(WebConfigKey, value); }
        }

        public static ISessionFactory NhSessionFactory
        {
            get { return GetVariable<ISessionFactory>(NHSessionFactoryKey); }
            private set { SetVariable(NHSessionFactoryKey, value); }
        }

        public static DataTable BlockchainSchema
        {
            get { return GetVariable<DataTable>(BlockchainSchemaKey); }
            private set { SetVariable(BlockchainSchemaKey, value); }
        }

        public static DataCache DataCache
        {
            get { return GetVariable<DataCache>(DataCacheKey); }
            private set { SetVariable(DataCacheKey, value); }
        }

        private static void SetVariable<T>(string key, T variable) {
            _applicationState[key] = variable;
        }

        private static T GetVariable<T>(string key) {
            return (T)_applicationState[key];
        }

    }
}
