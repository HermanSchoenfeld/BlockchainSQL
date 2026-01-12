// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;

namespace BlockchainSQL.Server {
	[Flags]
	public enum CommandLineArgumentOptions {
		SingleDash = 1 << 0,
		DoubleDash = 1 << 1,
		ForwardSlash = 1 << 2,
		CaseSensitive = 1 << 3,
		PrintHelpOnH = 1 << 4,
		PrintHelpOnHelp = 1 << 5,
		Default = DoubleDash | PrintHelpOnH | PrintHelpOnHelp
	}
}
