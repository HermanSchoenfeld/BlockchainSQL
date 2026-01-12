using System;
using System.Threading.Tasks;
using BlockchainSQL.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Sphere10.Framework;
using Sphere10.Framework.Web.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace BlockchainSQL.Web.Controllers {
	public class FormController : BaseController {

		public FormController(IConfiguration configuration, IOptions<SiteOptions> siteOptions) {
			Configuration = configuration;
			SiteOptions = siteOptions;
		}

		private IConfiguration Configuration { get; }

		private IOptions<SiteOptions> SiteOptions { get; }

		[HttpPost]
		[FormAction]
		public async Task<ActionResult> Contact(ContactFormInput model) {
			try {
				if (!ModelState.IsValid) {
					return PartialView(model);
				}
				
				await Task.Run(() =>
					Tools.Mail.SendEmail(
						SiteOptions.Value.SMTPServer,
						model.Email,
						"BlockchainSQL Enquiry",
						"Name: {1}{0}Email: {2}{0}Subject: {3}".FormatWith(Environment.NewLine, model.Name, model.Email, model.Message),
						SiteOptions.Value.ContactRecipientEmail,
						requiresSSL: false,
						username: SiteOptions.Value.SMTPUsername,
						password: SiteOptions.Value.SMTPPassword,
						port: SiteOptions.Value.SMTPPort
					));
			} catch (Exception) {
				// Log error
				return Json(new FormResult {
					Result = false,
					ResultType = FormResultType.ShowMessage,
					Message = "We are experiencing technical difficulties. Please try later or contact us by another method."
				});
			}
			return Json(new FormResult {
				Result = true,
				ResultType = FormResultType.ShowMessage,
				Message = "Thank you for contacting us, {0}. We will get back to you as soon as we can!".FormatWith(model.Name)
			});

		}

	
		
		[HttpPost]
		[FormAction]
		public async Task<ActionResult> Login(LoginForm form) {

			if (form.Username == SiteOptions.Value.AdminUsername && form.Password == SiteOptions.Value.AdminPassword) {
				await SignInAsync();
				return Json(new FormResult {
					Result = true,
					ResultType = FormResultType.Redirect,
					Url = Url.Action("Index", "Config")
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
				Url = Url.Action("Index", "Home")
			});
		}


	}
}
