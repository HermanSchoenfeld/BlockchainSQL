// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace BlockchainSQL.Web.Code {
	public class ViewLocationExpander : IViewLocationExpander {

		public void PopulateValues(ViewLocationExpanderContext context) {
			context.Values["customviewlocation"] = nameof(ViewLocationExpander);
		}

		public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations) {
			string[] locations = {
				"~/Views/Forms/{0}.cshtml",
				"~/Views/Shared/Forms/{0}.cshtml"
			};
			return locations.Union(viewLocations); //Add mvc default locations after ours
		}
	}
}
