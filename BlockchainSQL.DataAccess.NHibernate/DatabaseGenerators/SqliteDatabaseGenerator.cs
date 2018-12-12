using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlockchainSQL.DataAccess.NHibernate {
    internal class SqliteDatabaseGenerator : DatabaseGeneratorBase {

        public override bool DatabaseExists(string connectionString) {
            var sqliteSB = new System.Data.SQLite.SQLiteConnectionStringBuilder(connectionString);
            return File.Exists(sqliteSB.DataSource);
        }

        public override void DropDatabase(string connectionString) {
            var sqliteSB = new System.Data.SQLite.SQLiteConnectionStringBuilder(connectionString);
            File.Delete(sqliteSB.DataSource);
        }

        public override void CreateEmptyDatabase(string connectionString) {
            var sqliteSB = new System.Data.SQLite.SQLiteConnectionStringBuilder(connectionString);
            Tools.Sqlite.Create(sqliteSB.DataSource, sqliteSB.Password, 32768, SqliteJournalMode.Default, SqliteSyncMode.Normal, AlreadyExistsPolicy.Error);
        }



        protected override void CreateDatabaseViaConfiguration(Configuration configuration, string connectionString) {
            string filename, password;
            GetFilenamePasswordFromConnectionString(connectionString, out filename, out password);
            if (File.Exists(filename))
                File.Delete(filename);

            base.CreateDatabaseViaConfiguration(configuration, connectionString);
        }

        protected override IPersistenceConfigurer GetPersistenceConfigurer(string connectionString) {
            string filename, password;
            GetFilenamePasswordFromConnectionString(connectionString, out filename, out password);
            if (!string.IsNullOrEmpty(password)) {
                return SQLiteConfiguration.Standard.UsingFileWithPassword(filename,password);
            }
            return SQLiteConfiguration.Standard.UsingFile(filename);
        }

        private void GetFilenamePasswordFromConnectionString(string connectionString, out string filename, out string password) {
            var builder = new SQLiteConnectionStringBuilder(connectionString);
            filename = builder.DataSource;
            password = builder.Password;
        }


    }
}
