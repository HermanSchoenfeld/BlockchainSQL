using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainSQL.Web.Models
{

    public abstract class FormModelBase {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid ID { get; set; } = Guid.NewGuid();

        public abstract string FormName { get; }
    }
}