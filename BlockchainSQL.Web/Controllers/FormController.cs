using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.DataAccess;
using BlockchainSQL.Web.Models;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tools;

namespace BlockchainSQL.Web.Controllers {
	public class FormController : BaseController {
		private IConfiguration Configuration { get; }

		private IDatabaseGenerator DatabaseGenerator { get; }

		public FormController(IConfiguration configuration) {
			Configuration = configuration;
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
		public async Task<ActionResult> ConfigureWebDatabase(ConfigureWebDatabaseFormInput model) {
			if (!ModelState.IsValid) {
				return PartialView(model);
			}

			try {
				var dbmsType = DBMSType.SQLServer;

				var builder = new SqlConnectionStringBuilder {
					DataSource = model.Server + "," + model.Port,
					InitialCatalog = model.Database,
					Password = model.Password,
					UserID = model.Username,
				};

				if (DatabaseGenerator.DatabaseExists(builder.ConnectionString)) {
					AppConfig.SetWebDatabaseConnectionString(builder.ConnectionString);
					return Json(new {
						Result = true,
						Message =
							"Web database connection details updated."
					});
				} else {
					if (model.GenerateIfNotExists) {
						if (await GenerateDatabase(dbmsType, builder.ConnectionString, builder.InitialCatalog)) {
							return Json(new {
								Result = true,
								Message = "Database has been created successfully."
							});
						} else {
							return Json(new {
								Result = false,
								Message = "Unable to create database."
							});
						}
					} else {
						return Json(new {
							Result = false,
							Message = "Could not connect to database, check connection details."
						});
					}
				}
			} catch (Exception error) {
				// Log error
				return Json(new {
					Result = false,
					Message = error.ToDisplayString()
				});
			}
		}

		public async Task<ActionResult> ConfigureBlockchainDb(ConfigureBlockchainDbFormInput model) {
			if (!ModelState.IsValid) {
				return PartialView(model);
			}
			var builder = new SqlConnectionStringBuilder {
				DataSource = model.Server + "," + model.Port,
				InitialCatalog = model.Database,
				Password = model.Password,
				UserID = model.Username,
			};

			try {
				if (DatabaseGenerator.DatabaseExists(builder.ConnectionString)) {
					AppConfig.SetBlockchainDatabaseConnectionString(builder.ConnectionString);

					return Json(new {
						Result = true,
						Message = "Blockchain database connection details updated."
					});
				} else {
					return Json(new {
						Result = false,
						Message = "Could not connect to database, check connection details."
					});
				}
			} catch (Exception error) {
				// Log error
				return Json(new {
					Result = false,
					Message = error.ToDisplayString()
				});
			}
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
