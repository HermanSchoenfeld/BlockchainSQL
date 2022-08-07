using Hydrogen;
using Hydrogen.Application;
using Hydrogen.Data;
using System.ComponentModel;

namespace BlockchainSQL.Processing {
	public class ServiceScannerSettings : SettingsObject {

		public bool StoreScriptData { get; set; } = true;

		public int MaxMemoryBufferSizeMB { get; set; } = 500;

	}
}