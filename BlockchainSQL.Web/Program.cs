// This application entry point is based on ASP.NET Core new project templates and is included
// as a starting point for app host configuration.
// This file may need updated according to the specific scenario of the application being upgraded.
// For more information on ASP.NET Core hosting, see https://docs.microsoft.com/aspnet/core/fundamentals/host/web-host

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Hydrogen;
using Hydrogen.Application;
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
					SystemLog.RegisterLogger(HydrogenFramework.Instance.CreateApplicationLogger("web.log", visibleToAllUsers:true));
					SystemLog.Info("Web Server Started");
				})
				.UseHydrogenFramework()
				.Build()
				.StartHydrogenFramework()
				.RunAsync();
			SystemLog.Info("Web Server Ended");
			//HydrogenFramework.Instance.StartFramework();
			//SystemLog.RegisterLogger(HydrogenFramework.Instance.CreateApplicationLogger("web.log", visibleToAllUsers:true));
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
