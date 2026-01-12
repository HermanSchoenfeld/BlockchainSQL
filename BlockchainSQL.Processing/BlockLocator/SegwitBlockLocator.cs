// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System.Linq;

namespace BlockchainSQL.Processing {

	/// <summary>
	/// Used for segwit testing/diagnostics
	/// </summary>
	internal class SegwitBlockLocator : BlockLocator {
		public override BlockLocators GetBlockLocators() {
			const long SegwitStartBlock = 481824;
			const long PreSegwitRange = 1000;
			const long StartBlock = SegwitStartBlock - PreSegwitRange;
			long[] indices =  DetermineBlockLocatorIndices(StartBlock);
			
			return new BlockLocators {
				Locations = base.DAC.GetBlockLocators(indices).ToArray()
			};
		}

	}
}
