using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainSQL.Web.Controllers {
	
	public class HomeController : BaseController {

		[Route("/")]
		public ActionResult Index(string errorMessage) {
			if (!AppConfig.IsValid)
				return RedirectToAction("Index", "Config");

			if (!string.IsNullOrWhiteSpace(errorMessage))
				AddPageMessage(errorMessage, null, PageMessageSeverity.Error);

			return View("~/Views/Query/Index.cshtml", QueryPageModel.Default);
		}

		[Route("/product")]
		public ActionResult Product() {
			return View();
		}

		[Route("/about")]
		public ActionResult About() {
			return View();
		}
	}
}
