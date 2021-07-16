using Sphere10.Framework.Application;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
	public class BizLogicSettings : SettingsObject {

        public DBMSType? DBMSType;

        public string ConnectionString;
    }
}
