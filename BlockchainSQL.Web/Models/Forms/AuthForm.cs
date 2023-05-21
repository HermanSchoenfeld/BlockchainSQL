using System.ComponentModel.DataAnnotations;
using Hydrogen.Web.AspNetCore;

namespace BlockchainSQL.Web.Models {
	public class LoginForm : AweFormModelBase {

		[Required] 
		public string Username { get; set; }

		public string Password { get; set; }

		public override string FormName => "Login";
	}
}
