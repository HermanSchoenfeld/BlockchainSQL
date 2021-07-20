using Sphere10.Framework;
using Sphere10.Framework.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Server {
	public class InstallWizard : WizardBase<InstallWizardModel> {

		public InstallWizard() 
			: base("Intall BlockchainSQL Server Service", InstallWizardModel.Default, "Install") {
		}

		protected override IEnumerable<WizardScreen<InstallWizardModel>> ConstructScreens() {
			yield return new ServiceFolderScreen();
			yield return new BlockchainDatabaseScreen();
			yield return new NodeScreen();
			yield return new ScannerScreen();
		}

		protected override Task<Result> Finish() {
			throw new NotImplementedException();
		}
	}
}
