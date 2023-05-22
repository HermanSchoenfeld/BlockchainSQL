using Hydrogen.Windows.Forms;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hydrogen.Application;
using BlockchainSQL.Processing;
using Microsoft.Extensions.DependencyInjection;

namespace BlockchainSQL.Server {
	public partial class BSQLMainForm : MainForm {
		public BSQLMainForm() {
			InitializeComponent();
			base.MenuStrip.Visible = true;
			base.ToolStrip.Visible = false;
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
			OpenForm<BlockFileImporterForm>();
		}

		private async void _installServiceButton_Click(object sender, EventArgs e) {
			try {
				var wizard = new InstallWizard();
				if (await wizard.Start(this) == WizardResult.Success)
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
			OpenForm<NetworkBlockImporterForm>();
		}

		private void _databaseDiagnosticButton_Click(object sender, EventArgs e) {
			OpenForm<DiagnosticForm>();
		}

		private void _aboutMenuItem_Click(object sender, EventArgs e) {
			try {
				this.ShowAboutBox();
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private void _productManualMenuItem_Click(object sender, EventArgs e) {
			try {
				var launcher = HydrogenFramework.Instance.ServiceProvider.GetService<IWebsiteLauncher>();
				launcher.LaunchWebsite("https://sphere10.com/products/blockchainsql/manual");
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private void _hardwareManualMenuItem_Click(object sender, EventArgs e) {
			try {
				var launcher = HydrogenFramework.Instance.ServiceProvider.GetService<IWebsiteLauncher>();
				launcher.LaunchWebsite("https://sphere10.com/products/blockchainsql/hardware-manual");
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
		}

		private void _aboutMenuItem_Click_1(object sender, EventArgs e) {
			try {
				this.ShowAboutBox();
			} catch (Exception error) {
				ExceptionDialog.Show(this, error);
			}
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
