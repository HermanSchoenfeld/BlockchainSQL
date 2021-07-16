using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockchainSQL.DataAccess;
using BlockchainSQL.DataObjects;
using Sphere10.Framework;
using Sphere10.Framework.Data;

namespace BlockchainSQL.Server
{
    public partial class SettingsControl : UserControl {
        public SettingsControl() {
            InitializeComponent();
            LoadDefaults();
        }

        public Result ValidateInputs() {
            var result = Result.Default;
            foreach (KnownSettings setting in Enum.GetValues(typeof (KnownSettings))) {
                string errorMessage;
                if (!ValidateSettingDisplayValue(setting, out errorMessage)) 
                    result.AddError(errorMessage);
            }
            return result;
        }

        public void LoadDefaults() {
            LoadInternal(BlockchainDatabase.GenerateDefaultSettings());
        }

        public async Task LoadFrom(DBMSType dbmsType, string connectionString) {
            LoadInternal(
                await Task.Run(() => BlockchainDatabase.NewDAC(dbmsType, connectionString).GetSettings())
            );
        }

        public async Task SaveTo(DBMSType dbmsType, string connectionString) {
            var validation = ValidateInputs();
            if (validation.Failure)
                throw new SoftwareException(validation.ErrorMessages.ToParagraphCase());

            var dac = BlockchainDatabase.NewDAC(dbmsType, connectionString);
            using (var scope = dac.BeginScope()) {
                scope.BeginTransaction();
                foreach (KnownSettings setting in Enum.GetValues(typeof(KnownSettings))) {
                    await Task.Run(() => dac.SetSettingValue(setting, GetSettingDisplayValue(setting)));
                }
                scope.Commit();
            }
        }


        private void LoadInternal(IEnumerable<Setting> settings) {
            foreach(var setting in settings)
                SetSettingDisplayValue((KnownSettings)setting.ID, setting.Value);
        }

        private bool ValidateSettingDisplayValue(KnownSettings setting, out string errorMessage) {
            errorMessage = null;
            switch (setting) {
                case KnownSettings.StoreScriptData:
                    return true;
                case KnownSettings.NetworkPeer1:
                    var ip1Text = _node1TextBox.Text;
                    if (string.IsNullOrWhiteSpace(ip1Text))
                        return true;
                    IPAddress ip1;
                    if (!IPAddress.TryParse(ip1Text, out ip1)) {
                        errorMessage = "Node 1 is not a valid IP address";
                        return false;
                    }
                    return true;
                case KnownSettings.NetworkPeer1Port:
                    var portVal = _nodeCustomPortIntBox.Value;
                    if (portVal == null)
                        return true;
                    if (ushort.MinValue <= portVal.Value && portVal.Value <= ushort.MaxValue) {
                        return true;
                    }
                    errorMessage = "Trusted Node Port must be within 0 and 65535 (or blank for default)";
                    return false;
                case KnownSettings.NetworkPeer1PollRate:
                    var pollRate = _nodePollRateIntBox.Value;
                    if (pollRate == null) {
                        errorMessage = "Node Poll Rate must have a value";
                        return false;
                    }
                    if (1 <= pollRate.Value && pollRate.Value <= TimeSpan.FromMinutes(10).TotalSeconds) {
                        return true;
                    }
                    errorMessage = "Node Poll Rate must be between 1 second and 10 minutes";
                    return false;                    
                case KnownSettings.MaxMemoryBufferSize:
                    var val = _maxMemoryIntBox.Value;
                    if (val != null) {
                        if (10 <= val && val <= 10000)
                            return true;
                        errorMessage = "Max Memory must be within 10mb and 1TB";
                        return false;
                    }
                    errorMessage = "Max Memory has no value";
                    return false;
                default:
                    throw new NotSupportedException(setting.ToString());
            }
        }
        private void SetSettingDisplayValue(KnownSettings setting, string value) {
            switch (setting) {
                case KnownSettings.StoreScriptData:
                    _optionsListBox.SetItemChecked(SettingIndex(setting), Tools.Parser.Parse<bool>(value));
                    break;
                case KnownSettings.NetworkPeer1:
                    _node1TextBox.Text = value;
                    break;
                case KnownSettings.NetworkPeer1Port:
                    _nodeCustomPortIntBox.Text =value;
                    break;
                case KnownSettings.NetworkPeer1PollRate:
                    _nodePollRateIntBox.Text = value;
                    break;
                case KnownSettings.MaxMemoryBufferSize:
                    _maxMemoryIntBox.Text = value;
                    break;
                default:
                    throw new NotSupportedException(setting.ToString());
            }
        }

        private string GetSettingDisplayValue(KnownSettings setting) {
            switch (setting) {
                case KnownSettings.StoreScriptData:
                    return _optionsListBox.GetItemChecked(SettingIndex(setting)) ? "1" : "0";
                case KnownSettings.NetworkPeer1:
                    var ip1Text = _node1TextBox.Text;
                    return string.IsNullOrWhiteSpace(ip1Text) ? null : IPAddress.Parse(ip1Text).ToString();
                case KnownSettings.NetworkPeer1Port:
                    return _nodeCustomPortIntBox.Text;
                case KnownSettings.NetworkPeer1PollRate:
                    return _nodePollRateIntBox.Text;
                case KnownSettings.MaxMemoryBufferSize:
                    return _maxMemoryIntBox.Text;
                default:
                    throw new NotSupportedException(setting.ToString());
            }            
        }

        private int SettingIndex(KnownSettings setting) {
            switch (setting) {
                case KnownSettings.StoreScriptData:
                    return 0;
                case KnownSettings.NetworkPeer1:
                case KnownSettings.NetworkPeer1Port:
                case KnownSettings.NetworkPeer1PollRate:
                case KnownSettings.MaxMemoryBufferSize:
                default:
                    throw new NotSupportedException(setting.ToString());
            }
        }

    }
}
