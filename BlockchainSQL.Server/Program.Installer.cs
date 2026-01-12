// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows.Forms;
using BlockchainSQL.Processing;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using Sphere10.Framework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BlockchainSQL.Server {
	public static partial class Program {


		private static void RunAsInstallServiceCommand(CommandLineResults installCommand) {
			Sphere10Framework.Instance.StartFramework(configure => 
				configure.AddTransient<IUserInterfaceServices, ConsoleApplicationUserInterfaceServices>()/*,
				Sphere10FrameworkOptions.EnableDrm | Sphere10FrameworkOptions.BackgroundLicenseVerify*/
			);
			
			var path = installCommand.GetSingleArgumentValue("path");
			var start = installCommand.HasArgument("start");
			var dbms = installCommand.GetEnumArgument<DBMSType>("dbms");
			if (!dbms.IsIn(DBMSType.SQLServer))
				throw new InvalidOperationException($"Only SQLServer supported for BlockchainSQL database");
			var db = installCommand.GetSingleArgumentValue("db");
			if (!Tools.MSSQL.TestConnectionString(db)) {
				throw new InvalidOperationException($"Unable to connect to BlockchainSQL database: {db}");
			}
			var ip = Tools.Parser.Parse<IPAddress>(installCommand.GetSingleArgumentValue("ip"));
			var port = Tools.Parser.Parse<int>(installCommand.GetSingleArgumentValueOrDefault("port", "8333"));
			var poll = Tools.Parser.Parse<int>(installCommand.GetSingleArgumentValueOrDefault("poll", "10"));
			var store_scripts = installCommand.HasArgument("store_scripts");
			var maxmem = Tools.Parser.Parse<int>(installCommand.GetSingleArgumentValueOrDefault("maxmem", "500"));
			var web = installCommand.HasArgument("web");
			int web_port = default;
			var web_dbms = DBMSType.SQLServer;
			var web_db = string.Empty;
			var web_bsql_dbms = DBMSType.SQLServer;
			var web_bsql_db = string.Empty;
			if (web) {
				web_port = Tools.Parser.Parse<int>(installCommand.GetSingleArgumentValue("web_port"));
				web_dbms = Tools.Parser.Parse<DBMSType>(installCommand.GetSingleArgumentValue("web_dbms"));
				web_db = installCommand.GetSingleArgumentValue("web_db");
				web_bsql_dbms = installCommand.GetSingleArgumentValueOrDefault("web_bsql_dbms", web_dbms);
				web_bsql_db = installCommand.GetSingleArgumentValueOrDefault("web_bsql_db", web_db);
				// Test connection string for web
			}


			var bsqlDBSettings = GlobalSettings.Get<ServiceDatabaseSettings>();
			bsqlDBSettings.DBMSType = dbms;
			bsqlDBSettings.ConnectionString = db;
			bsqlDBSettings.Validate().ThrowOnFailure();
			bsqlDBSettings.Save();

			var nodeSettings = GlobalSettings.Get<ServiceNodeSettings>();
			nodeSettings.IP = ip.ToString();
			nodeSettings.Port = port;
			nodeSettings.PollRateSEC = poll;
			nodeSettings.Validate().ThrowOnFailure();
			nodeSettings.Save();

			var scannerSettings = GlobalSettings.Get<ServiceScannerSettings>();
			scannerSettings.StoreScriptData = store_scripts;
			scannerSettings.MaxMemoryBufferSizeMB = maxmem;
			scannerSettings.Validate().ThrowOnFailure();
			scannerSettings.Save();

			var webSettings = GlobalSettings.Get<WebSettings>();
			webSettings.Enabled = web;
			webSettings.Port = web_port;
			webSettings.WebDBMSType = web_dbms;
			webSettings.WebDatabaseConnectionString = web_db;
			webSettings.BlockchainDBMSType = web_bsql_dbms;
			webSettings.BlockchainDatabaseConnectionString = web_bsql_db;
			webSettings.Validate().ThrowOnFailure();
			webSettings.Save();

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			ServiceManager.InstallService(path, start).Wait();
			Console.WriteLine("Service Installed");
			if (start)
				Console.WriteLine("Service Started");
		}

	}
}
