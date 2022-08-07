using System;
using System.Threading.Tasks;
using Hydrogen;

namespace BlockchainSQL.Processing {
	public static class BlockchainSourceExtensions {

		public static async Task<IDisposable> EnterOpenScope(this IBlockStream source) {
			var shouldClose = true;
			if (!source.IsOpen) {
				await Task.Run(() => source.Open());
				shouldClose = true;
			} else {
				shouldClose = false;
			}
			return new ActionScope(() => {
				if (shouldClose && source.IsOpen) {
					Tools.Exceptions.ExecuteIgnoringException(source.Close);
				}
			}
			);
		}

	}
}
