using System;
using System.ComponentModel;
using System.Configuration.Install;
using Sphere10.Framework;

namespace BlockchainSQL.Server
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer {
        public ProjectInstaller() {
            InitializeComponent();
        }

        private void serviceInstaller1_Committed(object sender, InstallEventArgs e) {
            Tools.BlockchainSQL.SetServiceRecovery(TimeSpan.FromMinutes(1)).WaitSafe();        
        }
    }
}
