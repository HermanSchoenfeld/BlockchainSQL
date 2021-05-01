using System;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.Processing;
using BlockchainSQL.Server.Service;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows;

namespace BlockchainSQL.Server
{
    partial class BlockchainSQLService : ServiceBase
    {
        private readonly ILogger _logger;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ManualResetEventSlim _waiter;

        public BlockchainSQLService()
        {
            InitializeComponent();
            _cancellationTokenSource = new CancellationTokenSource();
            _logger = new EventLogLogger("BlockchainSQL Server", "Application")
            {
                Options = LogOptions.WarningEnabled | LogOptions.ErrorEnabled
            };
            if (Tools.Config.GetAppSetting<bool>("LogInfo"))
                _logger.Options = _logger.Options | LogOptions.InfoEnabled;
            _waiter = new ManualResetEventSlim();
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                if (_logger == null) return;
                var exception = args.ExceptionObject as Exception;
                _logger.Error("FATAL ERROR: {0}", (exception.ToDiagnosticString() ?? (args.ExceptionObject?.ToString() ?? "NULL")));
            };
        }

        protected override async void OnStart(string[] args)
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    var serviceExePath = Assembly.GetEntryAssembly().Location;
                    var database = await DatabaseReferenceFileManager.LoadDatabaseConnectionFile(serviceExePath);
                    await StartScanning(database, _logger, _cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception error)
                {
                    _logger.LogException(error);
                    _logger.Warning("Will restart scannning in 1 minute");
                    try
                    {
                        await Task.Delay(TimeSpan.FromMinutes(1), _cancellationTokenSource.Token);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            _waiter.Set();
        }

        protected override async void OnStop()
        {
            try
            {
                _cancellationTokenSource.Cancel();
                if (!_waiter.IsSet)
                    await Task.Run(() => _waiter.Wait());
            }
            catch (Exception error)
            {
                _logger.LogException(error);
            }
        }

        private async Task StartScanning(DBReference database, ILogger logger, CancellationToken cancelToken)
        {
            var databaseValidation = await TestDatabase(database);
            if (!databaseValidation.Success)
                throw new ApplicationException("Failed to connect to database. " + databaseValidation.ErrorMessages.ToParagraphCase());

            var dbmsType = database.DBMSType;
            var connectionString = database.ConnectionString;

            using (var scope = new BizLogicScope(dbmsType, connectionString, _logger))
            {
                var nodeIP = scope.Settings.NetworkPeer1;
                int? nodePort = null; // TODO add port setting
                var nodeValidation = await ValidateNode(nodeIP, nodePort);
                if (!nodeValidation.Success)
                    throw new ApplicationException("Failed to connect to Bitcoin node. " + nodeValidation.ErrorMessages.ToParagraphCase());

                using (var nodeStream = BizLogicFactory.NewNodeBlockStream(NodeEndpoint.For(nodeIP, nodePort)))
                {
                    var blockStreamParser = BizLogicFactory.NewNodeStreamParser(nodeStream, BizLogicFactory.NewBlockLocator(), BizLogicFactory.NewPreProcessor(false, true), BizLogicFactory.NewPostProcessor(), BizLogicFactory.NewBlockStreamPersistor());
                    Action<int> progressHandler = i => Tools.Lambda.NoOp();
                    await blockStreamParser.Parse(cancelToken, progressHandler, false, scope.Settings.NetworkPeerPollRate);
                }
            }
        }

        protected virtual async Task<Result> TestDatabase(DBReference database)
        {
            var result = Result.Default;
            try
            {
                await Task.Run(() =>
                {
                    var dac = DACFactory.CreateDAC(database.DBMSType, database.ConnectionString, _logger);
                    dac.CreateOpenConnection();
                });
            }
            catch (Exception error)
            {
                result.AddException(error);
            }
            return result;
        }

        protected virtual async Task<Result> ValidateNode(string ipAddress, int? port)
        {
            var result = Result<NodeEndpoint>.Default;
            NodeEndpoint endPoint;
            if (!NodeEndpoint.TryParse(ipAddress, port, out endPoint))
            {
                result.AddError("Node IP address '{0}' is not a validly formatted IP adress", ipAddress ?? "(NULL");
            }
            else
            {
                return await NodeCommunicator.TestConnection(endPoint);
            }
            return result;
        }
    }
}

