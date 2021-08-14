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
			//var pathValidation = await ValidateDestinationPath();
			//if (pathValidation.Failure) {
			//	throw new SoftwareException(pathValidation);
			//}

			//var dbValidation = await ValidateDatabase();
			//if (dbValidation.Failure)
			//	throw new SoftwareException(dbValidation);
		}

		public async Task Install() {
			//return Tools.BlockchainSQL.LaunchInstallServiceProcess(
			//	_pathSelector.Path,
			//	_blockchainDatabaseConnectionPanel.Database
			//);

		}


		protected override IEnumerable<WizardScreen<InstallWizardModel>> ConstructScreens() {
			yield return new NodeSettingsScreen();
			yield return new ScannerSettingsScreen();
			yield return new WebSettingsScreen();

			yield return new InstallationDirectoryScreen();
			yield return new BSQLDatabaseSettingsScreen();
		}

		protected override async Task<Result> Finish() {
			// Do Install
			return Result.Default;
		}

		//SaveFormSettings();
		//await Validate();
		//await Install();
	}
}
