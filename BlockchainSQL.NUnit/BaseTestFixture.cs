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
using Sphere10.Framework;
using Sphere10.Framework.Data;

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

        [TestFixtureSetUp]
        protected virtual void Setup() {
            _connectionStrings.Add(DBMSType.SQLServer, CreateDatabase(DBMSType.SQLServer));            
        }

        [TestFixtureTearDown]
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
            var dbCreator = DataAccessFactory.NewDatabaseGenerator(dbmsType);
            Task.Run(() => dbCreator.DropDatabase(connectionString)).Wait();
        }

        private static string CreateMSSQLDatabase() {
            var x = typeof(BlockchainSQL.DataAccess.NHibernate.IDataGenerator);
            var dbName = Guid.NewGuid().ToStrictAlphaString().ToUpperInvariant();
            var connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = LocalServer;
            connStringBuilder.UserID = Username;
            connStringBuilder.Password = Password;
            connStringBuilder.InitialCatalog = dbName;
            var dbCreator = DataAccessFactory.NewDatabaseGenerator(DBMSType.SQLServer);
            Task.Run(() => dbCreator.CreateEmptyDatabase(connStringBuilder.ToString())).Wait();
            Task.Run(() => dbCreator.CreateNewDatabase(connStringBuilder.ToString(), DatabaseGenerationDataPolicy.PrimingData, dbName)).Wait();
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
