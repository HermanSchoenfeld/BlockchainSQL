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

namespace BlockchainSQL.Server.Controls {
	public partial class NodeSettingsControl : UserControl {
		private NodeSettings _model;

		public NodeSettingsControl() {
			InitializeComponent();
		}

		public NodeSettings Model {
			get => _model;
			set {
				_model = value;
				if (value != null)
					CopyModelToUI();
				else
					ClearUI();
			}
		}

		private void CopyModelToUI() {
			Guard.Ensure(_model != null, "Model not set");
			_ipTextBox.Text = Model.IP;
			_portIntBox.Value = Model.Port;
			_pollRateIntBox.Value = Model.PollRateSEC;
		}

		private void CopyUIToModel() {
			Guard.Ensure(_model != null, "Model not set");
			Model.IP = _ipTextBox.Text;
			Model.Port = _portIntBox.Value.GetValueOrDefault(8333);
			Model.PollRateSEC = _pollRateIntBox.Value.GetValueOrDefault(10);
		}

		private void ClearUI() {
			_ipTextBox.Text = string.Empty;
			_portIntBox.Value = null;
			_pollRateIntBox.Value = null;
		}
	}
}
