using Sphere10.Framework.Web.AspNetCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlockchainSQL.Web.Models {

	public class ContactFormInput : FormModelBase {

		public override string FormName => "Contact";

		[Required]
		[DisplayName("Name")]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		[DisplayName("Email")]
		public string Email { get; set; }

		[Required]
		[DisplayName("Message")]
		public string Message { get; set; }

	}
}