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
using BlockchainSQL.DataAccess;

namespace BlockchainSQL.Server {
	public partial class WebSettingsScreen : InstallWizardScreenBase {
		public WebSettingsScreen() {
			InitializeComponent();
		}

		public override async Task<Result> Validate() {
			var result = Model.WebSettings.Validate();
			if (result.Failure)
				return result;
			result = await ValidateDatabase();
			return result;
		}

		public async Task<Result> ValidateDatabase() {
			var result = await _webSettingsControl.DatabasePanel.TestConnection();
			return result;
		}

		protected override void CopyModelToUI() {
			_webSettingsControl.Model = new WebSettings {
				Enabled = Model.WebSettings.Enabled,
				Port = Model.WebSettings.Port,
				DBMSType = Model.WebSettings.DBMSType,
				DatabaseConnectionString = Model.WebSettings.DatabaseConnectionString,
			};
		}

		protected override void CopyUIToModel() {
			Model.WebSettings.Enabled = _webSettingsControl.Model.Enabled;
			Model.WebSettings.Port = _webSettingsControl.Model.Port;
			Model.WebSettings.DBMSType = _webSettingsControl.Model.DBMSType;
			Model.WebSettings.DatabaseConnectionString = _webSettingsControl.Model.DatabaseConnectionString;
		}

	}
}
