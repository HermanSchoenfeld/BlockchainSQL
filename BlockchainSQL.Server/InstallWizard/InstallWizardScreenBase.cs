// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using Sphere10.Framework.Windows.Forms;

namespace BlockchainSQL.Server {
	// This base class is needed to stop WinForms designer from throwing. This class cannot be designed by it's descendents can. This is due to the generic base.
	public class InstallWizardScreenBase : WizardScreen<InstallWizardModel> { 
    }

}
