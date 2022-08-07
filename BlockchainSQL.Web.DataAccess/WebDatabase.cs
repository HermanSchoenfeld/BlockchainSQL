using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Hydrogen.Data;
using Hydrogen.Data.NHibernate;

namespace BlockchainSQL.Web.DataAccess {
    public static class WebDatabase {

        public static IDatabaseManager NewDatabaseGenerator(DBMSType dbmsType) {
            switch (dbmsType) {
                case DBMSType.SQLServer:
                    return  new WebDatabaseManagerMSSQL();
                case DBMSType.Sqlite:
                    return new  WebDatabaseManagerSqlite();
                case DBMSType.Firebird:
                    break;
                case DBMSType.FirebirdFile:
                    break;
                default:
                    break;
            }
            throw new ApplicationException(string.Format("Unsupported DBMS '{0}'", dbmsType));
        }

        public static ISessionFactory CreateSessionFactory(DBMSType dbmsType, string connectionString) {
            var dbGen = (NHibernateDatabaseManagerBase)NewDatabaseGenerator(dbmsType);
            return dbGen.OpenDatabase(connectionString);
        }
    }
}
