// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Sphere10.Framework;
using Microsoft.AspNetCore.Mvc;
using ISession = NHibernate.ISession;
using BlockchainSQL.Web.Code;

namespace BlockchainSQL.Web.Controllers {
	public abstract class BaseController : Controller {

		public void AddPageMessage(string message, string title = null, PageMessageSeverity severity = PageMessageSeverity.Info,
		                           bool dismissable = true) {
			AddPageMessage(new PageMessage { Description = message, Title = title, Dismissable = dismissable, Severity = severity });
		}

		public void AddPageMessage(PageMessage message) {
			if (!ViewData.ContainsKey("PageMessages")) {
				ViewBag.PageMessages = new List<PageMessage>();
			}
			ViewBag.PageMessages.Add(message);
		}

		public ActionResult HomePageRedirect(string errorMessage = null) {
			return Redirect("/{0}".FormatWith(!string.IsNullOrWhiteSpace(errorMessage)
				? "?errorMessage=" + Uri.EscapeDataString(errorMessage)
				: string.Empty));
		}

		public ISession OpenSession() {
			if (!DatabaseManager.IsValid)
				throw new SoftwareException("No Web DBMS is configured");
			return DatabaseManager.NhSessionFactory.OpenSession();
		}
		
		public async Task SignInAsync() {
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
		}

		

	}
}
