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
	public partial class ServiceNodeSettingsScreen : InstallWizardScreenBase {
		public ServiceNodeSettingsScreen() {
			InitializeComponent();
		}

		protected override void CopyModelToUI() {
			_nodeSettingsControl.Model = new ServiceNodeSettings {
				IP = Model.NodeSettings.IP,
				Port = Model.NodeSettings.Port,
				PollRateSEC = Model.NodeSettings.PollRateSEC
			};
		}

		protected override void CopyUIToModel() {
			Model.NodeSettings.IP = _nodeSettingsControl.Model.IP;
			Model.NodeSettings.Port = _nodeSettingsControl.Model.Port;
			Model.NodeSettings.PollRateSEC = _nodeSettingsControl.Model.PollRateSEC;
		}

		public override async Task<Result> Validate() {
			return Model.NodeSettings.Validate();
		}

		protected override void OnStateChanged() {
			base.OnStateChanged();
		}
	}
}

