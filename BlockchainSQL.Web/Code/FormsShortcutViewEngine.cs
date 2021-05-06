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
