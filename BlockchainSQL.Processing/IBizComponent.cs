using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataAccess;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public interface IBizComponent {
        ILogger Log { get; }

        ApplicationDAC CustomDAC { get; set; }
        ApplicationDAC CreateDAC();
    }
}
