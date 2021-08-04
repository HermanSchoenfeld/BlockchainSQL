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
using BlockchainSQL.DataAccess;

namespace BlockchainSQL.Server {
	public partial class BlockchainDatabaseScreen : InstallWizardScreenBase {
		public BlockchainDatabaseScreen() {
			InitializeComponent();
		}

		public override async Task OnPresent() {
			await base.OnPresent();
			_blockchainDatabaseSettingsControl.Model = Model.BlockchainDatabaseSettings;
		}

		public override Task<Result> Validate() {
			return ValidateDatabase();
		}

		public async Task<Result> ValidateDatabase() {
			var result = await _blockchainDatabaseSettingsControl.DatabasePanel.TestConnection();
			if (result.Success) {
				var dac = BlockchainDatabase.NewDAC(_blockchainDatabaseSettingsControl.DatabasePanel.GetDAC());
				if (!dac.IsValidSchema())
					result.AddError("Database schema is not valid. Ensure ");
			}
			return result;
		}

		public override async Task OnNext() {
			await base.OnNext();
			Model.BlockchainDatabaseSettings = _blockchainDatabaseSettingsControl.Model;
		}

		private void _blockchainDatabaseSettingsControl_Load(object sender, EventArgs e) {

		}
	}
}
