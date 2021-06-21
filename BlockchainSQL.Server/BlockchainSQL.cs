using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.Server.Service;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace Tools
{
    public class BlockchainSQL {
        public const string ServiceName = "BlockchainSQL Server";


        public static async Task LaunchInstallServiceProcess(string destPath, DBReference database) {
	        var location = Assembly.GetExecutingAssembly().Location;
	        var fileName = location.EndsWith(".dll")
		        ? location.TrimEnd(".dll") + ".exe"
		        : location;
	        
	        var info = new ProcessStartInfo {
                UseShellExecute = true, 
                FileName = fileName, 
                Arguments = String.Format("-install \"{0}\" \"{1}\" \"{2}\"", destPath, database.DBMSType, database.ConnectionString), 
                ErrorDialog = false, 
                Verb = "runas",
            };            
            var started = Process.Start(info);
            var output = new StringBuilder();
            started.OutputDataReceived += (sender, args) => {
                output.Append(args.Data);
            };
           
            await started.WaitForExitAsync();
            if (started.ExitCode != 0) {
               //if (Directory.Exists(destPath)) // DO NOT DELETE SINCE INSTALLING TWICE WILL REMOVE FILES BUT KEEP SERVICE
                //    await FileSystem.DeleteAllFilesAsync(destPath, true, true); // cleans up locked files
                var outputMessage = output.ToString();
                throw new SoftwareException(!String.IsNullOrWhiteSpace(outputMessage) ? outputMessage : "Unexpected error");
            }
        }

        public static async Task InstallService(string destPath, DBReference database) {
			var location = Assembly.GetExecutingAssembly().Location;
			var currentExePath = location.EndsWith(".dll")
				? location.TrimEnd(".dll") + ".exe"
				: location;


			try {
                if (Directory.Exists(destPath) && Directory.EnumerateFiles(destPath).Any()) {
                    throw new SoftwareException("Path '{0}' already contains files", destPath);
                }
                
                await FileSystem.CopyDirectoryAsync(Path.GetDirectoryName(currentExePath), destPath, true, true, true);
                var destExePath = Path.Combine(destPath, Path.GetFileName(currentExePath));               
                Debug.Assert(File.Exists(destExePath));
                await DatabaseReferenceFileManager.CreateDatabaseConnectionFile(destExePath, database);
                
				ProcessStartInfo info = new ProcessStartInfo {
					UseShellExecute = true,
					FileName = "sc.exe",
					Arguments = $"create \"BlockchainSQL Server\" binPath=\"{destExePath}\" start=auto",
					Verb = "runas",
					ErrorDialog = false
				};

				var output = new StringBuilder();
				var started = Process.Start(info);
				started.OutputDataReceived += (sender, args) => {
					output.Append(args.Data);
				};

				await started.WaitForExitAsync();

				if (started.ExitCode != 0) {		
					var outputMessage = output.ToString();
					throw new SoftwareException(!string.IsNullOrWhiteSpace(outputMessage) ? outputMessage : "Error during service install.");
				}

				try {
                    var serviceController = GetServiceController();
                    serviceController?.Start();
                } catch {
	                // ignored
                }
            } catch {
                if (Directory.Exists(destPath))
                    await FileSystem.DeleteDirectoryAsync(destPath, true);
                throw;
            }
        }

        public static async Task LaunchUninstallServiceProcess(string destPath) {

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
                UseShellExecute = true,
                FileName = files[0],
                Arguments = String.Format("-uninstall \"{0}\"", destPath), 
                ErrorDialog = false, 
                Verb = "runas", 
                RedirectStandardOutput = false,
            };
            var started = Process.Start(info);
            var output = new StringBuilder();
            started.OutputDataReceived += (sender, args) => {
                output.Append(args.Data);
            };
            await Task.Run(() => started.WaitForExit());
            if (started.ExitCode != 0) {
                throw new SoftwareException(output.ToString());
            }
            // Clean dangling files left due to file lock bugs from ManagedInstallerClass
            await FileSystem.DeleteDirectoryAsync(destPath);
        }

        public static async Task UninstallService(string destPath) {
            if (String.IsNullOrWhiteSpace(destPath))
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

			ProcessStartInfo info = new ProcessStartInfo() {
				UseShellExecute = true,
				FileName = "sc.exe",
				Arguments = $"delete \"BlockchainSQL Server\"",
				Verb = "runas",
				ErrorDialog = false
			};

			var output = new StringBuilder();
			var started = Process.Start(info);
			started.OutputDataReceived += (sender, args) => {
				output.Append(args.Data);
			};

			await started.WaitForExitAsync();

			if (started.ExitCode != 0) {
				var outputMessage = output.ToString();
				throw new SoftwareException(!string.IsNullOrWhiteSpace(outputMessage) ? outputMessage : "Error during service uninstall.");
			}

			await FileSystem.DeleteDirectoryAsync(destPath, true);
        }


        public static async Task SetServiceRecovery(TimeSpan duration ) {
            var info = new ProcessStartInfo("cmd.exe", string.Format("/c sc failure \"{1}\" reset=0 actions=restart/{0}/restart/{0}/restart/{0}", duration.Milliseconds, ServiceName));
            var process = System.Diagnostics.Process.Start(info);
            await Task.Run( () => process.WaitForExit());
        }

        public static ServiceController GetServiceController() {
            return ServiceController
                    .GetServices()
                    .FirstOrDefault(s => s.ServiceName == ServiceName);
        }
    }
}
