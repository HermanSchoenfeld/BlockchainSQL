using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using BlockchainSQL.Web.Code;

namespace BlockchainSQL.Web {
    public class ViewEnginesConfig {
        public static void Register() {
            //ViewEngines.Engines.Add(new FormsShortcutViewEngine());
        }
    }
}
