using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Processing;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Server {

	public class ServiceManager {
		
		public const string ServiceName = "BlockchainSQL Server";

		/// <summary>
		/// Launches the service installation.
		/// </summary>
		/// <param name="destPath">Path service will be installed to</param>
		/// <param name="startAfterInstall">Whether to launch service after installation</param>
		/// <returns>Task</returns>
		/// <remarks>Settings are shared between web/service/gui and should be set before installation of service.</remarks>
		public static async Task LaunchInstallServiceProcess(string destPath, bool startAfterInstall, BlockchainDatabaseSettings dbSettings, NodeSettings nodeSettings, ScannerSettings scannerSettings, WebSettings webSettings) {
			var args = new StringBuilder();
			args.Append($"install --path \"{destPath}\" --dbms {dbSettings.DBMSType} --db {Tools.Runtime.EncodeCommandLineArgumentWin(dbSettings.ConnectionString)} --ip {nodeSettings.IP} --port {nodeSettings.Port} --poll {nodeSettings.PollRateSEC} --maxmem {scannerSettings.MaxMemoryBufferSizeMB}");
			if (scannerSettings.StoreScriptData)
				args.Append(" --store_scripts");
			if (startAfterInstall)
				args.Append(" --start");
			if (webSettings.Enabled) {
				args.Append($" --web --web_port {webSettings.Port} --web_dbms {webSettings.DBMSType} --web_db {Tools.Runtime.EncodeCommandLineArgumentWin(webSettings.DatabaseConnectionString)}");
			}


			var location = Assembly.GetExecutingAssembly().Location;
			var fileName = location.EndsWith(".dll")
				? location.TrimEnd(".dll") + ".exe"
				: location;
			
			var info = new ProcessStartInfo {
				FileName = fileName,
				Arguments = args.ToString(), 
				ErrorDialog = false,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				Verb = "runas",
			};
			var started = Process.Start(info);

			await started.WaitForExitAsync();
			if (started.ExitCode != 0) {
				//if (Directory.Exists(destPath)) // DO NOT DELETE SINCE INSTALLING TWICE WILL REMOVE FILES BUT KEEP SERVICE
				//    await FileSystem.DeleteAllFilesAsync(destPath, true, true); // cleans up locked files
				var output = await started.StandardOutput.ReadToEndAsync();
				throw new SoftwareException(!string.IsNullOrWhiteSpace(output) ? output : "Error during service install.");
			}
		}

		public static async Task InstallService(string destPath, bool startAfterInstall) {
			var location = Assembly.GetExecutingAssembly().Location;
			var currentExePath = location.EndsWith(".dll")
				? location.TrimEnd(".dll") + ".exe"
				: location;

			try {
				if (Directory.Exists(destPath) && Directory.EnumerateFiles(destPath).Any()) {
					throw new SoftwareException("Path '{0}' already contains files", destPath);
				}

				Tools.FileSystem.CopyDirectory(Path.GetDirectoryName(currentExePath), destPath, true);

				var destExePath = Path.Combine(destPath, Path.GetFileName(currentExePath));
				Debug.Assert(File.Exists(destExePath));

				var info = new ProcessStartInfo {
					FileName = "sc.exe",
					Arguments = $"create \"BlockchainSQL Server\" binPath=\"{destExePath}\" start=auto",
					Verb = "runas",
					ErrorDialog = false,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true
				};

				var started = Process.Start(info);

				await started.WaitForExitAsync();

				if (started.ExitCode != 0) {
					var output = await started.StandardOutput.ReadToEndAsync();
					throw new SoftwareException(!string.IsNullOrWhiteSpace(output) ? output : "Error during service install.");
				}

				if (startAfterInstall) {
					var serviceController = GetServiceController();
					serviceController?.Start();
				}
			} catch {
				if (Directory.Exists(destPath))
					await Tools.FileSystem.DeleteDirectoryAsync(destPath, true);
				throw;
			}
		}

		public static async Task LaunchUninstallServiceProcess(string destPath) {
			Guard.ArgumentNotNullOrEmpty(destPath, nameof(destPath));
			if (String.IsNullOrWhiteSpace(destPath))
				throw new ArgumentNullException("destPath", destPath);

			if (!Directory.Exists(destPath))
				throw new ArgumentException("Service is not installed at '{0}'".FormatWith(destPath), nameof(destPath));

			var files = Directory.GetFiles(destPath, "*.exe").Where(f => !f.ToUpperInvariant().EndsWith(".VSHOST.EXE")).ToArray();
			if (files.Length == 0) {
				throw new SoftwareException("Service executable not found at '{0}'", destPath);
			}

			if (files.Length != 1) {
				throw new SoftwareException("Multiple executables found at '{0}'. Remove foreign executables and try again.", destPath);
			}

			var info = new ProcessStartInfo {
				UseShellExecute = false,
				FileName = files[0],
				Arguments = String.Format("uninstall --path \"{0}\"", destPath),
				ErrorDialog = false,
				Verb = "runas",
				RedirectStandardOutput = true,
			};
			var started = Process.Start(info);
		
			await started.WaitForExitAsync();
			
			if (started.ExitCode != 0) {
				throw new SoftwareException(await started.StandardOutput.ReadToEndAsync());
			}
			// Clean dangling files left due to file lock bugs from ManagedInstallerClass
			await Tools.FileSystem.DeleteDirectoryAsync(destPath);
		}

		public static async Task UninstallService(string destPath) {
			if (string.IsNullOrWhiteSpace(destPath))
				throw new ArgumentNullException(nameof(destPath), destPath);

			var serviceController = GetServiceController();
			if (serviceController != null) {
				if (serviceController.CanStop) {
					serviceController.Stop();
					serviceController.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
				}
			}

			if (!Directory.Exists(destPath))
				throw new ArgumentException("Service is not installed at '{0}'".FormatWith(destPath), nameof(destPath));

			var files = Directory.GetFiles(destPath, "*.exe").Where(f => !f.ToUpperInvariant().EndsWith(".VSHOST.EXE")).ToArray();
			if (files.Length == 0) {
				throw new SoftwareException("Service executable not found at '{0}'", destPath);
			}

			if (files.Length != 1) {
				throw new SoftwareException("Conflicting executables found at '{0}'. Remove foreign files ending with '-Service.exe'.", destPath);
			}

			var info = new ProcessStartInfo {
				UseShellExecute = false,
				FileName = "sc.exe",
				Arguments = $"delete \"BlockchainSQL Server\"",
				Verb = "runas",
				ErrorDialog = false,
				RedirectStandardOutput = true
			};

			var started = Process.Start(info);

			await started.WaitForExitAsync();

			if (started.ExitCode != 0) {
				var output = await started.StandardOutput.ReadToEndAsync();
				throw new SoftwareException(!string.IsNullOrWhiteSpace(output) ? output : "Error during service uninstall.");
			}

			await Tools.FileSystem.DeleteDirectoryAsync(destPath, true);

			GlobalSettings.Get<BlockchainDatabaseSettings>().Delete();
			GlobalSettings.Get<NodeSettings>().Delete();
			GlobalSettings.Get<ScannerSettings>().Delete();
			GlobalSettings.Get<WebSettings>().Delete();
		}

		public static async Task SetServiceRecovery(TimeSpan duration) {
			var info = new ProcessStartInfo("cmd.exe", string.Format("/c sc failure \"{1}\" reset=0 actions=restart/{0}/restart/{0}/restart/{0}", duration.Milliseconds, ServiceName));
			var process = Process.Start(info);
			await process.WaitForExitAsync();
		}

		public static ServiceController GetServiceController() {
			return ServiceController
					.GetServices()
					.FirstOrDefault(s => s.ServiceName == ServiceName);
		}

		public static bool IsInstalled() => ServiceController.GetServices().Any(s => s.ServiceName == ServiceName);
		
	}
}
