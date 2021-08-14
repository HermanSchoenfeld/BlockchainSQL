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
	public partial class WebSettingsScreen : InstallWizardScreenBase {
		public WebSettingsScreen() {
			InitializeComponent();
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

		public override async Task<Result> Validate() {
			return Model.WebSettings.Validate();
		}

	}
}
