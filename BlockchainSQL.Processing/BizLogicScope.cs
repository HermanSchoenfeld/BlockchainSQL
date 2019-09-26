using System;
using System.Collections.Generic;
using System.Linq;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public sealed class BizLogicScope : ScopeContext<BizLogicScope> {
        private readonly bool _databaseFreeContext;
        private readonly DBMSType _dbmsType;
        private readonly string _connectionString;
        private readonly ApplicationDAC _dac;
        private readonly SettingsCache _settings;
        public BizLogicScope(ApplicationDAC dac)
            : this(dac.DBMSType, dac.ConnectionString, dac.Log) {
        }

        public BizLogicScope(ILogger logger = null)
            : this(GlobalSettings.Get<BizLogicSettings>().DBMSType, GlobalSettings.Get<BizLogicSettings>().ConnectionString, logger, false) {
        }

        public BizLogicScope(DBMSType dbmsType, string connectionString, ILogger logger = null)
            : this(dbmsType, connectionString, logger, false) {
        }

        private BizLogicScope(DBMSType? dbmsType, string connectionString, ILogger logger, bool databaseFree)
            : base(typeof (BizLogicScope).FullName, ScopeContextPolicy.MustBeRoot) {
            _databaseFreeContext = databaseFree;
            if (dbmsType.HasValue)
                _dbmsType = dbmsType.Value;
            _connectionString = connectionString;
            Log = logger ?? new NoOpLogger();
            _databaseFreeContext = databaseFree;
            if (!_databaseFreeContext)
                _dac = CreateDAC();
            _settings = new SettingsCache();
        }

        public static BizLogicScope Current {
            get {
                var current = GetCurrent(typeof (BizLogicScope).FullName);
                if (current == null)
                    throw new SoftwareException("Business logic components must be used within a BizLogicScope");
                return current;
            }
        }

        public bool UsesDatabase {
            get { return !_databaseFreeContext; }
        }

        public DBMSType DBMSType {
            get {
                CheckSet();
                return _dbmsType;
            }
        }

        public string ConnectionString {
            get {
                CheckSet();
                return _connectionString;
            }
        }

        public ILogger Log { get; private set; }

        public ApplicationDAC DAC {
            get {
                CheckSet();
                return _dac;
            }
        }

        public SettingsCache Settings {
            get { return _settings; }
        }

        public ApplicationDAC CreateDAC() {
            CheckSet();
            var dac = DataAccessFactory.NewDAC(this.DBMSType, ConnectionString, Log);
            //dac.Log = new ConsoleLogger();
            return dac;
        }
        public static BizLogicScope EnterDatabaseFreeScope(ILogger logger = null) {
            return new BizLogicScope(null, null, logger, true);
        }

        protected override void OnScopeEnd(BizLogicScope rootScope, bool inException) {
        }

        private void CheckSet() {
            if (_databaseFreeContext)
                throw new SoftwareException("A database-free BizLogic context is unable to create a DAC");
        }
    }
}
