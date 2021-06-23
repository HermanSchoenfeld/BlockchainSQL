using System;
using System.Collections.Generic;
using BlockchainSQL.Web.Models;
using NHibernate;
using Sphere10.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BlockchainSQL.Web.Controllers
{
    public abstract class BaseController : Controller {
	    
        public void AddPageMessage(string message, string title = null, PageMessageSeverity severity = PageMessageSeverity.Info, bool dismissable = true) {
            AddPageMessage(new PageMessage {Description = message, Title = title, Dismissable = dismissable, Severity = severity});
        }

        public void AddPageMessage(PageMessage message) {
            if (!ViewData.ContainsKey("PageMessages")) {
                ViewBag.PageMessages = new List<PageMessage>();
            }
            ViewBag.PageMessages.Add(message);
        }

        public ActionResult HomePageRedirect(string errorMessage = null) {
            return Redirect("/{0}".FormatWith(!string.IsNullOrWhiteSpace(errorMessage) ? "?errorMessage=" +Uri.EscapeDataString(errorMessage) : string.Empty));
        }

        public ISession OpenSession() {
            if (!AppConfig.IsValid)
                throw new SoftwareException("No Web DBMS is configured");
            return AppConfig.NhSessionFactory.OpenSession();
        }

    }
}