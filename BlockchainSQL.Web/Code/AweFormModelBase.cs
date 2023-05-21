using System;
using System.Text.Encodings.Web;
using Hydrogen;
using Hydrogen.Web.AspNetCore;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Omu.AwesomeMvc;

namespace BlockchainSQL.Web {

	public class AweFormModelBase : FormModelBase {

		public virtual string FormName { get; set; }
	}
}