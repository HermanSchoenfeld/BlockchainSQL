using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sphere10.Framework;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;
using Sphere10.Framework.Application;

namespace BlockchainSQL.Server
{
    public partial class ServiceInstallDialog : FormEx {

        public ServiceInstallDialog() {
            InitializeComponent();
            _databaseConnectionPanel.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
        }

        protected override void PopulatePrimingData() {
            base.PopulatePrimingData();
            LoadFormSettings();
        }


        public async Task<Result> ValidateDestinationPath() {
            var result = Result.Default;
            var path = _pathSelector.Path;
            if (!Tools.FileSystem.IsWellFormedDirectoryPath(path)) {
                result.AddError("Malfomed destination path '{0}'", path);
            } else if (Directory.Exists(path)) {
                var files = Directory.GetFiles(path);
                if (files.Length > 0) {
                    var exeFile = Path.GetFileName(Assembly.GetExecutingAssembly().Location).ToUpperInvariant();                    
                    if (files.Select(Path.GetFileName).Select(s => s.ToUpperInvariant()).Contains(exeFile)) {
                        result.AddError("Service already installed in '{0}'", path);
                    } else {
                        if (QuestionDialog.Show(this, SystemIconType.Question, "Path already exists", "Destination directory '{0}' contains files. If you continue this will delete them?".FormatWith(path), "Continue", "Cancel") == DialogExResult.Button2) {
                            result.AddError("Path '{0}' contains files", path);
                        } 
                    }                    
                }
            }
            return result;
        }


        public Task<Result> ValidateDatabase() {
            return _databaseConnectionPanel.TestConnection();
        }

        public async Task Validate() {
            var pathValidation = await ValidateDestinationPath();
            if (pathValidation.Failure) {
                throw new SoftwareException(pathValidation);
            }

            var dbValidation = await ValidateDatabase();
            if (dbValidation.Failure)
                throw  new SoftwareException(dbValidation);
        }

        public Task Install() {
            return Tools.BlockchainSQL.LaunchInstallServiceProcess(
                _pathSelector.Path,
                _databaseConnectionPanel.Database
            );
        }
        
        private async void _installButton_Click(object sender, EventArgs e) {
            try {
                using (_loadingCircle.BeginAnimationScope(this)) {
                    SaveFormSettings();
                    await Validate();
                    await Install();                                        
                }
                MessageBox.Show(this, "Success", "Service was installed and started", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private async void _testConnectionButon_Click(object sender, EventArgs e) {
            try {
                using (_loadingCircle.BeginAnimationScope(this)) {
                    var result = await ValidateDatabase();
                    if (result.Failure)
                        MessageBox.Show(this, result.ErrorMessages.ToParagraphCase(), "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show(this, "Success", "Connection Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private async void _generateNewDatabaseButton_Click(object sender, EventArgs e) {
            try {
                var dialog = new GenerateDatabaseForm();
                dialog.ShowDialog(this);
                if (dialog.DialogResult == DialogResult.OK) {
                    if (dialog.GeneratedDatabase != null) {
                        this._databaseConnectionPanel.SelectedDBMSType = dialog.GeneratedDatabase.Value.DBMSType;
                        this._databaseConnectionPanel.ConnectionString = dialog.GeneratedDatabase.Value.ConnectionString;
                    }
                }
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        protected virtual void LoadFormSettings() {
            using (this.EnterUpdateScope()) {
                var settings = UserSettings.Get<FormSettings>();
                _pathSelector.Path = settings.DestFolder;
                _passwordTextBox.Text = settings.Password;
                _databaseConnectionPanel.SelectedDBMSType = settings.DBMS;
                _databaseConnectionPanel.ConnectionString = settings.ConnectionString;
            }
        }

        protected virtual void SaveFormSettings() {
            var settings = UserSettings.Get<FormSettings>();
            settings.DestFolder = _pathSelector.Path;
            settings.Password = _passwordTextBox.Text;
            settings.DBMS = _databaseConnectionPanel.SelectedDBMSType;
            settings.ConnectionString = _databaseConnectionPanel.ConnectionString;
#if DEBUG
			settings.Save();
#endif
        }

        public class FormSettings : SettingsObject {
            public string DestFolder { get; set; }

            public string Password { get; set; }

            [DefaultValue(DBMSType.SQLServer)]
            public DBMSType DBMS { get; set; }

            public string ConnectionString { get; set; }

        }
    }

}
