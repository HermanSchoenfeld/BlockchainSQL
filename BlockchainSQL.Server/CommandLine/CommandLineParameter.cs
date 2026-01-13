// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

namespace BlockchainSQL.Server {
	public class CommandLineParameter {

		public CommandLineParameter(string name, CommandLineParameterOptions traits = CommandLineParameterOptions.Default, params string[] dependencies) 
			: this(name, string.Empty, traits, dependencies) {
		}

		public CommandLineParameter(string name, string description, CommandLineParameterOptions traits = CommandLineParameterOptions.Default, params string[] dependencies) {
			Name = name;
			Description = description;
			Traits = traits;
			Dependencies = dependencies ?? new string[0];
		}

		public string Name { get; init; }

		public string Description { get; init; }

		public CommandLineParameterOptions Traits { get; init; }

		public string[] Dependencies { get; init; }
	}
}
