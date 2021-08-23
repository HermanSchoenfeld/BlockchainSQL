using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Processing;
using NHibernate.Proxy;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Server {
	public class InstallWizard : WizardBase<InstallWizardModel> {

		public InstallWizard() 
			: base("Intall BlockchainSQL Server Service", InstallWizardModel.Default, "Install") {
		}

		public async Task Validate() {
			// Rely on screen validations
		}


		protected override async Task<Result> Finish() {
			try {
				await ServiceManager.LaunchInstallServiceProcess(Model.ServiceDirectory,
					Model.StartAfterInstall,
					Model.BlockchainDatabaseSettings,
					Model.NodeSettings,
					Model.ScannerSettings,
					Model.WebSettings);
			} catch (Exception error) {
				return Result.Error("Failed to install Service Process.", error.ToDisplayString());
			}
			return Result.Valid;
		}

		protected override IEnumerable<WizardScreen<InstallWizardModel>> ConstructScreens() {
			yield return new InstallationDirectoryScreen();
			yield return new BSQLDatabaseSettingsScreen();
			yield return new NodeSettingsScreen();
			yield return new ScannerSettingsScreen();
			yield return new WebSettingsScreen();
		}

	}
}
