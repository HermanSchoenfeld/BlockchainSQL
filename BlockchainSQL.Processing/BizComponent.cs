using BlockchainSQL.DataAccess;
using Sphere10.Framework;

namespace BlockchainSQL.Processing
{
    public abstract class BizComponent : IBizComponent {
        private readonly BizLogicScope _scope;

        protected BizComponent() {
            _scope = BizLogicScope.Current;
        }
        public ApplicationDAC CustomDAC { get; set; }
        public virtual ILogger Log => _scope.Log;
        public virtual ApplicationDAC DAC => CustomDAC ?? _scope.DAC;

        public virtual SettingsCache Settings => _scope.Settings;

        public virtual ApplicationDAC CreateDAC() {
            return _scope.CreateDAC();
        }
    }
}
