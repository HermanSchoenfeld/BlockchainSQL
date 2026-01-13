// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using NUnit.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework;

namespace BlockchainSQL.NUnit
{

    [TestFixture]
    public class BaseTestFixture {
        private const string LocalServer = "localhost";
        private const string Username = "sa";
        private const string Password = "";
        private readonly IDictionary<DBMSType, string> _connectionStrings;

        public BaseTestFixture() {
            _connectionStrings = new Dictionary<DBMSType, string>();
        }

        [OneTimeSetUp]
        protected virtual void Setup() {
            _connectionStrings.Add(DBMSType.SQLServer, CreateDatabase(DBMSType.SQLServer));            
        }

        [OneTimeTearDown]
        protected virtual void Teardown() {
            foreach (var item in _connectionStrings) {
                Tools.Exceptions.ExecuteIgnoringException(() => DropDatabase(item.Key, item.Value));
            }
        }

        protected virtual UnitTestScope EnterTestScope(DBMSType dbmsType) {
            if (!_connectionStrings.ContainsKey(dbmsType))
                throw new NotSupportedException(dbmsType.ToString());

            return new UnitTestScope(dbmsType, _connectionStrings[dbmsType]);
        }

        protected static string CreateDatabase(DBMSType dbms) {
            switch (dbms) {
                case DBMSType.SQLServer:
                    return CreateMSSQLDatabase();
                case DBMSType.Sqlite:
                    return CreateSqliteDatabase();
                default:
                    throw new NotSupportedException(dbms.ToString());
            }
        }

        protected static void DropDatabase(DBMSType dbmsType, string connectionString) {
            var dbCreator = BlockchainDatabase.NewDatabaseManager(dbmsType);
            Task.Run(() => dbCreator.DropDatabase(connectionString)).Wait();
        }

        private static string CreateMSSQLDatabase() {
            var x = typeof(IDataGenerator);
            var dbName = Guid.NewGuid().ToStrictAlphaString().ToUpperInvariant();
            var connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = LocalServer;
            connStringBuilder.UserID = Username;
            connStringBuilder.Password = Password;
            connStringBuilder.InitialCatalog = dbName;
            var dbCreator = BlockchainDatabase.NewDatabaseManager(DBMSType.SQLServer);
            Task.Run(() => dbCreator.CreateEmptyDatabase(connStringBuilder.ToString())).Wait();
            Task.Run(() => dbCreator.CreateApplicationDatabase(connStringBuilder.ToString(), DatabaseGenerationDataPolicy.PrimingData, dbName)).Wait();
            return connStringBuilder.ToString();
        }


        private static string CreateSqliteDatabase() {
            throw new NotImplementedException();
        }

        protected virtual void AssertBlockchain(ApplicationDAC dac, params string[] blockHashes) {
            AssertBlockchainSegment(dac, (long)KnownBranches.MainChain, 0, blockHashes.Length - 1, blockHashes);
        }

        protected virtual void AssertBlockchainSegment(ApplicationDAC dac, long branchID, long fromBlockHeight, long toBlockHeight,  params string[] blockHashes) {
            var blocks = dac
                .Select(
                    "Block",
                    new[] {"Hash"},
                    whereClause: "BranchID = {0} AND Height >= {1} AND Height <= {2}".FormatWith(branchID, fromBlockHeight, toBlockHeight),
                    orderByClause: "Height ASC"
                ).Rows.Cast<DataRow>().ToArray();
            Assert.AreEqual(blocks.Length, blockHashes.Length);
            for (var i = 0; i < blocks.Length; i++)
                Assert.AreEqual( TestHelper.ToHashBytes(blockHashes[i]), blocks[i].Get<byte[]>("Hash"));
        }
    }
}
