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

		public static void RunAsService() {
			// load first-run time
			Sphere10Framework.Instance.StartFramework(configure => 
					configure.AddTransient<IUserInterfaceServices, ConsoleApplicationUserInterfaceServices>()/*,
				Sphere10FrameworkOptions.EnableDrm | Sphere10FrameworkOptions.BackgroundLicenseVerify*/
			);
			ServiceBase.Run(new BlockchainSQLService());
		}

	}
}
