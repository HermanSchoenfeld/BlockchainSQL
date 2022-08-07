using Hydrogen.Windows.Forms;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hydrogen.Application;
using BlockchainSQL.Processing;

namespace BlockchainSQL.Server {
	public partial class MainForm : LiteMainForm {
		public MainForm() {
			InitializeComponent();
		}

		protected override void OnActivated(EventArgs e) {
			base.OnActivated(e);
			_installServiceButton.Enabled = !ServiceManager.IsInstalled();
			_uninstallServiceButton.Enabled = !_installServiceButton.Enabled;
			//_databaseDiagnosticButton.Enabled = _uninstallServiceButton.Enabled;
		}

		private void _generateDatabaseButton_Click(object sender, EventArgs e) {
			OpenForm<GenerateDatabaseForm>();
		}

		private void _blockFileScannerButton_Click(object sender, EventArgs e) {
			OpenForm<BlockFileScannerForm>();
		}

		private async void _installServiceButton_Click(object sender, EventArgs e) {
			try {
				var wizard = new InstallWizard();
				if(await wizard.Start(this) == WizardResult.Success) 
					DialogEx.Show(this, "BlockchainSQL Server Windows Service was installed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private async void _uninstallServiceButton_Click(object sender, EventArgs e) {
			try {
				var dirPicker = new FolderBrowserDialog();
				dirPicker.Description = "Select folder where BlockchainSQL Service is installed";
				dirPicker.ShowNewFolderButton = false;
				if (dirPicker.ShowDialog(this) == DialogResult.OK) {
					var dir = dirPicker.SelectedPath;
					using (LoadingCircle.EnterAnimationScope(this)) {
						await ServiceManager.LaunchUninstallServiceProcess(dir);
					}
					DialogEx.Show(this, "BlockchainSQL Server Windows Service was uninstalled", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private void _networkButton_Click(object sender, EventArgs e) {
			OpenForm<NetworkScannerForm>();
		}

		private void _databaseDiagnosticButton_Click(object sender, EventArgs e) {
			OpenForm<DiagnosticForm>();
		}


		private void OpenForm<TForm>() where TForm : Form, new() {
			try {
				var form = new TForm();
				form.ShowDialog(this);
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

	}
}
