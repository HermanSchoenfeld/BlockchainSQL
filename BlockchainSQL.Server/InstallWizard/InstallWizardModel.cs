using BlockchainSQL.Processing;
using System;
using System.IO;

namespace BlockchainSQL.Server {
	public class InstallWizardModel {

		public bool StartAfterInstall { get; set; }

		public string ServiceDirectory { get; set; }

		public ServiceDatabaseSettings ServiceDatabaseSettings { get; set; }

		public ServiceNodeSettings NodeSettings { get; set; }

		public ServiceScannerSettings ScannerSettings { get; set; }

		public WebSettings WebSettings { get; set; }

		public static InstallWizardModel Default => new InstallWizardModel {
			ServiceDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "BlockchainSQL"),
			StartAfterInstall = true,
			ScannerSettings = new ServiceScannerSettings(),
			ServiceDatabaseSettings = new ServiceDatabaseSettings(),
			NodeSettings = new ServiceNodeSettings(),
			WebSettings = new WebSettings()
		};
	}

}
