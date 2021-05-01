using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Tools;

namespace BlockchainSQL.DataAccess {
	public static class DataAccessFactory {

	    public static ApplicationDAC NewDAC(DBMSType dbmsType, string connectionString, ILogger logger = null) {
			IDAC dbmsDAC;
	        IVendorSpecificImplementation vendorSpecificImplementation;
            switch (dbmsType) {
                case DBMSType.SQLServer:
                    dbmsDAC = new MSSQLDAC(connectionString, logger);
                    vendorSpecificImplementation = new MSSQLVendorSpecificImplementation();
                    break;
                case DBMSType.Sqlite:
                    dbmsDAC = new SqliteDAC(connectionString, logger);
                    vendorSpecificImplementation = new NoOpVendorSpecificImplementation();
                    break;
                default:
                    throw new ApplicationException(string.Format("Unsupported DBMS '{0}'", dbmsType));
            }
            return new ApplicationDAC(dbmsDAC, vendorSpecificImplementation);
	    }

        public static IDatabaseGenerator NewDatabaseGenerator(DBMSType dbmsType) {
            switch (dbmsType) {
                case DBMSType.SQLServer:
                    return (IDatabaseGenerator)Tools.Object.Create("BlockchainSQL.DataAccess.NHibernate.MssqlDatabaseGenerator");
                case DBMSType.Sqlite:
                    return (IDatabaseGenerator)Tools.Object.Create("BlockchainSQL.DataAccess.NHibernate.SqliteDatabaseGenerator");
                default:
                    throw new ApplicationException(string.Format("Unsupported DBMS '{0}'", dbmsType));
            }
        }

        public static IEnumerable<Setting> GenerateDefaultSettings() {
	        yield return new Setting {
	            ID = (int) KnownSettings.StoreScriptData,
	            Name = KnownSettings.StoreScriptData.ToString(),
	            Value = "0",
	        };
            yield return new Setting {
                ID = (int)KnownSettings.MaxMemoryBufferSize,
                Name = KnownSettings.MaxMemoryBufferSize.ToString(),
                Value = "500",
            };
	        yield return new Setting {
	            ID = (int) KnownSettings.NetworkPeer1,
	            Name = KnownSettings.NetworkPeer1.ToString(),
	            Value = null,
	        };
            yield return new Setting {
                ID = (int)KnownSettings.NetworkPeer1Port,
                Name = KnownSettings.NetworkPeer1Port.ToString(),
                Value = null,
            };
            yield return new Setting {
                ID = (int)KnownSettings.NetworkPeer1PollRate,
                Name = KnownSettings.NetworkPeer1PollRate.ToString(),
                Value = "30",
            };

	    }

	}
}
