using System;
using System.Threading.Tasks;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlockchainSQL.Web.Controllers {

	public class ConfigController : BaseController {

		private IOptions<SiteOptions> SiteOptions { get; }

		public ConfigController(IOptions<SiteOptions> options) {
			SiteOptions = options ?? throw new ArgumentNullException(nameof(options));
		}

		[Authorize]
		[HttpGet]
		[Route("/config")]
		public ActionResult Index() {
			return View();
		}

		[HttpGet]
		[Route("/initial-config")]
		public ActionResult InitialConfig() {

			if (AppConfig.IsConfigured)
				return RedirectToAction("Index");

			return View("Index");
		}

		[HttpPost]
		public async Task<ActionResult> Auth(LoginForm form) {
			try {
				if (SiteOptions.Value.ConfigPassword == form.Password) {

					await SignInAsync();

					return RedirectToAction("Index", "Config");
				} else {
					ModelState.AddModelError("Password", "Invalid Password");
					return View(form);
				}
			} catch (Exception) {
				ModelState.AddModelError("Password", "Invalid Password");
				return View(form);
			}
		}
	}
}
