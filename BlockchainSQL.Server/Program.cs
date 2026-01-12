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
using System.ServiceProcess;
using System.Windows.Forms;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using Sphere10.Framework.Application;
using Sphere10.Framework.CryptoEx;
using Sphere10.Framework.Windows;

namespace BlockchainSQL.Server {
	public static partial class Program {

		private static CommandLineParameters Arguments = new CommandLineParameters() {
			Header = new[] {
				"{ProductName} {ProductVersion} ({ProductUrl})",
				"{CopyrightNotice}"
			},

			Footer = new[] {
				"Developed by {CompanyName} (ABN: {CompanyNumber} Web: {CompanyUrl})"
			},

			Commands = new CommandLineCommand[] {
				new("install", "Installs the BlockchainSQL Server Windows Service",
					new CommandLineParameter[] {
						new("path", "Path where to install the BlockchainSQL Server Windows Service", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("start", "\tPath to install windows service"),
						new("dbms", $"DBMS that will host the blockchain database used by indexing service. Options are: {DBMSType.SQLServer}", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("db", $"\tDatabase connection string to blockchain database used by indexing service", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("ip", $"\tIP address of the Bitcoin Core node", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
						new("port", "Port of the Bitcoin Core network protocol (default 8333)", CommandLineParameterOptions.Optional  | CommandLineParameterOptions.RequiresValue),
						new("poll", "Number of seconds between polling node for new blocks (default 10)", CommandLineParameterOptions.Optional  | CommandLineParameterOptions.RequiresValue),
						new("store_scripts", "Scanner should store scripts resulting in very large databaes (default true). Options: True, False", CommandLineParameterOptions.Optional),
						new("maxmem", "Maximum megabytes of memory to consume during scanning (default 500)", CommandLineParameterOptions.Optional  | CommandLineParameterOptions.RequiresValue),
						new("web", "\tWhether or not to install web application explorer", CommandLineParameterOptions.Optional),
						new("web_port", "Whether or not to install web application explorer (default 5000)", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_dbms", $"DBMS that will host the web database. Options are: {new[] {DBMSType.SQLServer, DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile}.ToDelimittedString(", ")}", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_db", "\tDatabase connection string for web database", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_bsql_dbms", $"DBMS of blockchain database used by web-server (will default to `dbms` argument if omitted)", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_bsql_db", "\tDatabase connection string to blockchain database that web-server will use (will default to `db` argument if omitted)", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
					}
				),
				new("uninstall", "Uninstalls BlockchainSQL Server Windows Service",
					new CommandLineParameter[] {
						new("path", "Path where to install the BlockchainSQL Server Windows Service", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
					}
				),
				new("service", "Run in service mode")
			}
		};

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args) {
			try {
				// Ensure explicit loading of certain dlls for IoC init
				var type = typeof(BlockchainSQL.DataAccess.NHibernate.ModuleConfiguration);
			
				using (new AttachToCommandPromptScope()) {
					var userArgsResult = Arguments.TryParseArguments(args);
					if (userArgsResult.IsFailure) {
						userArgsResult.ErrorMessages.ForEach(Console.WriteLine);
						return;
					}
					var userArgs = userArgsResult.Value;
					if (userArgs.HelpRequested) {
						Arguments.PrintHelp();
						return;
					}
				
					switch (userArgs.SubCommand?.CommandName?.ToUpperInvariant()) {
						case "":
						case null:
							if (Environment.UserInteractive) {
								Sphere10Framework.Instance.Initialized += () => {
									SystemLog.RegisterLogger( Sphere10Framework.Instance.CreateApplicationLogger("gui.log", true));
								};
								RunAsGUI();
							} else {
								Sphere10Framework.Instance.Initialized += () => {
									SystemLog.RegisterLogger(Sphere10Framework.Instance.CreateApplicationLogger("service.log", visibleToAllUsers: true));
								};
								RunAsService();
							}
							break;
						case "INSTALL":
							Sphere10Framework.Instance.Initialized += () => {
								SystemLog.RegisterLogger(Sphere10Framework.Instance.CreateApplicationLogger("installer.log"));
							};
							RunAsInstallServiceCommand(userArgs.SubCommand);
							break;
						case "UNINSTALL":
							Sphere10Framework.Instance.Initialized += () => {
								SystemLog.RegisterLogger(Sphere10Framework.Instance.CreateApplicationLogger("installer.log"));
							};
							RunAsUninstallServiceCommand(userArgs.SubCommand);
							break;
						case "SERVICE":
							Sphere10Framework.Instance.Initialized += () => {
								SystemLog.RegisterLogger(Sphere10Framework.Instance.CreateApplicationLogger("service.log", visibleToAllUsers: true));
							};
							RunAsService();
							break;
					}
					Environment.ExitCode = 0;
				}
			} catch (Exception error) {
				Console.WriteLine(error.ToDiagnosticString());
				System.Threading.Thread.Sleep(200); // give time for output to flush to parent process
				Environment.ExitCode = -1;
			} finally {
				if (Sphere10Framework.Instance.IsStarted)
					Sphere10Framework.Instance.EndFramework();
			}
		}


	}
}
