// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

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
using Sphere10.Framework.Windows.Forms;

namespace BlockchainSQL.Server.Controls {
	public partial class ServiceNodeSettingsControl : UserControlEx {
		private ServiceNodeSettings _model;

		public ServiceNodeSettingsControl() {
			InitializeComponent();
		}

		public ServiceNodeSettings Model {
			get => _model;
			set {
				_model = value;
				if (value != null)
					using (EnterUpdateScope(FinishedUpdateBehaviour.DoNothing))
						CopyModelToUI();
				else
					ClearUI();
			}
		}

		protected override void CopyModelToUI() {
			if (Model == null)
				return;
			_ipTextBox.Text = Model.IP;
			_portIntBox.Value = Model.Port;
			_pollRateIntBox.Value = Model.PollRateSEC;
		}

		protected override void CopyUIToModel() {
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

		protected override void OnStateChanged() {
			base.OnStateChanged();
		}
	}
}
