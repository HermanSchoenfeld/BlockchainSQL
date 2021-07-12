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

namespace BlockchainSQL.Web.Controllers {
	public class FormController : BaseController {
		private IConfiguration Configuration { get; }
		
		private IOptions<SiteOptions> SiteOptions { get; }

		private IDatabaseGenerator DatabaseGenerator { get; }

		public FormController(IConfiguration configuration, IOptions<SiteOptions> siteOptions) {
			Configuration = configuration;
			SiteOptions = siteOptions;
			DatabaseGenerator = WebDatabase.NewDatabaseGenerator(DBMSType.SQLServer);
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
						AppConfig.Options.SMTPServer,
						model.Email ?? "no-reply@sphere10.com",
						"BlockchainSQL Enquiry",
						"Name: {1}{0}Email: {2}{0}Subject: {3}".FormatWith(Environment.NewLine, model.Name, model.Email, model.Message),
						AppConfig.Options.ContactRecipientEmail,
						requiresSSL: true,
						username: AppConfig.Options.SMTPUsername,
						password: AppConfig.Options.SMTPPassword,
						port: AppConfig.Options.SMTPPort
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
		public async Task<ActionResult> ConfigureDatabases(ConfigureDatabaseFormInput model) {
			if (!ModelState.IsValid) {
				return PartialView(model);
			}

			try {
				var dbmsType = DBMSType.SQLServer;

				var webConnectionStringBuilder = new SqlConnectionStringBuilder {
					DataSource = model.WebDbModel.Server + "," + model.WebDbModel.Port,
					InitialCatalog = model.WebDbModel.Database,
					Password = model.WebDbModel.Password,
					UserID = model.WebDbModel.Username,
				};

				if (DatabaseGenerator.DatabaseExists(webConnectionStringBuilder.ConnectionString))
					AppConfig.SetWebDatabaseConnectionString(webConnectionStringBuilder.ConnectionString);
				else {
					if (model.WebDbModel.GenerateIfNotExists) {
						if (await GenerateDatabase(dbmsType,
							webConnectionStringBuilder.ConnectionString,
							webConnectionStringBuilder.InitialCatalog))
							AppConfig.SetWebDatabaseConnectionString(webConnectionStringBuilder.ConnectionString);
						else {
							return Json(new {
								Result = false,
								Message = "Unable to create web database."
							});
						}
					} else {
						return Json(new {
							Result = false,
							Message = "Could not connect to web database, check connection details."
						});
					}
				}

				var blockchainConnectionStringBuilder = new SqlConnectionStringBuilder {
					DataSource = model.BlockchainDbModel.Server + "," + model.BlockchainDbModel.Port,
					InitialCatalog = model.BlockchainDbModel.Database,
					Password = model.BlockchainDbModel.Password,
					UserID = model.BlockchainDbModel.Username,
				};

				if (DatabaseGenerator.DatabaseExists(blockchainConnectionStringBuilder.ConnectionString))
					AppConfig.SetBlockchainDatabaseConnectionString(blockchainConnectionStringBuilder.ConnectionString);
				else {
					return Json(new FormResult {
						Result = false,
						Message = "Could not connect to the BlockchainSQL database, check connection details."
					});
				}
				
				return Json(new FormResult {
					Result = true,
					Message = "Database connection details configured successfully.",
					ResultType = FormResultType.Redirect,
					Location = Url.Action("Index", "Explorer")
				});
			} catch (Exception error) {
				// Log error
				return Json(new {
					Result = false,
					Message = error.ToDisplayString()
				});
			}
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

		private async Task<bool> GenerateDatabase(DBMSType dbmsType, string connectionString, string databaseName) {
			if (await Task.Run(() => DatabaseGenerator.DatabaseExists(connectionString)))
				return false;
			else {
				await Task.Run(() => DatabaseGenerator.CreateEmptyDatabase(connectionString));
				await Task.Run(() =>
					DatabaseGenerator.CreateNewDatabase(connectionString, DatabaseGenerationDataPolicy.PrimingData, databaseName));
			}

			return true;
		}
	}
}
