using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using BlockchainSQL.Web.Models;
using NHibernate;
using Sphere10.Framework;

namespace BlockchainSQL.Web.Controllers {
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

        public WebConfig Config => ApplicationSingletons.Config;

        public ISession OpenSession() {
            if (!ApplicationSingletons.Config.HasWebDBMS)
                throw new SoftwareException("No Web DBMS is configured");

            return ApplicationSingletons.NhSessionFactory.OpenSession();
        }

    }
}