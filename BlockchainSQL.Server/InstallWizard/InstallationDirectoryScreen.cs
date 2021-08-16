using Sphere10.Framework;
using Sphere10.Framework.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlDeepDeserializer = Sphere10.Framework.XmlDeepDeserializer;

namespace BlockchainSQL.Server {
	public partial class InstallationDirectoryScreen : InstallWizardScreenBase {
		private const string DefaultBlockchainSQLServiceFolder = "BlockchainSQL";

		public InstallationDirectoryScreen() {
			InitializeComponent();
			this.StateChanged += TryShowInstallDir;
			_installDirLabel.Text = string.Format(_installDirLabel.Text, DefaultBlockchainSQLServiceFolder);
		}

		public override Task<Result> Validate() {
			return ValidateDestinationPath(Model.ServiceDirectory);
		}

		protected override void CopyUIToModel() {
			var dir = _pathSelector.Path;
			if (_createServiceFolderCheckBox.Checked)
				dir = Path.Combine(dir, DefaultBlockchainSQLServiceFolder);
			Model.ServiceDirectory = dir;
			Model.StartAfterInstall = _startServiceCheckBox.Checked;
		}

		protected override void CopyModelToUI() {
			if (string.IsNullOrWhiteSpace(Model.ServiceDirectory)) {
				_pathSelector.Path = string.Empty;
				_createServiceFolderCheckBox.Checked = true;
				_startServiceCheckBox.Checked = false;
				return;
			}

			var dir = Model.ServiceDirectory;
			if (dir.EndsWith(DefaultBlockchainSQLServiceFolder)) {
				dir = Tools.FileSystem.GetParentDirectoryPath(dir);
				_createServiceFolderCheckBox.Checked = true;
			} else
				_createServiceFolderCheckBox.Checked = false;
			_pathSelector.Path = dir;
			_startServiceCheckBox.Checked = Model.StartAfterInstall;
			TryShowInstallDir();
		}

		private async Task<Result> ValidateDestinationPath(string path) {
			var result = Result.Default;
			if (!Tools.FileSystem.IsWellFormedDirectoryPath(path)) {
				result.AddError("Malfomed destination path '{0}'", path);
			} else if (Directory.Exists(path)) {
				var files = Directory.GetFiles(path);
				if (files.Length > 0) {
					var exeFile = Path.GetFileName(Assembly.GetExecutingAssembly().Location).ToUpperInvariant();
					if (files.Select(Path.GetFileName).Select(s => s.ToUpperInvariant()).Contains(exeFile)) {
						result.AddError("Service already installed in '{0}'", path);
					} else {
						if (QuestionDialog.Show(this, SystemIconType.Question, "Path already exists", "Destination directory '{0}' contains files. If you continue this will delete them?".FormatWith(path), "Continue", "Cancel") == DialogExResult.Button2) {
							result.AddError("Path '{0}' contains files", path);
						}
					}
				}
			}
			return result;
		}


		private void TryShowInstallDir() {
			if (!string.IsNullOrWhiteSpace(Model.ServiceDirectory) && Path.IsPathFullyQualified(Model.ServiceDirectory)) {
				_installDirLabel.Visible = true;
				_installDirLabel.Text = $"Installation Directory: {Model.ServiceDirectory}";
			} else
				_installDirLabel.Visible = false;

		}
	}
}
