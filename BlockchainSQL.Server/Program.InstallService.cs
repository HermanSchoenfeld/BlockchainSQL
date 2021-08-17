using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows.Forms;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Server {
	static class Program {

		private static CommandLineParameters Arguments = new CommandLineParameters() {
			Header = new[] {
				"{ProductName} {CurrentVersion} ({ProductUrl})",
				"{CopyrightNotice}"
			},

			Footer = new [] {
				"{CompanyUrl}"
			},

			Commands = new CommandLineCommand[] {
				new("install", "Installs the BlockchainSQL Service", 
					new CommandLineParameter[] {
						new("path", "Path where to install the BlockchainSQL Server Windows Service", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("start", "Path to install windows service"),
						new("dbms", $"DBMS that will host the BlockchainSQL database. Options are: {DBMSType.SQLServer}", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("db", $"Database connection string for BlockchainSQL database", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("ip", $"IP address of the Bitcoin Core node", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("port", "Port of the Bitcoin Core network protocol (default 8333)", CommandLineParameterOptions.Optional  | CommandLineParameterOptions.RequiresValue),
						new("poll", "Number of seconds between polling node for new blocks (default 10)", CommandLineParameterOptions.Optional  | CommandLineParameterOptions.RequiresValue),
						new("store_scripts", "Scanner should store scripts resulting in very large databaes (default true). Options: True, False", CommandLineParameterOptions.Optional),
						new("maxmem", "Maximum megabytes of memory to consume during scanning (default 500)", CommandLineParameterOptions.Optional  | CommandLineParameterOptions.RequiresValue),
						new("web", "Whether or not to install web application explorer (default 500)", CommandLineParameterOptions.Optional),
						new("web_port", "Whether or not to install web application explorer (default 500)", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_dbms", $"DBMS that will host the BlockchainSQL web database. Options are: {new[] {DBMSType.SQLServer, DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile}.ToDelimittedString(", ")}", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_db", "Database connection string for BlockchainSQL web database", CommandLineParameterOptions.Optional),
					}
				)
			},
			Options = CommandLineArgumentOptions.DoubleDash | CommandLineArgumentOptions.PrintHelpOnH
		};


		private static void RunAsInstallServiceCommand(CommandLineResults commandLine) {
			var path = commandLine.GetSingleArgumentValue("path");
			var start = commandLine.HasArgument("start");
			var dbms = commandLine.GetEnumArgument<DBMSType>("dbms");
			if (!dbms.IsIn(DBMSType.SQLServer))
				throw new InvalidOperationException($"Only SQLServer supported for BlockchainSQL database");
			var connString = commandLine.GetSingleArgumentValue("db");
			if (Tools.MSSQL.TestConnectionString(connString)) {
				throw new InvalidOperationException($"Unable to connect to BlockchainSQL database: {connString}");
			}
			




			/*
					var bsqlDBSettings = GlobalSettings.Get<BlockchainDatabaseSettings>();
					bsqlDBSettings.DBMSType = Model.BlockchainDatabaseSettings.DBMSType;
					bsqlDBSettings.ConnectionString = Model.BlockchainDatabaseSettings.ConnectionString;
					bsqlDBSettings.Save();

					var nodeSettings = GlobalSettings.Get<NodeSettings>();
					nodeSettings.IP = Model.NodeSettings.IP;
					nodeSettings.Port = Model.NodeSettings.Port;
					nodeSettings.PollRateSEC = Model.NodeSettings.PollRateSEC;
					nodeSettings.Save();

					var scannerSettings = GlobalSettings.Get<ScannerSettings>();
					scannerSettings.StoreScriptData = Model.ScannerSettings.StoreScriptData;
					scannerSettings.MaxMemoryBufferSizeMB = Model.ScannerSettings.MaxMemoryBufferSizeMB;
					scannerSettings.Save();

					var webSettings = GlobalSettings.Get<WebSettings>();
					webSettings.Enabled = Model.WebSettings.Enabled;
					webSettings.Port = Model.WebSettings.Port;
					webSettings.DBMSType = Model.WebSettings.DBMSType;
					webSettings.DatabaseConnectionString = Model.WebSettings.DatabaseConnectionString;
					webSettings.Save();
			
			try {
				Debug.Assert(arguments != null);
				Debug.Assert(arguments.Length > 0);
				Debug.Assert(arguments[0].ToUpperInvariant() == "-INSTALL");
				if (arguments.Length != 1) {
					PrintUsage("Invalid number arguments for service installation.");
					Environment.Exit(-1);
				}
				var path = arguments[1];

				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);

				ServiceManager.InstallService(path).Wait();
				Console.WriteLine("Service Installed & Started");
				Environment.Exit(0);
			} catch (Exception error) {
				Console.WriteLine(error.ToDisplayString());
				System.Threading.Thread.Sleep(200); // give time for output to flush to parent process
				Environment.Exit(-1);
			}

			*/
		}


	}
}
