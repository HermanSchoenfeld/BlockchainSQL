// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Sphere10.Framework.Data;
using Sphere10.Framework.Data.NHibernate;

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
            var dbGen = (NHDatabaseManagerBase)NewDatabaseGenerator(dbmsType);
            return dbGen.OpenDatabase(connectionString);
        }
    }
}
