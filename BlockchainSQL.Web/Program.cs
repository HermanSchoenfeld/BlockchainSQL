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

namespace BlockchainSQL.Web {
	public class Program {
		public static void Main(string[] args) {
			SystemLog.RegisterLogger(new FileAppendLogger(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlockchainSQL", "log", "Web.log")));
			Sphere10Framework.Instance.StartFramework();
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host
			.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>	webBuilder.UseStartup<Startup>());
	}
}
