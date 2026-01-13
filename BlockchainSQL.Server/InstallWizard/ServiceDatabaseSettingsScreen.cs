// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using Sphere10.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockchainSQL.DataAccess;
using Sphere10.Framework.Windows.Forms;
using BlockchainSQL.Processing;

namespace BlockchainSQL.Server {
	public partial class ServiceDatabaseSettingsScreen : InstallWizardScreenBase {
		public ServiceDatabaseSettingsScreen() {
			InitializeComponent();
		}

		protected override void CopyModelToUI() {
			_blockchainDatabaseSettingsControl.Model = new ServiceDatabaseSettings {
				DBMSType = Model.ServiceDatabaseSettings.DBMSType,
				ConnectionString = Model.ServiceDatabaseSettings.ConnectionString,
			};
		}

		protected override void CopyUIToModel() {
			Model.ServiceDatabaseSettings.DBMSType = _blockchainDatabaseSettingsControl.Model.DBMSType;
			Model.ServiceDatabaseSettings.ConnectionString = _blockchainDatabaseSettingsControl.Model.ConnectionString;
		}

		public override Task<Result> Validate() {
			return ValidateDatabase();
		}

		public async Task<Result> ValidateDatabase() {
			var result = await _blockchainDatabaseSettingsControl.DatabasePanel.TestConnection();
			if (result.IsSuccess) {
				var dac = BlockchainDatabase.NewDAC(_blockchainDatabaseSettingsControl.DatabasePanel.GetDAC());
				if (!dac.IsValidSchema())
					result.AddError("BlockchainSQL Database schema is not valid. Try regenerating.");
			}
			return result;
		}

		public override async Task OnNext() {
			await base.OnNext();
			Model.ServiceDatabaseSettings = _blockchainDatabaseSettingsControl.Model;
			Model.WebSettings.BlockchainDBMSType = _blockchainDatabaseSettingsControl.Model.DBMSType;
			Model.WebSettings.BlockchainDatabaseConnectionString = _blockchainDatabaseSettingsControl.Model.ConnectionString;
		}

	}
}
