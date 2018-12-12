using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BlockchainSQL.Web.Models {

    public abstract class FormModelBase {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; } = Guid.NewGuid();

        public abstract string FormName { get; }
    }

   
}