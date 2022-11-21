using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Hydrogen.Web.AspNetCore;

namespace BlockchainSQL.Web.Models {

	public class SubscribeFormInput : FormModelBase {

		public override string FormName => "Subscribe";

		[Required]
		[EmailAddress]
		[DisplayName("Email")]
		public string Email { get; set; }


	}
}