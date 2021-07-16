using Sphere10.Framework.Web.AspNetCore;
using System.ComponentModel.DataAnnotations;

namespace BlockchainSQL.Web.Models {
	public class LoginForm : FormModelBase {

		[Required] public string Username { get; set; }

		[Required] public string Password { get; set; }

		public override string FormName => "Login";
	}
}
