using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;

namespace BlockchainSQL.DataAccess {
    public static class ApplicationDACExtensions {

        public static Setting GetByID(this ApplicationDAC dac, KnownSettings setting) {
            return dac.GetSettingByID((int) setting);
        }

        public static T GetSettingValue<T>(this ApplicationDAC dac, KnownSettings setting) {
            return dac.GetSettingValue<T>((int) setting);
        }

        public static void SetSettingValue<T>(this ApplicationDAC dac, KnownSettings setting, T value) {
            dac.SetSettingValue((int)setting, (object)value);
        }

    }
}
