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

namespace BlockchainSQL.Server {
	public static partial class Program {


		public static void RunAsUninstallServiceCommand(CommandLineResults uninstallCommand) {
			var path = uninstallCommand.GetSingleArgumentValue("path");
			ServiceManager.UninstallService(path).Wait();
			Console.WriteLine("Service Stopped & Uninstalled");
		}


	}
}
