// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using Sphere10.Framework.Web.AspNetCore;

namespace BlockchainSQL.Web.Models {
	public class ConfigureDatabaseFormInput : AweFormModelBase {
		public override string FormName => "ConfigureDatabases";

		public ConfigureBlockchainDbFormModel BlockchainDbModel { get; init; } = new ();

		public ConfigureWebDatabaseFormModel WebDbModel { get; init; } = new();
	}
}