using System;
using System.Text.Encodings.Web;
using Sphere10.Framework;
using Sphere10.Framework.Web.AspNetCore;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Omu.AwesomeMvc;

namespace BlockchainSQL.Web {

	public class AweFormModelBase : FormModelBase {

		public virtual string FormName { get; set; }
	}
}