using Sphere10.Framework;
using Sphere10.Framework.Application;
using System.ComponentModel;
using System.Net;

namespace BlockchainSQL.Processing {
	public class NodeSettings : SettingsObject {
		public string IP { get; set; } = "127.0.0.1";

		public int Port { get; set; } = 8333;

		public int PollRateSEC { get; set; } = 10;

		public override Result Validate() {
			var result = Result.Default;

			if (!IPAddress.TryParse(IP, out _))
				result.AddError($"IP address is invalid: {IP??"(NULL)"}");

			if (Port < 0 || Port > 65535)
				result.AddError($"Port was not a valid TCP/IP port: {Port}");

			if (PollRateSEC < 1 || PollRateSEC > 1000) 
				result.AddError($"PollRateSEC must be between 1 and 1000 inclusive: {PollRateSEC}");
			
			return result;
		}
	}

}