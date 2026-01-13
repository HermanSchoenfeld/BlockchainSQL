// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using BlockchainSQL.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlockchainSQL.Web.Code {
	public class ConfigValidationFilter : IActionFilter {


		public void OnActionExecuting(ActionExecutingContext context) {
			try {
				if (context.Controller is not ConfigController and not FormController and not GridController and not QueryController) {
					if (!DatabaseManager.IsConfigured)
						context.Result = new RedirectToActionResult(nameof(ConfigController.InitialConfig), "Config", null);
				}
			} catch (Exception) {
				context.Result = new RedirectToActionResult(nameof(ConfigController.Index), "Config", null);
			}
		}

		public void OnActionExecuted(ActionExecutedContext context) {
		}
	}
}
