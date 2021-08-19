using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Tools;

namespace BlockchainSQL.DataAccess {
	public static class BlockchainDatabase {

	    public static ApplicationDAC NewDAC(DBMSType dbmsType, string connectionString, ILogger logger = null) {
			IDAC dac;
            switch (dbmsType) {
                case DBMSType.SQLServer:
                    dac = new MSSQLDAC(connectionString, logger);
                    break;
                case DBMSType.Sqlite:
                    dac = new SqliteDAC(connectionString, logger);
                    break;
                default:
                    throw new ApplicationException($"Unsupported DBMS '{dbmsType}'");
            }
            return NewDAC(dac, logger);
	    }

	    public static ApplicationDAC NewDAC(IDAC dac, ILogger logger = null) {
		    IDBVendorSpecificImplementation vendorSpecificImplementation;
		    switch (dac.DBMSType) {
				case DBMSType.SQLServer:
					vendorSpecificImplementation = new MSSQLVendorSpecificImplementation();
					break;
				case DBMSType.Sqlite:
					vendorSpecificImplementation = new NoOpVendorSpecificImplementation();
					break;
				default:
					throw new ApplicationException($"Unsupported DBMS '{dac.DBMSType}'");
			}
		    return new ApplicationDAC(dac, vendorSpecificImplementation);
	    }

		public static IDatabaseManager NewDatabaseManager(DBMSType dbmsType) {
            switch (dbmsType) {
                case DBMSType.SQLServer:
                    return (IDatabaseManager)Tools.Object.Create("BlockchainSQL.DataAccess.NHibernate.BlockchainSQLDatabaseManagerMSSQL");
                case DBMSType.Sqlite:
                    return (IDatabaseManager)Tools.Object.Create("BlockchainSQL.DataAccess.NHibernate.BlockchainSQLDatabaseManagerSqlite");
                default:
                    throw new ApplicationException($"Unsupported DBMS '{dbmsType}'");
            }
        }
        

	}
}
