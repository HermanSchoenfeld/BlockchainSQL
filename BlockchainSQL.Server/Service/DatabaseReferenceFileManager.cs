//using System.IO;
//using System.Threading.Tasks;
//using Sphere10.Framework.Data;
//using Sphere10.Framework;

//namespace BlockchainSQL.Server.Service
//{
//    public static class DatabaseReferenceFileManager {
//        private const string ConnectFileName = "Database.connection";
//        private const string Salt = "##KEdp0324kasdfl,fkn#^#$%^sfdsq23q45555555555";
//        public static async Task CreateDatabaseConnectionFile(string serviceExePath, DBReference dbReference) {
//            var path = Path.GetDirectoryName(serviceExePath);
//            var connectionFilePath = Path.Combine(path, ConnectFileName);
//            var password = serviceExePath.ToLowerInvariant();

//            if (File.Exists(connectionFilePath))
//                File.Delete(connectionFilePath);

//            await Task.Run(() =>
//                File.WriteAllText(
//                    connectionFilePath,
//                    Tools.Crypto.EncryptStringAES(
//                        Tools.Xml.WriteToString(dbReference),
//                        password,
//                        Salt
//                    )
//                )
//            );
//        }


//        public static async Task<DBReference> LoadDatabaseConnectionFile(string serviceExePath) {
//	        serviceExePath = serviceExePath.EndsWith(".dll")
//		        ? serviceExePath.TrimEnd(".dll") + ".exe"
//		        : serviceExePath;
	        
//	        var path = Path.GetDirectoryName(serviceExePath);
//            var connectionFilePath = Path.Combine(path, ConnectFileName);
//            var password = serviceExePath.ToLowerInvariant();

//            if (!File.Exists(connectionFilePath))
//                throw new FileNotFoundException("Database connection file not found", connectionFilePath);

//            return await Task.Run(() =>
//                Tools.Xml.ReadFromString<DBReference>(
//                    Tools.Crypto.DecryptStringAES(
//                        File.ReadAllText(connectionFilePath),
//                        password,
//                        Salt
//                    )
//                )
//            );
//        }
//    }
//}
