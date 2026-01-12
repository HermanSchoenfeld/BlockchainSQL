// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.Server {
	public class CommandLineCommand : CommandLineParameter {

		public CommandLineCommand(string name, CommandLineParameter[] parameters = null, CommandLineCommand[] subCommands = null, params string[] dependencies)
			: this(name, string.Empty, parameters, subCommands, dependencies) {
		}

		public CommandLineCommand(string name, string description, CommandLineParameter[] parameters = null, CommandLineCommand[] subCommands = null, params string[] dependencies) 
			: base(name, description, default, dependencies) {
			Parameters = parameters ?? new CommandLineParameter[0];
			SubCommands = subCommands ?? new CommandLineCommand[0];
		}

		public CommandLineParameter[] Parameters { get; init; }
		
		public CommandLineCommand[] SubCommands { get; init; }
	}
}
