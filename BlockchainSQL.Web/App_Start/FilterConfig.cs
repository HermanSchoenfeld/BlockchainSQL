using System.Web;
using System.Web.Mvc;

namespace BlockchainSQL.Web {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
