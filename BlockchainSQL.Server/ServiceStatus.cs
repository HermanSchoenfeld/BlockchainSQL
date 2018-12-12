using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Server {
    public enum ServiceStatus {
        NotInstalled,
        Starting,
        Started,
        Stopping,
        Stopped,
        Error
    }
}
