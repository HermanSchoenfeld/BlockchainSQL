using Sphere10.Framework;
using Sphere10.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.DataAccess {
    public interface IDatabaseGenerator {
        bool DatabaseExists(string connectionString);
        void DropDatabase(string connectionString);
        void CreateEmptyDatabase(string connectionString);
        void CreateNewDatabase(string connectionString, DatabaseGenerationDataPolicy dataPolicy, string databaseName);      
    }
}
