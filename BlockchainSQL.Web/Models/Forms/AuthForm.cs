// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.ComponentModel.DataAnnotations;
using Sphere10.Framework.Web.AspNetCore;

namespace BlockchainSQL.Web.Models {
	public class LoginForm : AweFormModelBase {

		[Required] 
		public string Username { get; set; }

		public string Password { get; set; }

		public override string FormName => "Login";
	}
}
