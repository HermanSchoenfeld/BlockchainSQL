// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.DataAccess;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
	public sealed class BizLogicScope : SyncContextScope {
        private readonly bool _databaseFreeContext;
        private readonly DBMSType _dbmsType;
        private readonly string _connectionString;
        private readonly ApplicationDAC _dac;

        //public BizLogicScope(ApplicationDAC dac)
        //    : this(dac.DBMSType, dac.ConnectionString, dac.Log) {
        //}

        //public BizLogicScope(ILogger logger = null)
        //    : this(GlobalSettings.Get<ServiceDatabaseSettings>().DBMSType, GlobalSettings.Get<ServiceDatabaseSettings>().ConnectionString, logger, GlobalSettings.Provider, false) {
        //}

        public BizLogicScope(DBMSType dbmsType, string connectionString, ILogger logger = null)
            : this(dbmsType, connectionString, logger, GlobalSettings.Provider, false) {
        }

        private BizLogicScope(DBMSType? dbmsType, string connectionString, ILogger logger, ISettingsProvider settings, bool databaseFree)
            : base(ContextScopePolicy.MustBeRoot, typeof (BizLogicScope).FullName) {
            _databaseFreeContext = databaseFree;
            if (dbmsType.HasValue)
                _dbmsType = dbmsType.Value;
            _connectionString = connectionString;
            Log = logger ?? new NoOpLogger();
			Settings = settings ?? GlobalSettings.Provider;
            _databaseFreeContext = databaseFree;
            if (!_databaseFreeContext)
                _dac = CreateDAC();
        }

        public static BizLogicScope Current {
            get {
                var current = GetCurrent(typeof (BizLogicScope).FullName) as BizLogicScope;
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

		public ISettingsProvider Settings { get; init; }

        public ApplicationDAC DAC {
            get {
                CheckSet();
                return _dac;
            }
        }


        public ApplicationDAC CreateDAC() {
            CheckSet();
            var dac = BlockchainDatabase.NewDAC(this.DBMSType, ConnectionString, Log);
            //dac.Log = new ConsoleLogger();
            return dac;
        }
        public static BizLogicScope EnterDatabaseFreeScope(ILogger logger = null) {
            return new BizLogicScope(null, null, logger, GlobalSettings.Provider, true);
        }

        protected override void OnContextEnd() {
        }

        private void CheckSet() {
            if (_databaseFreeContext)
                throw new SoftwareException("A database-free BizLogic context is unable to create a DAC");
        }
    }
}
