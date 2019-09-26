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
using System.Threading.Tasks;
using BlockchainSQL.DataAccessLayer.NHibernate;
using Sphere10.Framework.Data;

namespace BlockchainSQL.DataAccess.NHibernate {

    internal class MssqlDatabaseGenerator : DatabaseGeneratorBase {

        public override bool DatabaseExists(string connectionString) {
            var mssqlSB = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            return Tools.MSSQL.Exists(
                    mssqlSB.DataSource,
                    mssqlSB.InitialCatalog,
                    userID: mssqlSB.IntegratedSecurity ? null : mssqlSB.UserID,
                    password: mssqlSB.IntegratedSecurity ? null : mssqlSB.Password,
                    integratedSecurity: mssqlSB.IntegratedSecurity
            );
        }

        public override void DropDatabase(string connectionString) {
            var mssqlSB = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                Tools.MSSQL.DropDatabase(
                    mssqlSB.DataSource,
                    mssqlSB.InitialCatalog,
                    username: mssqlSB.IntegratedSecurity ? null : mssqlSB.UserID,
                    password: mssqlSB.IntegratedSecurity ? null : mssqlSB.Password,
                    useIntegratedSecurity: mssqlSB.IntegratedSecurity
            );
        }

        public override void CreateEmptyDatabase(string connectionString) {
            var mssqlSB = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
                Tools.MSSQL.CreateDatabase(
                    mssqlSB.DataSource,
                    mssqlSB.InitialCatalog,
                    username: mssqlSB.IntegratedSecurity ? null : mssqlSB.UserID,
                    password: mssqlSB.IntegratedSecurity ? null : mssqlSB.Password,
                    useIntegratedSecurity: mssqlSB.IntegratedSecurity
                );
        }

        protected override void OnDatabaseCreated(string connectionString) {
            // MSSQL specific optimization
            var mssqlDAC = new MSSQLDAC(connectionString);

            //Optimize Script.ScriptBytesLE (since removed)
            //mssqlDAC.ExecuteNonQuery("sp_tableoption N'Script', 'large value types out of row', 'ON'");

            // Optomize ScriptInstruction.DataLE
            mssqlDAC.ExecuteNonQuery("sp_tableoption N'ScriptInstruction', 'large value types out of row', 'ON'");

        }

        protected override IPersistenceConfigurer GetPersistenceConfigurer(string connectionString) {
            return
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
                    .UseReflectionOptimizer();
        }

        protected override void ApplyDatabaseSpecificMappingConfiguration(MappingConfiguration mappingConfiguration) {
            mappingConfiguration.FluentMappings.Conventions.Add<BinaryColumnLengthConvention>();
            
        }
    }
}
