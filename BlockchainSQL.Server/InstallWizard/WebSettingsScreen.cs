// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.Processing;
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

namespace BlockchainSQL.Server {
	public partial class WebSettingsScreen : InstallWizardScreenBase {
		public WebSettingsScreen() {
			InitializeComponent();
		}

		public override async Task<Result> Validate() {
			var result = Model.WebSettings.Validate();
			if (result.IsFailure)
				return result;
			if (Model.WebSettings.Enabled)
				result = await ValidateDatabase();
			return result;
		}

		public async Task<Result> ValidateDatabase() {
			var result = Result.Default;
			;
			var webResult = await _webSettingsControl.WebDatabasePanel.TestConnection();
			if (webResult.IsFailure) {
				result.AddError("Unable to connect to web database");
				result.Merge(webResult);
			}

			var bsqlResult = await _webSettingsControl.BlockchainDatabasePanel.TestConnection();
			if (bsqlResult.IsFailure) {
				result.AddError("Unable to connect to blockchain database");
				result.Merge(bsqlResult);
			}

			return result;
		}

		protected override void CopyModelToUI() {
			_webSettingsControl.Model = new WebSettings {
				Enabled = Model.WebSettings.Enabled,
				Port = Model.WebSettings.Port,
				WebDBMSType = Model.WebSettings.WebDBMSType,
				WebDatabaseConnectionString = Model.WebSettings.WebDatabaseConnectionString,
				BlockchainDBMSType = Model.WebSettings.BlockchainDBMSType,
				BlockchainDatabaseConnectionString = Model.WebSettings.BlockchainDatabaseConnectionString
			};
		}

		protected override void CopyUIToModel() {
			Model.WebSettings.Enabled = _webSettingsControl.Model.Enabled;
			Model.WebSettings.Port = _webSettingsControl.Model.Port;
			Model.WebSettings.WebDBMSType = _webSettingsControl.Model.WebDBMSType;
			Model.WebSettings.WebDatabaseConnectionString = _webSettingsControl.Model.WebDatabaseConnectionString;
			Model.WebSettings.BlockchainDBMSType = _webSettingsControl.Model.BlockchainDBMSType;
			Model.WebSettings.BlockchainDatabaseConnectionString = _webSettingsControl.Model.BlockchainDatabaseConnectionString;
		}

	}
}
