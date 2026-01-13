// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using System.ComponentModel;

namespace BlockchainSQL.Processing {
	public class WebSettings : SettingsObject {
		public bool Enabled { get; set; } = true;

		public int Port { get; set; } = 5000;

		public DBMSType WebDBMSType { get; set; } = DBMSType.SQLServer;

		[EncryptedString]
		public string WebDatabaseConnectionString { get; set; } = "";


		public DBMSType BlockchainDBMSType { get; set; } = DBMSType.SQLServer;

		[EncryptedString]
		public string BlockchainDatabaseConnectionString { get; set; } = "";

		public bool SaveQueries { get; set; } = true;

	}
}


