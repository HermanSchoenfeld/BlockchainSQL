// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;

namespace BlockchainSQL.Processing {
	public class DifficultyCalculator : IDifficultyCalculator {
        static readonly float MaxBody = (float)Math.Log(0x00ffff);
        static readonly float Scaland = (float)Math.Log(256);

        public virtual float CalculateDifficulty(uint bits) {
            var x =  (float)Math.Exp(MaxBody - Math.Log(bits & 0x00ffffff) + Scaland * (0x1d - ((bits & 0xff000000) >> 24)));
            return x;
        }

    }

}
