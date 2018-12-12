using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sphere10.Framework.Maths;

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
