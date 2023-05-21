using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows.Forms;
using BlockchainSQL.Processing;
using Hydrogen;
using Hydrogen.Data;
using Hydrogen.Windows.Forms;
using Hydrogen.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BlockchainSQL.Server {
	public static partial class Program {

		public static void RunAsUninstallServiceCommand(CommandLineResults uninstallCommand) {
			HydrogenFramework.Instance.StartFramework(configure => 
					configure.AddTransient<IUserInterfaceServices, ConsoleApplicationUserInterfaceServices>(),
				HydrogenFrameworkOptions.EnableDrm | HydrogenFrameworkOptions.BackgroundLicenseVerify
			);
			var path = uninstallCommand.GetSingleArgumentValue("path");
			ServiceManager.UninstallService(path).Wait();
			Console.WriteLine("Service Stopped & Uninstalled");
		}

	}
}
