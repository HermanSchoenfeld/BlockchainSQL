using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public class BizLogicSettings : SettingsObject {

        public DBMSType? DBMSType;

        public string ConnectionString;
    }
}
