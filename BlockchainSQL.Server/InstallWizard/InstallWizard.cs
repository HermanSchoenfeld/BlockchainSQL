using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Server {
	public class InstallWizard : WizardBase<InstallWizardModel> {

		public InstallWizard() 
			: base("Intall BlockchainSQL Server Service", InstallWizardModel.Default, "Install") {
		}






		public async Task Validate() {
			var pathValidation = await ValidateDestinationPath();
			if (pathValidation.Failure) {
				throw new SoftwareException(pathValidation);
			}

			var dbValidation = await ValidateDatabase();
			if (dbValidation.Failure)
				throw new SoftwareException(dbValidation);
		}

		public Task Install() {
			return Tools.BlockchainSQL.LaunchInstallServiceProcess(
				_pathSelector.Path,
				_blockchainDatabaseConnectionPanel.Database
			);
		}


		protected override IEnumerable<WizardScreen<InstallWizardModel>> ConstructScreens() {
			yield return new ServiceFolderScreen();
			yield return new BlockchainDatabaseScreen();
			yield return new NodeScreen();
			yield return new ScannerScreen();
		}

		protected override async Task<Result> Finish() {
			// Do Install
		}

		//SaveFormSettings();
		//await Validate();
		//await Install();
	}
}
