using BlockchainSQL.DataAccess;
using BlockchainSQL.Processing;
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

namespace BlockchainSQL.Server {
	public partial class BlockchainDatabaseSettingsControl : UserControl {
		private BlockchainDatabaseSettings _model;

		public BlockchainDatabaseSettingsControl() {
			InitializeComponent();
			_databaseConnectionPanel.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
		}

		public BlockchainDatabaseSettings Model {
			get => _model;
			set {
				_model = value;
				if (_model != null)
					CopyModelToUI();
				else
					ClearUI();
			}
		}

		private void CopyModelToUI() {
			Guard.Ensure(_model != null, "Model not set");
			_databaseConnectionPanel.SelectedDBMSType = _model.DBMSType;
			_databaseConnectionPanel.ConnectionString = _model.ConnectionString;
		}

		private void CopyUIToModel() {
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
				var schemaGenerator = BlockchainDatabase.NewDatabaseGenerator(dbmsType);
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



				MessageBox.Show(this, "Success", "Database Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
				Result testResult;
				using (_loadingCircle.BeginAnimationScope(this)) {
					testResult = await TestConnection();
				}
				if (testResult.Failure)
					MessageBox.Show(this, testResult.ErrorMessages.ToParagraphCase(), "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				else
					MessageBox.Show(this, "Success", "Connection Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

	}
}
