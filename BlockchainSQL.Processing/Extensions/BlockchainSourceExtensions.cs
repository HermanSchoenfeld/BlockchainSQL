using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BlockchainSQL.Processing.Scanning;
using Sphere10.Framework;

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
            return new ActionScope(
                Tools.Lambda.NoOp,
                () => {
                    if (shouldClose && source.IsOpen) {
                        Tools.Exceptions.ExecuteIgnoringException(source.Close);
                    }
                }
            );
        }

    }
}
