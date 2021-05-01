using System.Web.Mvc;
using System.Web.Routing;

namespace BlockchainSQL.Web
{
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Server page",
                url: "Server",
                defaults: new {controller = "Home", action = "Product"}
                );


            routes.MapRoute(
                name: "About page",
                url: "About",
                defaults: new {controller = "Home", action = "About"}
                );


            routes.MapRoute(
                name: "Block explorer",
                url: "Explorer",
                defaults: new {controller = "Explorer", action = "Index"}
                );

            routes.MapRoute(
                name: "Configuration",
                url: "Config",
                defaults: new {controller = "Config", action = "Index"}
                );

            routes.MapRoute(
                "Block View By Height",
                "Block/{height}",
                new {controller = "Explorer", action = "ViewBlockByHeight" },
				new {height = @"\d+"}
            );

			routes.MapRoute(
				"Block View By Hash",
				"Block/{hash}",
				defaults: new { controller = "Explorer", action = "ViewBlockByHash", hash = "" }
			);


			routes.MapRoute(
                name: "Transaction View",
                url: "Txn/{txid}",
                defaults: new {controller = "Explorer", action = "ViewTransaction", txid = ""}
                );

            routes.MapRoute(
                name: "Address View",
                url: "Address/{address}",
                defaults: new {controller = "Address", action = "Index", address = ""}
                );

            routes.MapRoute(
                name: "Search",
                url: "Search/{text}",
                defaults: new { controller = "Search", action = "Index", @text = "" }
            );

            routes.MapRoute(
                name: "Query Template Load",
                url: "Query/Template/{templateID}",
                defaults: new {controller = "Query", action = "Template"}
                );

            routes.MapRoute(
                name: "Query Execution",
                url: "Query/Execute",
                defaults: new {controller = "Query", action = "Execute"}
                );

            routes.MapRoute(
                name: "Query Save",
                url: "Query/Save",
                defaults: new {controller = "Query", action = "Save"}
                );

            routes.MapRoute(
                name: "Query Load",
                url: "{queryID}",
                defaults: new {controller = "Query", action = "Load"},
                constraints: new {queryID = @"[a-zA-Z0-9]{6,}"}
                );



        

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );



        }
    }
}
