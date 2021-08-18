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
			AppDomain.CurrentDomain.UnhandledException += (s, e) =>
				Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", (Exception)e.ExceptionObject));
			Application.ThreadException += (s, e) =>
				Tools.Exceptions.ExecuteIgnoringException(() => ExceptionDialog.Show("Error", e.Exception));

			SystemLog.RegisterLogger(new ConsoleLogger());
			Sphere10Framework.Instance.StartWinFormsApplication<MainForm>();
		}

	}
}
