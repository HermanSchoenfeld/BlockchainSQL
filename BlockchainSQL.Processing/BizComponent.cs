// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <***REDACTED_EMAIL***>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using BlockchainSQL.DataAccess;
using Sphere10.Framework;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Processing {
	public abstract class BizComponent : IBizComponent {
        private readonly BizLogicScope _scope;

		protected BizComponent() {
			_scope = BizLogicScope.Current;
		}

		public ApplicationDAC CustomDAC { get; set; }

        public virtual ILogger Log => _scope.Log;

        public virtual ApplicationDAC DAC => CustomDAC ?? _scope.DAC;

		public virtual ISettingsProvider Settings => _scope.Settings;

        public virtual ApplicationDAC CreateDAC() {
            return _scope.CreateDAC();
        }
    }
}
