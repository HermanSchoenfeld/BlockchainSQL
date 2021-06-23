// This Startup file is based on ASP.NET Core new project templates and is included
// as a starting point for DI registration and HTTP request processing pipeline configuration.
// This file will need updated according to the specific scenario of the application being upgraded.
// For more information on ASP.NET Core startup files, see https://docs.microsoft.com/aspnet/core/fundamentals/startup

using System;
using BlockchainSQL.Web.Code;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Omu.AwesomeMvc;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services.Configure<RazorViewEngineOptions>(options => {
		        options.ViewLocationExpanders.Add(new ViewLocationExpander());
	        });
	        
	        var provider = new AweMetaProvider();
	        services.AddMvc(o =>
	        {
		        o.ModelMetadataDetailsProviders.Add(provider);
		        o.Filters.Add(typeof(ConfigValidationFilter));
	        });

	        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		        .AddCookie(options => {
			        options.LoginPath = "/config/auth";
		        });
	        
	        services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
	        if (env.IsDevelopment()) {
		        app.UseDeveloperExceptionPage();
	        }

	        app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
	            endpoints.MapControllerRoute(
		            name: "default",
		            pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            GlobalSettings.Provider = GlobalSettings.CreateDefaultProvider();
            
            TryInitializeDatabaseClasses();
        }

        private void TryInitializeDatabaseClasses() {
	        try {
		        AppConfig.InitializeDatabaseObjects();
	        } catch (Exception) {
	        }
        }
    }
}
