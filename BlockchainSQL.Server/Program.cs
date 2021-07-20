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

namespace BlockchainSQL.Server
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args) {
	        if (!Environment.UserInteractive || args.Any(a => a.ToUpperInvariant() == "-SERVICE")) {
                RunAsService();
            } else if (args.Length == 0) {
                RunAsGUI();
            } else if (args.Length > 0) {
                switch (args[0].ToUpperInvariant()) {
                    case "-INSTALL":
                        RunAsInstallServiceCommand(args);
                        break;
                    case "-UNINSTALL":
                        RunAsUninstallServiceCommand(args);
                        break;
                    default:
                        PrintUsage("Unsupported command");
                        Environment.Exit(-1);
                        break;
                }
            } else {
                PrintUsage();
            }
        }
        
        private static void RunAsGUI() {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                AppDomain.CurrentDomain.UnhandledException += (s, e) => Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", (Exception) e.ExceptionObject));
                Application.ThreadException += (s, e) => Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", e.Exception));
                
                SystemLog.RegisterLogger(new ConsoleLogger());
				SystemLog.RegisterLogger(new FileAppendLogger(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlockchainSQL", "log", "GUI.log")));
				Sphere10Framework.Instance.StartWinFormsApplication<MainForm>();
            } catch (Exception error) {
                Console.WriteLine(error.ToDisplayString());
                ExceptionDialog.Show(error);
                Environment.ExitCode = -1;
            }
        }

        private static void RunAsService() {
            try {
				// load first-run time
				SystemLog.RegisterLogger(new FileAppendLogger(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlockchainSQL", "log", "Service.log")));
				Sphere10Framework.Instance.StartFramework();
                ServiceBase.Run(new BlockchainSQLService());
            } catch (Exception error) {
                Console.WriteLine(error.ToDisplayString());
                Environment.ExitCode = -1;
            }            
        }

        private static void RunAsInstallServiceCommand(string[] arguments) {
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

                Tools.BlockchainSQL.InstallService(path).Wait();
                Console.WriteLine("Service Installed & Started");
                Environment.Exit(0);
            } catch (Exception error) {
                Console.WriteLine(error.ToDisplayString());
                System.Threading.Thread.Sleep(200); // give time for output to flush to parent process
                Environment.Exit(-1);
            }
        }

        private static void RunAsUninstallServiceCommand(string[] arguments) {
            try {
                Debug.Assert(arguments != null);
                Debug.Assert(arguments.Length > 0);
                Debug.Assert(arguments[0].ToUpperInvariant() == "-UNINSTALL");
                if (arguments.Length != 2) {
                    PrintUsage("Invalid number arguments for service uninstallation.");
                    Environment.Exit(-1);
                }
                var path = arguments[1];
                Tools.BlockchainSQL.UninstallService(path).Wait();
                Console.WriteLine("Service Stopped & Uninstalled");
                Environment.Exit(0);
            } catch (Exception error) {
                Console.WriteLine(error.ToDisplayString());
                System.Threading.Thread.Sleep(200); // give time for output to flush to parent process
                Environment.ExitCode = -1;                
            }
        }

        private static void PrintUsage(string errorMessage = null) {
            var usageText =
                @"BlockchainSQL Server\t\tCopyright (c) Sphere 10 Software {0}
Usage: 
    (empty args)                                    Launches GUI application
    -install ""PATH"" ""DBTYPE"" ""CONNSTRING""     Installs BlockchainSQL Service
    -uninstall ""PATH""                             Uninstalls BlockchainSQL Service

Arguments:
    PATH    Full directory path
    DBTYPE  One of [SQLServer, Sqlite, Firebird, FirebirdFile]
    CONNSTRING The connection string used to connect to target database

".FormatWith(DateTime.Now.Year);

            Console.WriteLine(usageText);
            if (!string.IsNullOrWhiteSpace(errorMessage)) {
                Console.WriteLine("ERROR: " + errorMessage);
            }
        }
    }
}
