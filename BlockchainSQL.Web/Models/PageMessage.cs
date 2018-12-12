using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainSQL.Web.Models {
    public class PageMessage {
        public PageMessageSeverity Severity { get; set; } = PageMessageSeverity.Info;
        public string Title { get; set; } = null;
        public string Description { get; set; } = "No text was provided for this page message";

        public bool Dismissable { get; set; } = true;

        public static string SeverityString(PageMessageSeverity severity) {
            switch (severity) {
                case PageMessageSeverity.Info:
                    return "info";
                case PageMessageSeverity.Success:
                    return "success";
                case PageMessageSeverity.Warning:
                    return "warning";
                case PageMessageSeverity.Error:
                    return "danger";
                case PageMessageSeverity.Royal:
                    return "royal";
                case PageMessageSeverity.Primary:
                    return "primary";
                default:
                    throw new ArgumentOutOfRangeException(nameof(severity), severity, null);
            }
        }
    }
}