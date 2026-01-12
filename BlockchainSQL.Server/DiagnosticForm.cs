using System;
using System.ComponentModel;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;

namespace BlockchainSQL.Server
{

    public partial class DiagnosticForm : FormEx {
        

        public DiagnosticForm() {
            InitializeComponent();
            _dbConnectionBar.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
            GeneratedDatabase = null;
            Disconnect();
            _serviceStatusControl.Start();
        }

        protected override void PopulatePrimingData() {
            LoadFormSettings();
        }

        public bool Connected { get; set; }

        public DBReference? GeneratedDatabase { get; set; }

        #region Event Handlers

        private void _connectButton_Click(object sender, EventArgs e) {
            try {
                if (!Connected)
                    Connect();
                else
                    Disconnect();
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private void _disableIndexesButton_Click(object sender, EventArgs e) {
            try {
                throw new NotImplementedException();
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private void _enableIndexesButton_Click(object sender, EventArgs e) {
            try {
                throw new NotImplementedException();
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private void _shrinkDatabaseButton_Click(object sender, EventArgs e) {
            try {
                throw new NotImplementedException();
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private void _postProcessButton_Click(object sender, EventArgs e) {
            try {
                throw new NotImplementedException();
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private async void _saveSettingsButton_Click(object sender, EventArgs e) {
            try {
                //await _settingsControl.SaveTo(_dbConnectionBar.SelectedDBMSType, _dbConnectionBar.ConnectionString);
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private void _logsButton_Click(object sender, EventArgs e) {
            try {
                
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }


        #endregion

        #region Auxillary Methods

        private async void Connect() {
            using (_loadingCircle.BeginAnimationScope(this)) {
                var connectionResult = await _dbConnectionBar.TestConnection();
                if (!connectionResult.IsSuccess)
                    throw new SoftwareException(connectionResult);                             
                _connectButton.Text = "Disconnect";
            }
            Connected = true;
            _optionsGroupBox.Enabled = true;
            _toolsGroupBox.Enabled = true;
            _dbConnectionBar.Enabled = false;            
            SaveFormSettings();
            //await _settingsControl.LoadFrom(_dbConnectionBar.SelectedDBMSType, _dbConnectionBar.ConnectionString);
        }

        private void Disconnect() {
            _connectButton.Text = "Connect";
            Connected = false;
            _optionsGroupBox.Enabled = false;
            _toolsGroupBox.Enabled = false;
            _dbConnectionBar.Enabled = true;
        }

        private void LoadFormSettings() {
            try {
                var settings = UserSettings.Get<FormSettings>();
                _dbConnectionBar.SelectedDBMSType = settings.DBMSType;
                _dbConnectionBar.ConnectionString = settings.ConnectionString;
            } catch {
                // dont propagate error (usually caused by programmer changing object type
            }
        }

        private void SaveFormSettings() {
            var settings = UserSettings.Get<FormSettings>();
            settings.DBMSType = _dbConnectionBar.SelectedDBMSType;
            settings.ConnectionString = _dbConnectionBar.ConnectionString;
            settings.Save();
        }

        #endregion

        #region Internal types
        public class FormSettings : SettingsObject {

			public DBMSType DBMSType { get; set; } = DBMSType.SQLServer;


			public string ConnectionString { get; set; }

			public string DataGenRadioBox { get; set; } = "_primingRadioButton";


		}

		#endregion
	}
}
