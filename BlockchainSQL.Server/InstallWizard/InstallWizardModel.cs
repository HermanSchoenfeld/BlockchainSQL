// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.Processing;
using System;
using System.IO;

namespace BlockchainSQL.Server {
	public class InstallWizardModel {

		public bool StartAfterInstall { get; set; }

		public string ServiceDirectory { get; set; }

		public ServiceDatabaseSettings ServiceDatabaseSettings { get; set; }

		public ServiceNodeSettings NodeSettings { get; set; }

		public ServiceScannerSettings ScannerSettings { get; set; }

		public WebSettings WebSettings { get; set; }

		public static InstallWizardModel Default => new InstallWizardModel {
			ServiceDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "BlockchainSQL"),
			StartAfterInstall = true,
			ScannerSettings = new ServiceScannerSettings(),
			ServiceDatabaseSettings = new ServiceDatabaseSettings(),
			NodeSettings = new ServiceNodeSettings(),
			WebSettings = new WebSettings()
		};
	}

}
