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

		public static void RunAsService() {
			// load first-run time
			HydrogenFramework.Instance.StartFramework(configure => 
					configure.AddTransient<IUserInterfaceServices, ConsoleApplicationUserInterfaceServices>(),
				HydrogenFrameworkOptions.EnableDrm | HydrogenFrameworkOptions.BackgroundLicenseVerify
			);
			ServiceBase.Run(new BlockchainSQLService());
		}

	}
}
