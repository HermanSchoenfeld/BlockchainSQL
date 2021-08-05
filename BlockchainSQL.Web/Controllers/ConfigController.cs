using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.DataAccess;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Web.AspNetCore;

namespace BlockchainSQL.Web.Controllers {

	public class ConfigController : BaseController {

		public ConfigController(IOptions<SiteOptions> options) {
			SiteOptions = options ?? throw new ArgumentNullException(nameof(options));
		}

		private IDatabaseManager DatabaseGenerator { get; }

		private IOptions<SiteOptions> SiteOptions { get; }

		[Authorize]
		[HttpGet]
		[Route("/config")]
		public ActionResult Index() {
			return View();
		}

		[HttpGet]
		[Route("/initial-config")]
		public ActionResult InitialConfig() {

			if (DatabaseManager.IsConfigured)
				return RedirectToAction("Index");

			return View("Index");
		}


		[HttpPost]
		[FormAction]
		public async Task<ActionResult> ConfigureDatabases(ConfigureDatabaseFormInput model) {
			if (!ModelState.IsValid) {
				return PartialView(model);
			}

			try {
				var result = Result.Default;
				var webValid = DatabaseManager.IsValidWebDatabase(
					DBMSType.SQLServer,
					model.WebDbModel.Server,
					model.WebDbModel.Server,
					model.WebDbModel.Username,
					model.WebDbModel.Password,
					model.WebDbModel.Port,
					out var webConnectionString
				);
				if (!webValid)
					if (model.WebDbModel.GenerateIfNotExists) {
						if (!await DatabaseManager.GenerateWebDatabase(
							DBMSType.SQLServer,
							model.WebDbModel.Server,
							model.WebDbModel.Server,
							model.WebDbModel.Username,
							model.WebDbModel.Password,
							model.WebDbModel.Port
						)) {
							result.AddError("Unable to generate WebDB database, check connection details");
						}
					} else {
						result.AddError("Could not connect to the WebDB database, check connection details.");
					}

				var blockchainValid = DatabaseManager.IsValidBlockchainDatabase(
					DBMSType.SQLServer, 
					model.BlockchainDbModel.Server, 
					model.BlockchainDbModel.Server, 
					model.BlockchainDbModel.Username, 
					model.BlockchainDbModel.Password, 
					model.BlockchainDbModel.Port, 
					out var blockchainConnectionString
				);

				if (!blockchainValid) {
					result.AddError("Could not connect to the WebDB database, check connection details.");
				}

				if (result.Failure) {
					return Json(new FormResult {
						Result = false,
						Message = result.ErrorMessages.ToDelimittedString("<br/>")
					});
				}

				return Json(new FormResult {
					Result = true,
					Message = "Database connection details configured successfully.",
					ResultType = FormResultType.Redirect,
					Location = Url.Action("Index", "Explorer")
				});
			} catch (Exception error) {
				// Log error
				return Json(new {
					Result = false,
					Message = error.ToDisplayString()
				});
			}
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
