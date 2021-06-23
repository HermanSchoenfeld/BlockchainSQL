using System.ComponentModel.DataAnnotations;

namespace BlockchainSQL.Web.Models {
	public class AuthForm {
		
		[Required]
		public string Password { get; set; }
	}
}
