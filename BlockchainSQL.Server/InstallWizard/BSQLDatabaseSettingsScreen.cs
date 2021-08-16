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
using Sphere10.Framework.Windows.Forms;
using BlockchainSQL.Processing;

namespace BlockchainSQL.Server {
	public partial class BSQLDatabaseSettingsScreen : InstallWizardScreenBase {
		public BSQLDatabaseSettingsScreen() {
			InitializeComponent();
		}

		protected override void CopyModelToUI() {
			_blockchainDatabaseSettingsControl.Model = new BlockchainDatabaseSettings {
				DBMSType = Model.BlockchainDatabaseSettings.DBMSType,
				ConnectionString = Model.BlockchainDatabaseSettings.ConnectionString,
			};
		}

		protected override void CopyUIToModel() {
			Model.BlockchainDatabaseSettings.DBMSType = _blockchainDatabaseSettingsControl.Model.DBMSType;
			Model.BlockchainDatabaseSettings.ConnectionString = _blockchainDatabaseSettingsControl.Model.ConnectionString; 
		}

		public override Task<Result> Validate() {
			return ValidateDatabase();
		}

		public async Task<Result> ValidateDatabase() {
			var result = await _blockchainDatabaseSettingsControl.DatabasePanel.TestConnection();
			if (result.Success) {
				var dac = BlockchainDatabase.NewDAC(_blockchainDatabaseSettingsControl.DatabasePanel.GetDAC());
				if (!dac.IsValidSchema())
					result.AddError("BlockchainSQL Database schema is not valid. Try regenerating.");
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
