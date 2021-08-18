using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Processing;
using NHibernate.Proxy;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Server {
	public class InstallWizard : WizardBase<InstallWizardModel> {

		public InstallWizard() 
			: base("Intall BlockchainSQL Server Service", InstallWizardModel.Default, "Install") {
		}

		public async Task Validate() {
			// Rely on screen validations
		}

		public Task Install() {
			return ServiceManager.LaunchInstallServiceProcess(Model.ServiceDirectory, Model.StartAfterInstall, Model.BlockchainDatabaseSettings, Model.NodeSettings, Model.ScannerSettings, Model.WebSettings);
		}


		public class WebSettings : SettingsObject {
			public bool Enabled { get; set; } = true;

			public int Port { get; set; } = 5000;

			public DBMSType DBMSType { get; set; } = DBMSType.Sqlite;

			[Encrypted]
			public string DatabaseConnectionString { get; set; } = "";
		}

		protected override IEnumerable<WizardScreen<InstallWizardModel>> ConstructScreens() {
			yield return new InstallationDirectoryScreen();
			yield return new BSQLDatabaseSettingsScreen();
			yield return new NodeSettingsScreen();
			yield return new ScannerSettingsScreen();
			yield return new WebSettingsScreen();
		}

		protected override async Task<Result> Finish() {
			// Do Install
			return Result.Default;
		}

		//SaveFormSettings();
		//await Validate();
		//await Install();
	}
}
