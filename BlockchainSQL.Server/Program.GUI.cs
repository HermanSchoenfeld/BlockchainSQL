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
			AppDomain.CurrentDomain.UnhandledException += (s, e) =>
				Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", (Exception)e.ExceptionObject));
			Application.ThreadException += (s, e) =>
				Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", e.Exception));

			SystemLog.RegisterLogger(new ConsoleLogger());
			HydrogenFramework.Instance.StartWinFormsApplication<MainForm>();
		}

	}
}
