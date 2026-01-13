// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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
	public partial class ServiceScannerSettingsControl : UserControlEx {
		private ServiceScannerSettings _model;

		public ServiceScannerSettingsControl() {
			InitializeComponent();
		}

		public ServiceScannerSettings Model {
			get => _model;
			set {
				_model = value;
				if (_model != null)
					using (EnterUpdateScope(FinishedUpdateBehaviour.DoNothing))
						CopyModelToUI();
				else
					ClearUI();
			}
		}


		protected override void CopyModelToUI() {
			if (Model == null)
				return;
			_optionsListBox.SetItemChecked(0, Model.StoreScriptData);
			_maxMemoryIntBox.Value = _model.MaxMemoryBufferSizeMB;
		}

		protected override void CopyUIToModel() {
			Guard.Ensure(Model != null, "Model not set");
			Model.StoreScriptData = _optionsListBox.GetItemChecked(0);
			_model.MaxMemoryBufferSizeMB = _maxMemoryIntBox.Value.GetValueOrDefault(500);
		}
		private void ClearUI() {
			_optionsListBox.SetItemChecked(0, false);
			_maxMemoryIntBox.Value = null;
		}

		protected override void OnStateChanged() {
			base.OnStateChanged();
		}
	}
}
