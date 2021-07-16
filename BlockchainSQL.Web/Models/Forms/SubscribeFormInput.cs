using Sphere10.Framework.Web.AspNetCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlockchainSQL.Web.Models {

	public class SubscribeFormInput : FormModelBase {

		public override string FormName => "Subscribe";

		[Required]
		[EmailAddress]
		[DisplayName("Email")]
		public string Email { get; set; }


	}
}