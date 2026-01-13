// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.DataAccess;
using BlockchainSQL.Processing;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainSQL.Server {
	public partial class ServiceDatabaseSettingsControl : UserControlEx {
		private ServiceDatabaseSettings _model;

		public ServiceDatabaseSettingsControl() {
			InitializeComponent();
			_databaseConnectionPanel.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
		}

		public ServiceDatabaseSettings Model {
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

		public DatabaseConnectionPanel DatabasePanel => _databaseConnectionPanel;

		protected override void CopyModelToUI() {
			if (Model != null) {
				_databaseConnectionPanel.SelectedDBMSType = _model.DBMSType;
				_databaseConnectionPanel.ConnectionString = _model.ConnectionString;
			} else ClearUI();
		}

		protected override void CopyUIToModel() {
			Guard.Ensure(_model != null, "Model not set");
			_model.DBMSType = _databaseConnectionPanel.SelectedDBMSType;
			_model.ConnectionString = _databaseConnectionPanel.ConnectionString;
		}

		private void ClearUI() {
			_databaseConnectionPanel.SelectedDBMSType = DBMSType.SQLServer;
			_databaseConnectionPanel.ConnectionString = string.Empty;
		}

		private async Task<Result> TestConnection() {
			var result = Result.Default;
			try {
				this.EnableChildren(false);
				_loadingCircle.Enabled = true;
				_loadingCircle.Active = true;
				result = await _databaseConnectionPanel.TestConnection();
			} finally {
				this.EnableChildren(true);
				_loadingCircle.Active = false;
			}
			return result;
		}

		private async Task<bool> GenerateDatabase(DatabaseGenerationDataPolicy dataPolicy, DBMSType dbmsType, string connectionString, string databaseName) {
			using (_loadingCircle.BeginAnimationScope()) {
				var dropExisting = false;
				var createShell = false;
				var createDatabase = false;
				var schemaGenerator = BlockchainDatabase.NewDatabaseManager(dbmsType);
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

		private async void _generateDatabaseButton_Click(object sender, EventArgs e) {
			try {
				using (_loadingCircle.BeginAnimationScope(this)) {
					var dbmsType = _databaseConnectionPanel.SelectedDBMSType;
					var connectionString = _databaseConnectionPanel.ConnectionString;
					var databaseName = _databaseConnectionPanel.DatabaseName;
					await GenerateDatabase(DatabaseGenerationDataPolicy.PrimingData, dbmsType, connectionString, databaseName);
				}
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private async void _testButton_Click(object sender, EventArgs e) {
			try {
				using (_loadingCircle.BeginAnimationScope(this)) {
					var result = await TestConnection();
					if (result.IsSuccess) {
						var dac = BlockchainDatabase.NewDAC(_databaseConnectionPanel.GetDAC());
						if (!dac.IsValidSchema())
							result.AddError("Database schema is not valid. It has either been altered or is old. Generating a new database will resolve the problem. If 'Overwrite' is selected, current data will be lost. If 'Append' is selected, data will be kept and schema will be repaired (however data consistency may be inconsistent due to current state).  Proceed with caution.");
					}

					if (result.IsFailure)
						DialogEx.Show(this,
							result.ErrorMessages.ToParagraphCase(),
							"Connection Failed",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error);
					else {
						DialogEx.Show(this,
							"Success",
							"Connection Succeeded",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
					}
				}
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

	}
}
