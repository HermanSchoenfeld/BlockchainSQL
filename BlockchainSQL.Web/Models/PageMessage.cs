// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;

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