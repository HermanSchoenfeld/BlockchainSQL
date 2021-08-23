//-----------------------------------------------------------------------
// <copyright file="ModuleConfiguration.cs" company="Sphere 10 Software">
//
// Copyright (c) Sphere 10 Software. All rights reserved. (http://www.sphere10.com)
//
// Distributed under the MIT software license, see the accompanying file
// LICENSE or visit http://www.opensource.org/licenses/mit-license.php.
//
// <author>Herman Schoenfeld</author>
// <date>2021</date>
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;

namespace Sphere10.Framework.Application {
	public class ModuleConfiguration : ModuleConfigurationBase {

		public override int Priority => int.MinValue; // last to execute

		public override void RegisterComponents(ComponentRegistry registry) {

			// Components: share settings with web/server
			registry.RegisterComponentInstance<ISettingsProvider>(new CachedSettingsProvider(new DirectorySettingsProvider(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BlockchainSQL"))), "UserSettings");
			registry.RegisterComponentInstance<ISettingsProvider>(new CachedSettingsProvider(new DirectorySettingsProvider(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BlockchainSQL"))), "SystemSettings");
			registry.RegisterInitializationTask<IncrementUsageByOneTask>();
		}
	}
}
