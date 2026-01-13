// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;

namespace BlockchainSQL.Server {
	[Flags]
	public enum CommandLineArgTraits {
		Optional = 1,  // must occur 0..1 times
		Mandatory = 2,  // must occur 1 time
		Multiple = 4,   // if optional, can occur 0..N times, if mandatory can occur 1..N times
	}
}
