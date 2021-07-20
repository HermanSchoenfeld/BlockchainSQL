using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.Processing;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows;
using static BlockchainSQL.Server.ServiceInstallDialog;

namespace BlockchainSQL.Server {
	partial class BlockchainSQLService : ServiceBase {
		private readonly ILogger _logger;
		private readonly CancellationTokenSource _cancellationTokenSource;
		private readonly ManualResetEventSlim _waiter;
		private Process _webAppProcess;
		private FormSettings _settings;

		public BlockchainSQLService() {
			InitializeComponent();
			_cancellationTokenSource = new CancellationTokenSource();
			_logger = new EventLogLogger("BlockchainSQL Server", "Application") {
				Options = LogOptions.WarningEnabled | LogOptions.ErrorEnabled
			};
			if (Tools.Config.GetAppSetting<bool>("LogInfo"))
				_logger.Options = _logger.Options | LogOptions.InfoEnabled;
			_waiter = new ManualResetEventSlim();
			AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
				if (_logger == null) return;
				var exception = args.ExceptionObject as Exception;
				_logger.Error(string.Format("FATAL ERROR: {0}", (exception.ToDiagnosticString() ?? (args.ExceptionObject?.ToString() ?? "NULL"))));
			};
		}

		protected override async void OnStart(string[] args) {
			_logger.Info("Starting");
			_settings = GlobalSettings.Get<FormSettings>();
			
			while (!_cancellationTokenSource.IsCancellationRequested) {
				try {
					var serviceExePath = Assembly.GetEntryAssembly().Location;
					var dbSettings = GlobalSettings.Get<BlockchainDatabaseSettings>();
					if (dbSettings.DBMSType != DBMSType.SQLServer)
						throw new NotSupportedException($"Database type not supported {dbSettings.DBMSType}");

					var database = new DBReference() { DBMSType = dbSettings.DBMSType, ConnectionString = dbSettings.ConnectionString };

					if (IsWebAppEnabled())
						StartWebUI();

					await StartScanning(database, _logger, _cancellationTokenSource.Token);
				} catch (OperationCanceledException) {
				} catch (Exception error) {
					_logger.LogException(error);
					_logger.Warning("Will restart scanning in 1 minute");
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
			_logger.Info("Stopping");
			try {
				StopWebApplication();
				_cancellationTokenSource.Cancel();

				if (!_waiter.IsSet)
					await Task.Run(() => _waiter.Wait());
			} catch (Exception error) {
				_logger.LogException(error);
			}
		}

		private async Task StartScanning(DBReference database, ILogger logger, CancellationToken cancelToken) {
			var databaseValidation = await TestDatabase(database);
			if (!databaseValidation.Success)
				throw new ApplicationException("Failed to connect to database. " + databaseValidation.ErrorMessages.ToParagraphCase());

			var dbmsType = database.DBMSType;
			var connectionString = database.ConnectionString;

			using (var scope = new BizLogicScope(dbmsType, connectionString, _logger)) {
				var nodeIP = scope.Settings.Get<NodeSettings>().IP;
				int? nodePort = null; // TODO add port setting
				var nodeValidation = await ValidateNode(nodeIP, nodePort);
				if (!nodeValidation.Success)
					throw new ApplicationException("Failed to connect to Bitcoin node. " + nodeValidation.ErrorMessages.ToParagraphCase());

				using (var nodeStream = BizLogicFactory.NewNodeBlockStream(NodeEndpoint.For(nodeIP, nodePort))) {
					var blockStreamParser = BizLogicFactory.NewNodeStreamParser(nodeStream, BizLogicFactory.NewBlockLocator(), BizLogicFactory.NewPreProcessor(false, true), BizLogicFactory.NewPostProcessor(), BizLogicFactory.NewBlockStreamPersistor());
					Action<int> progressHandler = i => Tools.Lambda.NoOp();
					await blockStreamParser.Parse(cancelToken, progressHandler, false,  TimeSpan.FromSeconds(scope.Settings.Get<NodeSettings>().PollRateSEC));
				}
			}
		}


		protected virtual async Task<Result> TestDatabase(DBReference database) {
			var result = Result.Default;
			try {
				await Task.Run(() => {
					var dac = DACFactory.CreateDAC(database.DBMSType, database.ConnectionString, _logger);
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

			var url = $"http://*:{_settings.WebUIPort}";

			if (File.Exists(dllPath)) {
				ProcessStartInfo startInfo = new ProcessStartInfo() {
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
				_logger.Warning($"Web UI is enabled but did not find executable at expected path {dllPath}");
		}

		private void StopWebApplication() {
			_webAppProcess?.Kill(true);
		}

		private void WebUIProcess_Exited(object sender, EventArgs e) {
			_logger.Info("BlockchainSQL.Web process has exited.");
		}

		private string GetWebUIDllPath() {
			var installFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			return Path.Join(installFolder, "/Web/BlockchainSQL.Web.dll");
		}

		private bool IsWebAppEnabled() => _settings?.IsWebUIEnabled ?? false;
	}
}

