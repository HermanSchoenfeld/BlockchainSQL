// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using Sphere10.Framework.Data;

namespace BlockchainSQL.Web {
	public class SiteOptions {
		public bool EnableGoogleAnalytics { get; set; }

		public string GoogleTrackingCode { get; set; }

		public bool ShowProductPage { get; set; }

		public bool ShowCompanyAboutPage { get; set; }

		public string AdminUsername { get; set; }

		public string AdminPassword { get; set; }

		public string SMTPServer { get; set; }

		public int SMTPPort { get; set; }

		public string SMTPUsername { get; set; }

		public string SMTPPassword { get; set; }

		public string ContactRecipientEmail { get; set; }

		public string ContactFromEmail { get; set; }

	}
}
