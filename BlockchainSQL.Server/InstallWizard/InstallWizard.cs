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
			// Save settings to disk (these are loaded by service and web processes)
			var bsqlDBSettings = GlobalSettings.Get<BlockchainDatabaseSettings>();
			bsqlDBSettings.DBMSType = Model.BlockchainDatabaseSettings.DBMSType;
			bsqlDBSettings.ConnectionString = Model.BlockchainDatabaseSettings.ConnectionString;
			bsqlDBSettings.Save();

			var nodeSettings = GlobalSettings.Get<NodeSettings>();
			nodeSettings.IP = Model.NodeSettings.IP;
			nodeSettings.Port = Model.NodeSettings.Port;
			nodeSettings.PollRateSEC = Model.NodeSettings.PollRateSEC;
			nodeSettings.Save();

			var scannerSettings = GlobalSettings.Get<ScannerSettings>();
			scannerSettings.StoreScriptData = Model.ScannerSettings.StoreScriptData;
			scannerSettings.MaxMemoryBufferSizeMB = Model.ScannerSettings.MaxMemoryBufferSizeMB;
			scannerSettings.Save();

			var webSettings = GlobalSettings.Get<WebSettings>();
			webSettings.Enabled = Model.WebSettings.Enabled;
			webSettings.Port = Model.WebSettings.Port;
			webSettings.DBMSType = Model.WebSettings.DBMSType;
			webSettings.DatabaseConnectionString = Model.WebSettings.DatabaseConnectionString;
			webSettings.Save();

			return ServiceManager.LaunchInstallServiceProcess(Model.ServiceDirectory, Model.StartAfterInstall);
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
