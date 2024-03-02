using Hydrogen;
using Hydrogen.Windows.Forms;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace BlockchainSQL.Server;

public class InstallWizard : WizardBase<InstallWizardModel> {

	public InstallWizard()
		: base("Install BlockchainSQL Server Service", InstallWizardModel.Default, "Install") {
	}

	public async Task Validate() {
		// Rely on screen validations
	}

	protected override async Task<Result> Finish() {
		try {
			await ServiceManager.LaunchInstallServiceProcess(Model.ServiceDirectory,
				Model.StartAfterInstall,
				Model.ServiceDatabaseSettings,
				Model.NodeSettings,
				Model.ScannerSettings,
				Model.WebSettings);
		} catch (Exception error) {
			return Result.Error("Failed to install Service Process.", error.ToDisplayString());
		}
		return Result.Success;
	}

	protected override IEnumerable<WizardScreen<InstallWizardModel>> ConstructScreens() {
		yield return new InstallationDirectoryScreen();
		yield return new ServiceDatabaseSettingsScreen();
		yield return new ServiceNodeSettingsScreen();
		yield return new ServiceScannerSettingsScreen();
		yield return new WebSettingsScreen();
	}

}