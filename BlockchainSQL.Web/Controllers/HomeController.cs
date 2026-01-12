// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sphere10.Framework;


namespace BlockchainSQL.Web.Controllers {

	public class HomeController : BaseController {

		[Route("/")]
		public ActionResult Index(string errorMessage) {
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

		[Route("/error")]
		public ActionResult Error() {
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
			var exception = context.Error; // Your exception
			SystemLog.Exception(exception);
			var code = 500; // Internal Server Error by default

			//if (exception is MyNotFoundException) code = 404; // Not Found
			//else if (exception is MyUnauthException) code = 401; // Unauthorized
			//else if (exception is MyException) code = 400; // Bad Request

			Response.StatusCode = code; // You can use HttpStatusCode enum instead

			//return new MyErrorResponse(exception); // Your error model
			return View();
		}
	}
}
