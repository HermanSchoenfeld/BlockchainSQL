// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using Microsoft.Extensions.DependencyInjection;
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
                    return Sphere10Framework.Instance.ServiceProvider.GetNamedService<IDatabaseManager>(DBMSType.SQLServer.ToString());
                case DBMSType.Sqlite:
	                return Sphere10Framework.Instance.ServiceProvider.GetNamedService<IDatabaseManager>(DBMSType.Sqlite.ToString());
                default:
                    throw new ApplicationException($"Unsupported DBMS '{dbmsType}'");
            }
        }
        

	}
}
