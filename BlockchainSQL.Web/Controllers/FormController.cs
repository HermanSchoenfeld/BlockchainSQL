using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.DataAccess;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sphere10.Framework.Web.AspNetCore;

namespace BlockchainSQL.Web.Controllers {
	public class FormController : BaseController {
		private IConfiguration Configuration { get; }
		
		private IOptions<SiteOptions> SiteOptions { get; }

		public FormController(IConfiguration configuration, IOptions<SiteOptions> siteOptions) {
			Configuration = configuration;
			SiteOptions = siteOptions;
		}

		[HttpPost]
		[FormAction]
		public async Task<ActionResult> Contact(ContactFormInput model) {
			try {
				if (!ModelState.IsValid) {
					return PartialView(model);
				}
				await Task.Run(() =>
					Tools.Mail.SendEmail(
						DatabaseManager.Options.SMTPServer,
						model.Email ?? "no-reply@sphere10.com",
						"BlockchainSQL Enquiry",
						"Name: {1}{0}Email: {2}{0}Subject: {3}".FormatWith(Environment.NewLine, model.Name, model.Email, model.Message),
						DatabaseManager.Options.ContactRecipientEmail,
						requiresSSL: true,
						username: DatabaseManager.Options.SMTPUsername,
						password: DatabaseManager.Options.SMTPPassword,
						port: DatabaseManager.Options.SMTPPort
					));
			} catch (Exception) {
				// Log error
				return Json(new {
					Result = false,
					Message = "We are experiencing technical difficulties. Please try later or contact us by another method."
				});
			}
			return Json(new {
				Result = true,
				Message = "Thank you for contacting us, {0}. We will get back to you as soon as we can!".FormatWith(model.Name)
			});

		}

	
		
		[HttpPost]
		[FormAction]
		public async Task<ActionResult> Login(LoginForm form) {

			if (form.Username == Configuration["ConfigUsername"] && form.Password == Configuration["ConfigPassword"]) {
				await SignInAsync();
				return Json(new FormResult {
					Result = true,
					ResultType = FormResultType.Redirect,
					Location = Url.Action("Index", "Home")
				});
			} else {
				return Json(new FormResult {
					Result = false,
					ResultType = FormResultType.ShowMessage,
					Message = "Invalid Login Details"
				});
			}
		}

		[HttpPost]
		[FormAction]
		public async Task<ActionResult> Logout(LogoutForm _) {
			await HttpContext.SignOutAsync();

			return Json(new FormResult {
				Result = true,
				ResultType = FormResultType.Redirect,
				Location = Url.Action("Index", "Home")
			});
		}


	}
}
