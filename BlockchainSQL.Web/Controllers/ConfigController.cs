// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.DataAccess;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sphere10.Framework;
using Sphere10.Framework.Application;
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
					model.WebDbModel.Database,
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
							model.WebDbModel.Database,
							model.WebDbModel.Username,
							model.WebDbModel.Password,
							model.WebDbModel.Port
						)) {
							result.AddError("Unable to generate Web database, check connection details");
						}
					} else {
						result.AddError("Could not connect to the Web database, check connection details.");
					}

				var blockchainValid = DatabaseManager.IsValidBlockchainDatabase(
					DBMSType.SQLServer, 
					model.BlockchainDbModel.Server, 
					model.BlockchainDbModel.Database, 
					model.BlockchainDbModel.Username, 
					model.BlockchainDbModel.Password, 
					model.BlockchainDbModel.Port, 
					out var blockchainConnectionString
				);

				if (!blockchainValid) {
					result.AddError("Could not connect to the BSQL database, check connection details.");
				}

				if (result.IsFailure) {
					return Json(new FormResult {
						Result = false,
						Message = result.ErrorMessages.ToDelimittedString("<br/>")
					});
				}

				var blockchainSettings = GlobalSettings.Get<WebSettings>();
				blockchainSettings.BlockchainDatabaseConnectionString = blockchainConnectionString;
				blockchainSettings.BlockchainDBMSType = DBMSType.SQLServer;

				var webSettings = GlobalSettings.Get<WebSettings>();
				webSettings.WebDBMSType = DBMSType.SQLServer;
				webSettings.WebDatabaseConnectionString = webConnectionString;

				blockchainSettings.Save();
				webSettings.Save();
				DatabaseManager.InitializeDatabases();

				return Json(new FormResult {
					Result = true,
					Message = "Database connection details configured successfully.",
					ResultType = FormResultType.Redirect,
					Url = Url.Action("Index", "Explorer")
				});
			} catch (Exception error) {
				// Log error
				return Json(new FormResult {
					Result = false,
					Message = error.ToDisplayString(),
					ResultType = FormResultType.ShowMessage
				});
			}
		}

		[HttpPost]
		public async Task<ActionResult> Auth(LoginForm form) {
			try {
				if ((form.Username, form.Password) == (SiteOptions.Value.AdminUsername, SiteOptions.Value.AdminPassword)) {
					await SignInAsync();
					return RedirectToAction("Index", "Config");
				} else {
					ModelState.AddModelError("Password", "Invalid Credentials");
					return View(form);
				}
			} catch (Exception exception) {
				ModelState.AddModelError("Password", "Unexpected error");
				return View(form);
			}
		}

	}
}
