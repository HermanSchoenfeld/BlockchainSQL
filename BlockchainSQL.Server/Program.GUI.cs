using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Windows.Forms;
using BlockchainSQL.Processing;
using Hydrogen;
using Hydrogen.Data;
using Hydrogen.Windows.Forms;
using Hydrogen.Application;

namespace BlockchainSQL.Server {
	public static partial class Program {

		public static void RunAsGUI() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Application.ThreadException += (s, e) =>
				Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", e.Exception));
			SystemLog.RegisterLogger(new ConsoleLogger());
			HydrogenFramework.Instance.StartWinFormsApplication<BSQLMainForm>(options:HydrogenFrameworkOptions.EnableDrm | HydrogenFrameworkOptions.BackgroundLicenseVerify);
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
