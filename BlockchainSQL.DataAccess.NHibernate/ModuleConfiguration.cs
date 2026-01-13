// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
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
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BlockchainSQL.DataAccess.NHibernate {
	public class ModuleConfiguration : ModuleConfigurationBase {
		public override void RegisterComponents(IServiceCollection services) {
			base.RegisterComponents(services);
			services.AddNamedTransient<IDatabaseManager, BlockchainSQLDatabaseManagerMSSQL>(DBMSType.SQLServer.ToString());
			services.AddNamedTransient<IDatabaseManager, BlockchainSQLDatabaseManagerSqlite>(DBMSType.Sqlite.ToString());
		}
	}
}
