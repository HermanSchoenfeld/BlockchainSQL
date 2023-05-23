using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.Processing;
using Hydrogen;
using Hydrogen.Application;
using Hydrogen.Data;
using Hydrogen.Windows;


namespace BlockchainSQL.Server {
	partial class BlockchainSQLService : ServiceBase {
		private readonly CancellationTokenSource _cancellationTokenSource;
		private readonly ManualResetEventSlim _waiter;
		private Process _webAppProcess;

		public BlockchainSQLService() {
			InitializeComponent();
			_cancellationTokenSource = new CancellationTokenSource();
			// Register event logger for service
			SystemLog.RegisterLogger(new EventLogLogger("BlockchainSQL Server", "Application") {
				Options = LogOptions.WarningEnabled | LogOptions.ErrorEnabled
			});
			//if (Tools.Config.GetAppSetting<bool>("LogInfo"))
			//	_logger.Options = _logger.Options | LogOptions.InfoEnabled;
			_waiter = new ManualResetEventSlim();
			AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
				var exception = args.ExceptionObject as Exception;
				SystemLog.Error(string.Format("FATAL ERROR: {0}", (exception.ToDiagnosticString() ?? (args.ExceptionObject?.ToString() ?? "NULL"))));
			};
		}

		protected override async void OnStart(string[] args) {
			SystemLog.Info("Starting");
			
			while (!_cancellationTokenSource.IsCancellationRequested) {
				try {
					var serviceExePath = Assembly.GetEntryAssembly().Location;
					var dbSettings = GlobalSettings.Get<ServiceDatabaseSettings>();
					if (dbSettings.DBMSType != DBMSType.SQLServer)
						throw new NotSupportedException($"Database type not supported {dbSettings.DBMSType}");

					var database = new DBReference() { DBMSType = dbSettings.DBMSType, ConnectionString = dbSettings.ConnectionString };

					if (IsWebAppEnabled())
						StartWebUI();

					await StartScanning(database, SystemLog.Logger, _cancellationTokenSource.Token);
				} catch (OperationCanceledException) {
				} catch (Exception error) {
					SystemLog.Exception(error);
					SystemLog.Warning("Will restart scanning in 1 minute");
					try {
						await Task.Delay(TimeSpan.FromMinutes(1), _cancellationTokenSource.Token);
					} catch {
						// ignored
					}
				}
			}
			_waiter.Set();
		}

		protected override async void OnStop() {
			SystemLog.Info("Stopping");
			try {
				StopWebApplication();
				_cancellationTokenSource.Cancel();

				if (!_waiter.IsSet)
					await Task.Run(() => _waiter.Wait());
			} catch (Exception error) {
				SystemLog.Exception(error);
			}
		}

		private async Task StartScanning(DBReference database, ILogger logger, CancellationToken cancelToken) {
			var databaseValidation = await TestDatabase(database);
			if (!databaseValidation.Success)
				throw new ApplicationException("Failed to connect to database. " + databaseValidation.ErrorMessages.ToParagraphCase());

			var dbmsType = database.DBMSType;
			var connectionString = database.ConnectionString;

			using (var scope = new BizLogicScope(dbmsType, connectionString, SystemLog.Logger)) {
				var nodeIP = scope.Settings.Get<ServiceNodeSettings>().IP;
				int? nodePort = null; // TODO add port setting
				var nodeValidation = await ValidateNode(nodeIP, nodePort);
				if (!nodeValidation.Success)
					throw new ApplicationException("Failed to connect to Bitcoin node. " + nodeValidation.ErrorMessages.ToParagraphCase());

				using (var nodeStream = BizLogicFactory.NewNodeBlockStream(NodeEndpoint.For(nodeIP, nodePort), 1)) {
					var blockStreamParser = BizLogicFactory.NewNodeStreamParser(nodeStream, BizLogicFactory.NewBlockLocator(), BizLogicFactory.NewPreProcessor(false, true), BizLogicFactory.NewPostProcessor(), BizLogicFactory.NewBlockStreamPersistor());
					Action<int> progressHandler = i => Tools.Lambda.NoOp();
					await blockStreamParser.Parse(cancelToken, progressHandler, false,  TimeSpan.FromSeconds(scope.Settings.Get<ServiceNodeSettings>().PollRateSEC));
				}
			}
		}


		protected virtual async Task<Result> TestDatabase(DBReference database) {
			var result = Result.Default;
			try {
				await Task.Run(() => {
					var dac = DACFactory.CreateDAC(database.DBMSType, database.ConnectionString, SystemLog.Logger);
					dac.CreateOpenConnection();
				});
			} catch (Exception error) {
				result.AddException(error);
			}
			return result;
		}

		protected virtual async Task<Result> ValidateNode(string ipAddress, int? port) {
			var result = Result<NodeEndpoint>.Default;
			NodeEndpoint endPoint;
			if (!NodeEndpoint.TryParse(ipAddress, port, out endPoint)) {
				result.AddError("Node IP address '{0}' is not a validly formatted IP adress", ipAddress ?? "(NULL");
			} else {
				return await NodeCommunicator.TestConnection(endPoint);
			}
			return result;
		}

		private void StartWebUI() {
			var dllPath = GetWebUIDllPath();

			var webSettings = GlobalSettings.Get<WebSettings>();
			var url = $"http://*:{webSettings.Port}";

			if (File.Exists(dllPath)) {
				var startInfo = new ProcessStartInfo {
					FileName = "dotnet",
					ArgumentList = { $"{dllPath}", "--urls", $"{url}"},
					WorkingDirectory = Path.GetDirectoryName(dllPath)
				};

				Process process = new Process();
				process.EnableRaisingEvents = true;
				process.Exited += WebUIProcess_Exited;
				process.StartInfo = startInfo;

				if (!process.Start())
					throw new InvalidOperationException("BlockchainSQL.Web process failed to start");
				else
					_webAppProcess = process;
			} else
				SystemLog.Warning($"Web UI is enabled but did not find executable at expected path {dllPath}");
		}

		private void StopWebApplication() {
			_webAppProcess?.KillTree(TimeSpan.FromSeconds(10));
		}

		private void WebUIProcess_Exited(object sender, EventArgs e) {
			SystemLog.Info("BlockchainSQL.Web process has exited.");
		}

		private string GetWebUIDllPath() {
			var installFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			return Path.Join(installFolder, "/Web/BlockchainSQL.Web.dll");
		}

		private bool IsWebAppEnabled() => GlobalSettings.Get<WebSettings>().Enabled;
	}
}

