using Sphere10.Framework.Windows.Forms;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Server
{
    public partial class MainForm : LiteMainForm {
        public MainForm() {
            InitializeComponent();
            
            GlobalSettings.Provider =
	            new DirectorySettingsProvider(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
		            "BlockchainSQL"));
        }

        private void _generateDatabaseButton_Click(object sender, EventArgs e) {
            OpenForm<GenerateDatabaseForm>();
        }

        private void _blockFileScannerButton_Click(object sender, EventArgs e) {
            OpenForm<BlockFileScannerForm>();
        }
 
        private void _installServiceButton_Click(object sender, EventArgs e) {
            OpenForm<ServiceInstallDialog>();
        }

        private async void _uninstallServiceButton_Click(object sender, EventArgs e) {
            try {
                var dirPicker = new FolderBrowserDialog();
                dirPicker.Description = "Select folder where BlockchainSQL Service is installed";
                dirPicker.ShowNewFolderButton = false;
                if (dirPicker.ShowDialog(this) == DialogResult.OK) {
                    var dir = dirPicker.SelectedPath;
                    using (LoadingCircle.EnterAnimationScope(this)) {
                       await Tools.BlockchainSQL.LaunchUninstallServiceProcess(dir);
                    }
                    MessageBox.Show(this, "Success", "Service was stopped and uninstalled", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
