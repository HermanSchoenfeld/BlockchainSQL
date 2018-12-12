using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Processing {
    public class SettingsCache {

        private readonly ICache<KnownSettings, string> _valueCache;

        public SettingsCache() {
            _valueCache = new ActionCache<KnownSettings, string>(GetSettingValue);            
        }

        public bool StoreScriptData {
            get { return Tools.Parser.Parse<bool>(_valueCache[KnownSettings.StoreScriptData]); }
            set { SetSettingValue(KnownSettings.StoreScriptData, value); }
        }

        public long MaxMemoryBufferSize {
            get { return long.Parse(_valueCache[KnownSettings.MaxMemoryBufferSize]); }
            set { SetSettingValue(KnownSettings.MaxMemoryBufferSize, value); }
        }
        public string NetworkPeer1 {
            get { return _valueCache[KnownSettings.NetworkPeer1]; }
            set { SetSettingValue(KnownSettings.NetworkPeer1, value); }
        }

        public TimeSpan NetworkPeerPollRate {
            get { return TimeSpan.FromSeconds(int.Parse(_valueCache[KnownSettings.NetworkPeer1PollRate])); }
            set { SetSettingValue(KnownSettings.NetworkPeer1PollRate, (int) value.TotalSeconds); }
        }

        private string GetSettingValue(KnownSettings setting) {
            return BizLogicScope.Current.DAC.GetSettingValue<string>((int) setting);
        }

        private void SetSettingValue(KnownSettings setting, object value) {
            if (value != null) {
                TypeSwitch.Do(value,
                    TypeSwitch.Case<string>(() => Tools.Lambda.NoOp()),
                    TypeSwitch.Case<bool>((b) => value = b ? "1" : "0"),
                    TypeSwitch.Case<Mode>((m) => value = (int) m),
                    TypeSwitch.Case<long>((m) => value = (int)m),
                    TypeSwitch.Default(() => { throw new Exception("Internal Error"); })
                    );
            }
            using (var scope = BizLogicScope.Current.DAC.BeginScope()) {
                var isRoot = scope.IsRootScope;
                BizLogicScope.Current.DAC.SetSettingValue((int) setting, value);
            }
            _valueCache.Invalidate(setting);
        }

    }
}
