// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Threading.Tasks;
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
			return new ActionScope(() => {
				if (shouldClose && source.IsOpen) {
					Tools.Exceptions.ExecuteIgnoringException(source.Close);
				}
			}
			);
		}

	}
}
