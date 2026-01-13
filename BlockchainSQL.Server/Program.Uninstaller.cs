// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
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

		public static void RunAsUninstallServiceCommand(CommandLineResults uninstallCommand) {
			Sphere10Framework.Instance.StartFramework(configure => 
					configure.AddTransient<IUserInterfaceServices, ConsoleApplicationUserInterfaceServices>()/*,
				Sphere10FrameworkOptions.EnableDrm | Sphere10FrameworkOptions.BackgroundLicenseVerify*/
			);
			var path = uninstallCommand.GetSingleArgumentValue("path");
			ServiceManager.UninstallService(path).Wait();
			Console.WriteLine("Service Stopped & Uninstalled");
		}

	}
}
