// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.Processing;
using BlockchainSQL.Web.DataAccess;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainSQL.Server.Controls {
	public partial class WebSettingsControl : UserControlEx {
		private WebSettings _model;

		public WebSettingsControl() {
			InitializeComponent();
			_blockchainDatabaseConnectionPanel.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
		}

		public WebSettings Model {
			get => _model;
			set {
				_model = value;
				if (_model != null)
					using (EnterUpdateScope(FinishedUpdateBehaviour.DoNothing))
						CopyModelToUI();
				else
					ClearUI();
			}
		}

		public DatabaseConnectionPanel WebDatabasePanel => _webDatabaseConnectionPanel;

		public DatabaseConnectionPanel BlockchainDatabasePanel => _blockchainDatabaseConnectionPanel;

		protected override void CopyModelToUI() {
			if (Model == null)
				return;
			_enableWebUICheckBox.Checked = Model.Enabled;
			_portIntBox.Value = Model.Port;
			_webDatabaseConnectionPanel.SelectedDBMSType = Model.WebDBMSType;
			_webDatabaseConnectionPanel.ConnectionString = Model.WebDatabaseConnectionString;
			_blockchainDatabaseConnectionPanel.SelectedDBMSType = Model.BlockchainDBMSType;
			_blockchainDatabaseConnectionPanel.ConnectionString = Model.BlockchainDatabaseConnectionString;
		}

		protected override void CopyUIToModel() {
			Guard.Ensure(_model != null, "Model not set");
			Model.Enabled = _enableWebUICheckBox.Checked;
			Model.Port = _portIntBox.Value.GetValueOrDefault(5000);
			_model.WebDBMSType = _webDatabaseConnectionPanel.SelectedDBMSType;
			_model.WebDatabaseConnectionString = _webDatabaseConnectionPanel.ConnectionString;
			_model.BlockchainDBMSType = _blockchainDatabaseConnectionPanel.SelectedDBMSType;
			_model.BlockchainDatabaseConnectionString = _blockchainDatabaseConnectionPanel.ConnectionString;
		}

		private void ClearUI() {
			_enableWebUICheckBox.Checked = false;
			_portIntBox.Value = null;
			_webDatabaseConnectionPanel.SelectedDBMSType = DBMSType.SQLServer;
			_webDatabaseConnectionPanel.ConnectionString = string.Empty;
			_blockchainDatabaseConnectionPanel.SelectedDBMSType = DBMSType.SQLServer;
			_blockchainDatabaseConnectionPanel.ConnectionString = string.Empty;
		}

		private async Task<Result> TestWebDatabaseConnection() {
			var result = Result.Default;
			try {
				this.EnableChildren(false);
				_loadingCircle.Enabled = true;
				_loadingCircle.Active = true;
				result = await _webDatabaseConnectionPanel.TestConnection();
			} finally {
				this.EnableChildren(true);
				_loadingCircle.Active = false;
			}
			return result;
		}

		private async Task<Result> TestBlockchainDatabaseConnection() {
			var result = Result.Default;
			try {
				this.EnableChildren(false);
				_loadingCircle.Enabled = true;
				_loadingCircle.Active = true;
				result = await _blockchainDatabaseConnectionPanel.TestConnection();
			} finally {
				this.EnableChildren(true);
				_loadingCircle.Active = false;
			}
			return result;
		}

		private async Task<bool> GenerateWebDatabase(DatabaseGenerationDataPolicy dataPolicy, DBMSType dbmsType, string connectionString, string databaseName) {
			using (_loadingCircle.BeginAnimationScope()) {
				var dropExisting = false;
				var createShell = false;
				var createDatabase = false;
				var schemaGenerator = WebDatabase.NewDatabaseGenerator(dbmsType);
				if (await Task.Run(() => schemaGenerator.DatabaseExists(connectionString))) {
					switch (
						QuestionDialog.Show(
							this,
							SystemIconType.Warning,
							"Database Already Exists",
							"Database already exists. What would you like to do?",
							"&Cancel", "&Overwrite", "&Append")
						) {
						case DialogExResult.Button1:
						return false;
						case DialogExResult.Button2:
						dropExisting = true;
						createShell = true;
						createDatabase = true;
						break;
						case DialogExResult.Button3:
						createDatabase = true;
						break;
					}
				} else {
					createShell = true;
					createDatabase = true;
				}

				if (dropExisting)
					await Task.Run(() => schemaGenerator.DropDatabase(connectionString));

				if (createShell)
					await Task.Run(() => schemaGenerator.CreateEmptyDatabase(connectionString));

				if (createDatabase)
					await Task.Run(() => schemaGenerator.CreateApplicationDatabase(connectionString, dataPolicy, databaseName));

				DialogEx.Show(this, "Success", "Database Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

				return true;
			}
		}
		private void _enableWebUICheckBox_CheckedChanged(object sender, EventArgs e) {
			try {
				_webDatabaseConnectionPanel.Enabled = 
					_blockchainDatabaseConnectionPanel.Enabled = 
						_portLabel.Enabled =
							_portIntBox.Enabled = 
								_webDBGroupBox.Enabled = 
									_testWebDatabaseButton.Enabled =
										_generateWebDatabaseButton.Enabled =
											_bsqlDBGroupBox.Enabled = 
												_testBlockchainDatabaseButton.Enabled =
													_enableWebUICheckBox.Checked;
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private async void _generateWebDatabaseButton_Click(object sender, EventArgs e) {
			try {
				using (_loadingCircle.BeginAnimationScope(this)) {
					var dbmsType = _webDatabaseConnectionPanel.SelectedDBMSType;
					var connectionString = _webDatabaseConnectionPanel.ConnectionString;
					var databaseName = _webDatabaseConnectionPanel.DatabaseName;
					await GenerateWebDatabase(DatabaseGenerationDataPolicy.PrimingData, dbmsType, connectionString, databaseName);
				}
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private async void _testWebDatabaseButton_Click(object sender, EventArgs e) {
			try {
				Result testResult;
				using (_loadingCircle.BeginAnimationScope(this)) {
					testResult = await TestWebDatabaseConnection();
				}
				if (testResult.IsFailure)
					DialogEx.Show(this, testResult.ErrorMessages.ToParagraphCase(), "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				else
					DialogEx.Show(this, "Success", "Connection Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private async void _testBlockchainDatabaseButton_Click(object sender, EventArgs e) {
			try {
				Result testResult;
				using (_loadingCircle.BeginAnimationScope(this)) {
					testResult = await TestBlockchainDatabaseConnection();
				}
				if (testResult.IsFailure)
					DialogEx.Show(this, testResult.ErrorMessages.ToParagraphCase(), "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				else
					DialogEx.Show(this, "Success", "Connection Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}


		private void _splitContainer_SizeChanged(object sender, EventArgs e) {
			try {
				_splitContainer.SplitterDistance = _splitContainer.Width / 2;
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

	}
}
