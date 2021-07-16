using System;
using Sphere10.Framework;
namespace BlockchainSQL.Processing {
	public class FastDifficultyCalculator : IDifficultyCalculator {
        static readonly float MaxBody = (float)Math.Log(0x00ffff);
        static readonly float Scaland = (float)Math.Log(256);

        public virtual float CalculateDifficulty(uint bits) {
            //var x =  (float)Math.Exp(MaxBody - Math.Log(bits & 0x00ffffff) + Scaland * (0x1d - ((bits & 0xff000000) >> 24)));
            var x =  (float)FastExp.Exp(MaxBody - FastLog.Ln(bits & 0x00ffffff) + Scaland * (0x1d - ((bits & 0xff000000) >> 24)));
            return x;
        }

    }

}
