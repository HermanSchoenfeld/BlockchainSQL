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

namespace BlockchainSQL.Server {
	public static partial class Program {

		private static CommandLineParameters Arguments = new CommandLineParameters() {
			Header = new[] {
				"{ProductName} {CurrentVersion} ({ProductUrl})",
				"{CopyrightNotice}"
			},

			Footer = new[] {
				"{CompanyUrl}"
			},

			Commands = new CommandLineCommand[] {
				new("install", "Installs the BlockchainSQL Server Windows Service",
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
						new("web", "Whether or not to install web application explorer", CommandLineParameterOptions.Optional),
						new("web_port", "Whether or not to install web application explorer (default 5000)", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_dbms", $"DBMS that will host the BlockchainSQL web database. Options are: {new[] {DBMSType.SQLServer, DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile}.ToDelimittedString(", ")}", CommandLineParameterOptions.Optional | CommandLineParameterOptions.RequiresValue),
						new("web_db", "Database connection string for BlockchainSQL web database", CommandLineParameterOptions.Optional),
					}
				),
				new("uninstall", "Uninstalls BlockchainSQL Server Windows Service",
					new CommandLineParameter[] {
						new("path", "Path where to install the BlockchainSQL Server Windows Service", CommandLineParameterOptions.Mandatory | CommandLineParameterOptions.RequiresValue),
					}
				),
				new("service", "Run in service mode")
			},
			Options = CommandLineArgumentOptions.DoubleDash | CommandLineArgumentOptions.PrintHelpOnH
		};

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args) {
			try {
				var userArgsResult = Arguments.TryParseArguments(args);
				if (userArgsResult.Failure) {
					userArgsResult.ErrorMessages.ForEach(Console.WriteLine);
					return;
				}
				var userArgs = userArgsResult.Value;
				if (userArgs.HelpRequested) {
					Arguments.PrintHelp();
					return;
				}

				switch (userArgs.CommandName?.ToUpperInvariant()) {
					case "":
					case null:
						if (Environment.UserInteractive)
							RunAsGUI();
						else
							RunAsService();
						break;
					case "INSTALL":
						RunAsInstallServiceCommand(userArgs.SubCommand);
						break;
					case "UNINSTALL":
						RunAsUninstallServiceCommand(userArgs.SubCommand);
						break;
					case "SERVICE":
						RunAsService();
						break;
				}
				Environment.ExitCode = 0;
			} catch (Exception error) {
				Console.WriteLine(error.ToDiagnosticString());
				System.Threading.Thread.Sleep(200); // give time for output to flush to parent process
				Environment.ExitCode = -1;
			}
		}


	}
}
