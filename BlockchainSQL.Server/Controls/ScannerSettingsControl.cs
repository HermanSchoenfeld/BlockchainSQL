using BlockchainSQL.Processing;
using Sphere10.Framework;
using Sphere10.Framework.Windows.Forms;
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
	public partial class ScannerSettingsControl : UserControlEx {
		private ScannerSettings _model;

		public ScannerSettingsControl() {
			InitializeComponent();
		}

		public ScannerSettings Model {
			get => _model;
			set {
				_model = value;
				if (_model != null)
					CopyModelToUI();
				else
					ClearUI();
			}
		}


		private void CopyModelToUI() {
			_optionsListBox.SetItemChecked(0, Model.StoreScriptData);
			_maxMemoryIntBox.Value = _model.MaxMemoryBufferSizeMB;
		}

		private void CopyUIToModel() {
			Model.StoreScriptData = _optionsListBox.GetItemChecked(0);
			_model.MaxMemoryBufferSizeMB = _maxMemoryIntBox.Value.GetValueOrDefault(500);
		}
		private void ClearUI() {
			_optionsListBox.SetItemChecked(0, false);
			_maxMemoryIntBox.Value = null;
		}
	}
}
