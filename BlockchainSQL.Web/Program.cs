// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

// This application entry point is based on ASP.NET Core new project templates and is included
// as a starting point for app host configuration.
// This file may need updated according to the specific scenario of the application being upgraded.
// For more information on ASP.NET Core hosting, see https://docs.microsoft.com/aspnet/core/fundamentals/host/web-host

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BlockchainSQL.Web {
	public class Program {
		
		public static Task Main(string[] args) {
			var configBuilder = new ConfigurationBuilder().AddCommandLine(args).Build();
			return Host
				.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => {
					webBuilder.UseStartup<Startup>();
					Sphere10Framework.Instance.Initialized += () => {
						SystemLog.RegisterLogger(Sphere10Framework.Instance.CreateApplicationLogger("web.log", visibleToAllUsers:true));SystemLog.Info("Web Server Started");
					};
					
				})
				.UseSphere10Framework()
				.Build()
				.StartSphere10Framework()
				.RunAsync();
			SystemLog.Info("Web Server Ended");
			//Sphere10Framework.Instance.StartFramework();
			//SystemLog.RegisterLogger(Sphere10Framework.Instance.CreateApplicationLogger("web.log", visibleToAllUsers:true));
			//SystemLog.Info("Web Server Started");
			//CreateHostBuilder(args).Build().Run();
			//SystemLog.Info("Web Server Ended");
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host
			.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>	webBuilder.UseStartup<Startup>());
	}
}
