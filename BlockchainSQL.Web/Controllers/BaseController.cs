using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Sphere10.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using ISession = NHibernate.ISession;
using RouteData = Microsoft.AspNetCore.Routing.RouteData;

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
			if (!AppConfig.IsValid)
				throw new SoftwareException("No Web DBMS is configured");
			return AppConfig.NhSessionFactory.OpenSession();
		}

		public async Task<string> RenderViewAsync(string viewName, object model, bool isPartial = false) {
			var controllerContext = ControllerContext;
			IViewEngine viewEngine = ControllerContext.HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>();

			var httpContext = new DefaultHttpContext { RequestServices = controllerContext.HttpContext.RequestServices };
			var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

			var viewResult = viewEngine.FindView(actionContext, viewName, !isPartial);
			
			if (viewResult is null)
				throw new InvalidOperationException($"View with name {viewName} was not found");
			
			StringWriter stringWriter;
			await using (stringWriter = new StringWriter()) {
				var viewContext = new ViewContext(
					controllerContext,
					viewResult.View,
					new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model },
					TempData,
					stringWriter,
					new HtmlHelperOptions());

				await viewResult.View.RenderAsync(viewContext);
			}

			return stringWriter.GetStringBuilder().ToString();
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
