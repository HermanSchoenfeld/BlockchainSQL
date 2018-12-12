using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainSQL.Processing {
    public interface IDifficultyCalculator {
        float CalculateDifficulty(uint bits);
    }
}
