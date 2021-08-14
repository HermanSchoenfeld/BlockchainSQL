using BlockchainSQL.Processing;
using Sphere10.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainSQL.Server {
	public partial class ScannerSettingsScreen : InstallWizardScreenBase {
		public ScannerSettingsScreen() {
			InitializeComponent();
		}

		protected override void CopyModelToUI() {
			_scannerSettingsControl.Model = new ScannerSettings {
				StoreScriptData = Model.ScannerSettings.StoreScriptData,
				MaxMemoryBufferSizeMB = Model.ScannerSettings.MaxMemoryBufferSizeMB
			};
		}

		protected override void CopyUIToModel() {
			Model.ScannerSettings.StoreScriptData = _scannerSettingsControl.Model.StoreScriptData;
			Model.ScannerSettings.MaxMemoryBufferSizeMB = _scannerSettingsControl.Model.MaxMemoryBufferSizeMB; 
		}

		public override async Task<Result> Validate() {
			return Model.ScannerSettings.Validate();
		}

	}
}
