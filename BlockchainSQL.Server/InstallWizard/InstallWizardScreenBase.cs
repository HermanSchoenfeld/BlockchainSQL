using Hydrogen.Windows.Forms;

namespace BlockchainSQL.Server {
	// This base class is needed to stop WinForms designer from throwing. This class cannot be designed by it's descendents can. This is due to the generic base.
	public class InstallWizardScreenBase : WizardScreen<InstallWizardModel> { 
    }

}
