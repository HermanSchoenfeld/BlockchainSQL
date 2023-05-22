using System;
using System.ComponentModel;
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
	public partial class NetworkBlockImporterForm : FormEx {
		private CancellationTokenSource _cancellationTokenSource;
		private readonly ILogger _logger;
		public NetworkBlockImporterForm() {
			InitializeComponent();
			_dbConnectionBar.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
			_cancellationTokenSource = new CancellationTokenSource();
			_logger = new AsyncLogger(new MulticastLogger(new TextBoxLogger(_logBox), new ConsoleLogger())) {
				Options = LogOptions.InfoEnabled | LogOptions.WarningEnabled | LogOptions.ErrorEnabled
			};
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
				var settings = UserSettings.Get<NetworkScannerFormSettings>();
				_nodeIPTextBox.Text = settings.NodeIP;
				_nodePortBox.Value = settings.NodePort;
				_dbConnectionBar.SelectedDBMSType = settings.DBMS;
				_dbConnectionBar.ConnectionString = settings.ConnectionString;
			}
		}

		protected virtual void SaveFormSettings() {
			var settings = UserSettings.Get<NetworkScannerFormSettings>();
			settings.NodeIP = _nodeIPTextBox.Text;
			settings.NodePort = _nodePortBox.Value;
			settings.DBMS = _dbConnectionBar.SelectedDBMSType;
			settings.ConnectionString = _dbConnectionBar.ConnectionString;
			settings.Save();
		}

		protected virtual async Task TestDatabase() {
			using (_loadingCircle.BeginAnimationScope(this)) {
				var testResult = await _dbConnectionBar.TestConnection();
				if (testResult.Failure) {
					DialogEx.Show(this, testResult.ErrorMessages.ToParagraphCase(), "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else {
					DialogEx.Show(this, "Success", "Database Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		protected virtual async Task<Result> ValidateNode() {
			var result = Result<NodeEndpoint>.Default;
			NodeEndpoint endPoint;
			if (!NodeEndpoint.TryParse(_nodeIPTextBox.Text, _nodePortBox.Value, out endPoint)) {
				result.AddError("Node IP address is not a validly formatted IP adress");
			} else {
				using (_loadingCircle.BeginAnimationScope(this)) {
					return await NodeCommunicator.TestConnection(endPoint);
				}
			}
			return result;
		}

		protected virtual async Task StartScanning() {
			var blockchainSourceValidation = await ValidateNode();
			if (!blockchainSourceValidation.Success)
				throw new ApplicationException(blockchainSourceValidation.ErrorMessages.ToParagraphCase());
			var databaseValidation = await _dbConnectionBar.TestConnection();
			if (!databaseValidation.Success)
				throw new ApplicationException(databaseValidation.ErrorMessages.ToParagraphCase());

			var dbmsType = _dbConnectionBar.SelectedDBMSType;
			var connectionString = _dbConnectionBar.ConnectionString;
			using (var scope = new BizLogicScope(dbmsType, connectionString, _logger)) {
				using (var nodeStream = BizLogicFactory.NewNodeBlockStream(NodeEndpoint.For(_nodeIPTextBox.Text, _nodePortBox.Value))) {
					var blockStreamParser = BizLogicFactory.NewNodeStreamParser(nodeStream, BizLogicFactory.NewBlockLocator(), BizLogicFactory.NewPreProcessor(false, true), BizLogicFactory.NewPostProcessor(), BizLogicFactory.NewBlockStreamPersistor());
					Action<int> progressHandler = i => _progressBar.BeginInvokeEx(() => _progressBar.Value = i);
					SaveFormSettings();
					using (_loadingCircle.BeginAnimationScope(this, _startButton, _logBox, _progressBar)) {
						_progressBar.Visible = true;
						_startButton.Text = "Stop";
						await blockStreamParser.Parse(_cancellationTokenSource.Token, progressHandler, false, TimeSpan.FromSeconds(scope.Settings.Get<ServiceNodeSettings>().PollRateSEC));
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

		private async void _startButton_Click(object sender, EventArgs e) {
			try {
				if (!IsScanning) {
					try {
						using (new AlwaysOnScope(true, false))
							await StartScanning();
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
				_logger.Exception(error);
				_logger.Warning("Stopped scanning due to error");
				// ExceptionDialog.Show(this, error);
			}
		}

		private async void _testNodeButton_Click(object sender, EventArgs e) {
			try {
				var result = await ValidateNode();
				if (result.Failure)
					DialogEx.Show(this, result.ErrorMessages.ToParagraphCase(), "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
				else
					DialogEx.Show(this, "Success", "Connection Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}


		#endregion

	}

	public class NetworkScannerFormSettings : SettingsObject {

		public string NodeIP { get; set; }
		public int? NodePort { get; set; }
		public string BlockFilePath { get; set; }

		public DBMSType DBMS { get; set; } = DBMSType.SQLServer;


		public string ConnectionString { get; set; }

	}


}
