using System;
using System.Threading.Tasks;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.DataAccess;
using BlockchainSQL.Web.Models;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BlockchainSQL.Web.Controllers
{
    public class FormController : BaseController {
	    private IConfiguration Configuration { get; }

	    public FormController(IConfiguration configuration) {
		    Configuration = configuration;
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
                        port:  AppConfig.Options.SMTPPort
                        ));
            } catch (Exception error) {
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
        public async Task<ActionResult> CreateDatabase(CreateDatabaseFormInput model) {
            if (!ModelState.IsValid) {
                return PartialView(model);
            }
            try {
                var dbmsType = DBMSType.SQLServer;
                
                var connectionString = Tools.MSSQL.CreateConnectionString(model.Server, model.Database, model.Username, model.Password, port: model.Port); ;
                var databaseName = model.Database;
                if (await GenerateDatabase(dbmsType, connectionString, databaseName, model.OverwritePolicy)) {
	             
	                AppConfig.Register(Configuration);
	                
	                return Json(new {
                        Result = true,
                        Message = "Database has been created successfully. <br/>Connection String: <i>{0}</i>".FormatWith(connectionString)
                    });
                } else {
                    return Json(new {
                        Result = false,
                        Message = "Unable to create database. If already exists, please select appropriate overwrite policy."
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



        private async Task<bool> GenerateDatabase(DBMSType dbmsType, string connectionString, string databaseName,
                                                  DatabaseGenerationAlreadyExistsPolicy existsPolicy) {

	        var dropExisting = false;
	        var createShell = false;
	        var createDatabase = false;
	        var schemaGenerator = WebDatabase.NewDatabaseGenerator(dbmsType);
	        if (await Task.Run(() => schemaGenerator.DatabaseExists(connectionString))) {
		        switch (existsPolicy) {
			        case DatabaseGenerationAlreadyExistsPolicy.Error:
				        return false;
			        case DatabaseGenerationAlreadyExistsPolicy.Overwrite:
				        dropExisting = true;
				        createShell = true;
				        createDatabase = true;
				        break;
			        case DatabaseGenerationAlreadyExistsPolicy.Append:
				        createDatabase = true;
				        break;
		        }
	        } else {
		        createShell = true;
		        createDatabase = true;
	        }

	        if (dropExisting)
		        await Task.Run(() => schemaGenerator.DropDatabase(connectionString));

	        if (createShell)
		        await Task.Run(() => schemaGenerator.CreateEmptyDatabase(connectionString));

	        if (createDatabase)
		        await Task.Run(() =>
			        schemaGenerator.CreateNewDatabase(connectionString, DatabaseGenerationDataPolicy.PrimingData, databaseName));



	        return true;

        }
    }
}