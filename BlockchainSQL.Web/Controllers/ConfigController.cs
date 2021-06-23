using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlockchainSQL.Web.Controllers {
	public class ConfigController : BaseController {
		private IConfiguration Configuration { get; }

		public ConfigController(IConfiguration configuration) {
			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		[Authorize]
		public ActionResult Index() {
			AddPageMessage("Please confirm database connection config.", "Config required", PageMessageSeverity.Info);
			return View();
		}

		public ActionResult Auth() {
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Auth(AuthForm form) {
			try {
				if (Configuration["ConfigPassword"] == form.Password) {

					var claims = new List<Claim> {
						new(ClaimTypes.Role, "Administrator"),
					};

					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var authProperties = new AuthenticationProperties {
						ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
						IsPersistent = true,
						IssuedUtc = DateTimeOffset.UtcNow
					};

					await HttpContext.SignInAsync(
						CookieAuthenticationDefaults.AuthenticationScheme,
						new ClaimsPrincipal(claimsIdentity),
						authProperties);

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
