// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows.Forms;
using BlockchainSQL.Processing;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Server {
	public static partial class Program {

		public static void RunAsGUI() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.ThreadException += (s, e) =>
				Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", e.Exception));
			SystemLog.RegisterLogger(new ConsoleLogger());
			Sphere10Framework.Instance.StartWinFormsApplication<BSQLMainForm>(/*options:Sphere10FrameworkOptions.EnableDrm | Sphere10FrameworkOptions.BackgroundLicenseVerify*/);
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			try {
				if (e.ExceptionObject is Exception exception) {
					ExceptionDialog.Show("Unexpected error", exception);
				} else MessageBox.Show("No information available", "Unexpected Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} catch {
			}
		}
	}
}
