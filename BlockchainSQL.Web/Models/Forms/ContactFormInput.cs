using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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