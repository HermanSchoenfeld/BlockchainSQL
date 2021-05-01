using System;
using System.Web.Configuration;
using Sphere10.Framework.Data;
using Sphere10.Framework.Application;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.Web
{
    public class WebConfig {

        public WebConfig() {
            HasWebDBMS = WebConfigurationManager.ConnectionStrings.HasConnectionString("Web");
            HasValidWebDBMS = HasWebDBMS;
            if (HasWebDBMS) {
                WebDBMSType = DetermineDBMSTypeFromProviderName(WebConfigurationManager.ConnectionStrings["Web"].ProviderName);
                WebConnectionString = WebConfigurationManager.ConnectionStrings["Web"].ConnectionString;
            }

            BlockchainDBMSType = DetermineDBMSTypeFromProviderName(WebConfigurationManager.ConnectionStrings["Blockchain"].ProviderName);
            BlockchainConnectionString = WebConfigurationManager.ConnectionStrings["Blockchain"].ConnectionString;
            SMTPServer = WebConfigurationManager.AppSettings["SMTPServer"];
            SMTPPort = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
            SMTPUsername = WebConfigurationManager.AppSettings["SMTPUsername"];
            SMTPPassword = WebConfigurationManager.AppSettings["SMTPPassword"];
            ContactRecipientEmail = WebConfigurationManager.AppSettings["ContactRecipientEmail"];
        }

        public bool HasWebDBMS { get; private set; }

        public bool HasValidWebDBMS { get; set; }
        public DBMSType WebDBMSType { get; private set; }

        public string WebConnectionString { get; private set; }
        public DBMSType BlockchainDBMSType { get; private set; }

        public string BlockchainConnectionString { get; private set; }

        public string SMTPServer { get; private set; }

        public int SMTPPort { get; private set; }

        public string SMTPUsername { get; private set; }

        public string SMTPPassword { get; private set; }

        public string ContactRecipientEmail { get; private set; }


        private DBMSType DetermineDBMSTypeFromProviderName(string providerName) {
            switch (providerName.ToUpperInvariant()) {
                case "SYSTEM.DATA.SQLCLIENT":
                    return DBMSType.SQLServer;
                default:
                    throw new NotSupportedException(providerName);
            }
        }

    }
}