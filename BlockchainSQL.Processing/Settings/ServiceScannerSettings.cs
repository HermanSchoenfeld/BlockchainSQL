using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using System.ComponentModel;

namespace BlockchainSQL.Processing {
	public class ServiceScannerSettings : SettingsObject {

		public bool StoreScriptData { get; set; } = true;

		public int MaxMemoryBufferSizeMB { get; set; } = 500;

	}
}