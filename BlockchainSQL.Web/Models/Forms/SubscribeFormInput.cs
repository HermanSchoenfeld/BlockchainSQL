using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sphere10.Framework.Web.AspNetCore;

namespace BlockchainSQL.Web.Models {

	public class SubscribeFormInput : AweFormModelBase {

		public override string FormName => "Subscribe";

		[Required]
		[EmailAddress]
		[DisplayName("Email")]
		public string Email { get; set; }


	}
}