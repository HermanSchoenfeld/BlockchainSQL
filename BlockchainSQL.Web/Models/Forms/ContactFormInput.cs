// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sphere10.Framework.Web.AspNetCore;

namespace BlockchainSQL.Web.Models {

	public class ContactFormInput : AweFormModelBase {

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