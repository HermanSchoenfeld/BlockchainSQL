using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess.NHibernate;
using NHibernate;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Web.DataAccess {
    public static class WebDatabase {

        public static IDatabaseGenerator NewDatabaseGenerator(DBMSType dbmsType) {
            switch (dbmsType) {
                case DBMSType.SQLServer:
                    return  new WebDatabaseGeneratorMSSQL();
                case DBMSType.Sqlite:
                    return new  WebDatabaseGeneratorSqlite();
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
            var dbGen = (NHibernateDatabaseGeneratorBase)NewDatabaseGenerator(dbmsType);
            return dbGen.OpenDatabase(connectionString);
        }
    }
}
