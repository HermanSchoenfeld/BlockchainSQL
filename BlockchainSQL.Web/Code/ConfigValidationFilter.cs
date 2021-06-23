using System;
using BlockchainSQL.Web.Controllers;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlockchainSQL.Web.Code {
	public class ConfigValidationFilter : IActionFilter {

		public void OnActionExecuting(ActionExecutingContext context) {
			try {

				if (context.Controller is not ConfigController and not FormController and not GridController) {
					var valid = AppConfig.IsValid;

					if (!valid) {
						context.Result = new RedirectToActionResult(nameof(ConfigController.Index), "Config", null);
					}
				}
			} catch (Exception) {
				context.Result = new RedirectToActionResult(nameof(ConfigController.Index), "Config", null);
			}
		}

		public void OnActionExecuted(ActionExecutedContext context) {
		}
	}
}
