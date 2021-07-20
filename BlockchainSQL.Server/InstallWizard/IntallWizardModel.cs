using BlockchainSQL.Processing;
using System;
using System.IO;

namespace BlockchainSQL.Server {
	public class InstallWizardModel {

		public string ServiceDirectory { get; set; }

		public BlockchainDatabaseSettings BlockchainDatabaseSettings { get; set; }

		public NodeSettings NodeSettings { get; set; }

		public ScannerSettings ScannerSettings { get; set; }

		public WebSettings WebSettings { get; set; }

		public static InstallWizardModel Default => new InstallWizardModel {
			ServiceDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "BlockchainSQL Server"),
			BlockchainDatabaseSettings = new BlockchainDatabaseSettings(),
			NodeSettings = new NodeSettings(),
			WebSettings = new WebSettings()
		};
	}

}
