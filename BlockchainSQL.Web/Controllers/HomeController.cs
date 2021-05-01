using System.Web.Mvc;
using BlockchainSQL.Web.Models;

namespace BlockchainSQL.Web.Controllers
{
    public class HomeController : BaseController {

        public ActionResult Index(string errorMessage) {
            if (!Config.HasValidWebDBMS)
                return Redirect("/Config");

            if (!string.IsNullOrWhiteSpace(errorMessage))
                AddPageMessage(errorMessage, null, PageMessageSeverity.Error, true);

            return View("~/Views/Query/Index.cshtml", QueryPageModel.Default);
        }


        // GET: Product
        public ActionResult Product() {
            return View();
        }


        public ActionResult About() {
            return View();
        }

    }
}