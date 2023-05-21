using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockchainSQL.Processing;
using Hydrogen;
using Hydrogen.Application;
using Hydrogen.Data;
using Hydrogen.Windows;
using Hydrogen.Windows.Forms;

namespace BlockchainSQL.Server {
	public partial class BlockFileScannerForm : FormEx {
		private CancellationTokenSource _cancellationTokenSource;
		public BlockFileScannerForm() {
			InitializeComponent();
			_dbConnectionBar.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
			_cancellationTokenSource = new CancellationTokenSource();
		}

		#region Form Logic

		public bool IsScanning {
			get { return _loadingCircle.Visible; }
		}

		protected override void PopulatePrimingData() {
			LoadFormSettings();
		}

		protected virtual void LoadFormSettings() {
			using (this.EnterUpdateScope()) {
				var settings = UserSettings.Get<FileScannerFormSettings>();
				_blkDataPathControl.Path = settings.BlockFilePath;
				_dbConnectionBar.SelectedDBMSType = settings.DBMS;
				_dbConnectionBar.ConnectionString = settings.ConnectionString;
				_disableIndexCheckBox.Checked = settings.DisableIndexes;
			}
		}

		protected virtual void SaveFormSettings() {
			var settings = UserSettings.Get<FileScannerFormSettings>();
			settings.BlockFilePath = _blkDataPathControl.Path;
			settings.DBMS = _dbConnectionBar.SelectedDBMSType;
			settings.ConnectionString = _dbConnectionBar.ConnectionString;
			settings.DisableIndexes = _disableIndexCheckBox.Checked;
			settings.Save();
		}

		protected virtual async Task TestDatabase() {
			using (_loadingCircle.BeginAnimationScope(this)) {
				var testResult = await _dbConnectionBar.TestConnection();
				if (testResult.Failure) {
					DialogEx.Show(SystemIconType.Information, "Connection Failed", testResult.ErrorMessages.ToParagraphCase(), "OK");
				} else {
					DialogEx.Show(this, "Success", "Database Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		protected virtual async Task<Result> ValidateBLKDataFolder() {
			using (BizLogicScope.EnterDatabaseFreeScope()) {
				return await Task.Run(() => ProcessingTierHelper.ValidateBlocksDirectory(_blkDataPathControl.Path));
			}
		}

		protected virtual async Task StartScanning() {
			var blockchainSourceValidation = await Task.Run(() => ProcessingTierHelper.ValidateBlocksDirectory(_blkDataPathControl.Path));
			if (!blockchainSourceValidation.Success)
				throw new ApplicationException(blockchainSourceValidation.ErrorMessages.ToParagraphCase());
			var databaseValidation = await _dbConnectionBar.TestConnection();
			if (!databaseValidation.Success)
				throw new ApplicationException(databaseValidation.ErrorMessages.ToParagraphCase());


			var dbmsType = _dbConnectionBar.SelectedDBMSType;
			var connectionString = _dbConnectionBar.ConnectionString;
			var disableIndexes = _disableIndexCheckBox.Checked;
			ILogger logger = new MulticastLogger(new TextBoxLogger(_logBox), new ConsoleLogger()) {
				Options = LogOptions.InfoEnabled | LogOptions.WarningEnabled | LogOptions.ErrorEnabled
			};

			using (var scope = new BizLogicScope(dbmsType, connectionString, logger)) {

				using (var blockStream = BizLogicFactory.NewBlockStream(_blkDataPathControl.Path)) {
					var postProcessor = BizLogicFactory.NewPostProcessor();
					var blockStreamParser = BizLogicFactory.NewBLKFileStreamParser(blockStream, BizLogicFactory.NewBlockLocator(), BizLogicFactory.NewPreProcessor(true, true), postProcessor, BizLogicFactory.NewBlockStreamPersistor());
					Action<int> progressHandler = i => _progressBar.BeginInvokeEx(() => _progressBar.Value = i);
					SaveFormSettings();

					using (_loadingCircle.BeginAnimationScope(this, _startButton, _logBox, _progressBar)) {
						_progressBar.Visible = true;
						_startButton.Text = "Stop";
						if (disableIndexes) {
							await Task.Run(() => scope.DAC.DisableAllApplicationIndexes());
							logger.Warning("Disabled database indexes (temporarily)");
						}
						try {
							await blockStreamParser.Parse(_cancellationTokenSource.Token, progressHandler, true);
						} catch (Exception e) {
							logger.Exception(e);
						} finally {
							if (disableIndexes) {
								logger.Warning("Enabling database indexes (this can take a very long time up to 24 hours)");
								await Task.Run(() => scope.DAC.EnableAllApplicationIndexes());
							}
							logger.Info("Running post-processing tasks");
							await Task.Run(() => postProcessor.PostProcessAll());
							logger.Warning("Shrinking database");
							await Task.Run(() => scope.DAC.CleanupDatabase());
							logger.Info("Finished");
						}
					}
				}
			}
		}

		protected override void OnClosing(CancelEventArgs e) {
			if (_loadingCircle.Visible) {
				e.Cancel = true;
			} else {
				GC.Collect();
			}

			base.OnClosing(e);
		}

		#endregion

		#region Event Handlers

		private async void _testDatabaseButton_Click(object sender, EventArgs e) {
			try {
				await TestDatabase();
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private async void _BLKDataFolderValidator_PerformValidation(ValidationIndicator arg1, ValidationIndicatorEvent arg2) {
			try {
				var result = await ValidateBLKDataFolder();
				arg2.ValidationResult = result.Success;
				arg2.ValidationMessage = result.GetMessages().ToParagraphCase();
			} catch (Exception error) {
				ExceptionDialog.Show(error);
			}
		}

		private void _blkDataPathControl_PathChanged() {
			try {
				_BLKDataFolderValidator.RunValidation();
			} catch (Exception error) {
				ExceptionDialog.Show(error);
			}
		}

		private async void _startButton_Click(object sender, EventArgs e) {
			try {
				if (!IsScanning) {
					try {
						using (new AlwaysOnScope(true, false)) {
							await StartScanning();
						}
					} finally {
						_progressBar.Value = 0;
						_progressBar.Visible = false;
						_startButton.Text = "Start";
						_startButton.Enabled = true;
					}
				} else {
					_startButton.Text = "Cancelling";
					_startButton.Enabled = false;
					_cancellationTokenSource.Cancel();
					_cancellationTokenSource = new CancellationTokenSource();
				}
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		#endregion

	}

	public class FileScannerFormSettings : SettingsObject {
		public string BlockFilePath { get; set; }

		public DBMSType DBMS { get; set; } = DBMSType.SQLServer;

		public string ConnectionString { get; set; }

		public bool DisableIndexes { get; set; } = true;

	}
}
