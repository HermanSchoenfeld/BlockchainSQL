using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockchainSQL.DataAccess;
using Sphere10.Framework;
using Sphere10.Framework.Application;
using Sphere10.Framework.Data;
using Sphere10.Framework.Windows.Forms;

namespace BlockchainSQL.Server {

    public partial class GenerateDatabaseForm : FormEx {
        public GenerateDatabaseForm() {
            InitializeComponent();
            _databaseConnectionPanel.IgnoreDBMS = new[] { DBMSType.Sqlite, DBMSType.Firebird, DBMSType.FirebirdFile };
            GeneratedDatabase = null;
        }

        protected override void PopulatePrimingData() {
            LoadFormSettings();
        }

        public DBReference? GeneratedDatabase { get; set; }

        #region Event Handlers
        private async void _generateDatabaseButton_Click(object sender, EventArgs e) {
            try {
                using (_loadingCircle.BeginAnimationScope(this)) {
                    var dbmsType = _databaseConnectionPanel.SelectedDBMSType;
                    var connectionString = _databaseConnectionPanel.ConnectionString;
                    var databaseName = _databaseConnectionPanel.DatabaseName;
                    if (await GenerateDatabase(DatabaseGenerationDataPolicy.PrimingData, dbmsType, connectionString, databaseName)) {
                        SaveFormSettings();
                        GeneratedDatabase = _databaseConnectionPanel.Database;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        private async void _testButton_Click(object sender, EventArgs e) {
            try {
                Result testResult;
                using (_loadingCircle.BeginAnimationScope(this)) {
                    testResult = await TestConnection();
                }
                if (testResult.Failure)
                    MessageBox.Show(this, testResult.ErrorMessages.ToParagraphCase(), "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(this, "Success", "Connection Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception error) {
                ExceptionDialog.Show(this, error);
            }
        }

        #endregion

        #region Auxillary Methods

        private async Task<Result> TestConnection() {
            var result = Result.Default;
            try {
                this.EnableChildren(false);
                _loadingCircle.Enabled = true;
                _loadingCircle.Active = true;
                result = await _databaseConnectionPanel.TestConnection();
            } finally {
                this.EnableChildren(true);
                _loadingCircle.Active = false;
            }
            return result;
        }

        private async Task<bool> GenerateDatabase(DatabaseGenerationDataPolicy dataPolicy, DBMSType dbmsType, string connectionString, string databaseName) {
            using (_loadingCircle.BeginAnimationScope()) {
                var settingsValidation = _settingsControl.ValidateInputs(); 
                if (settingsValidation.Failure)
                    throw new SoftwareException(settingsValidation.ErrorMessages.ToParagraphCase(true));    
                
                var dropExisting = false;
                var createShell = false;
                var createDatabase = false;
                var schemaGenerator = DataAccessFactory.NewDatabaseGenerator(dbmsType);
                if (await Task.Run(() => schemaGenerator.DatabaseExists(connectionString))) {
                    switch (
                        QuestionDialog.Show(
                            this,
                            SystemIconType.Warning,
                            "Database Already Exists",
                            "Database already exists. What would you like to do?",
                            "&Cancel", "&Overwrite", "&Append")
                        ) {
                            case DialogExResult.Button1:
                                return false;
                            case DialogExResult.Button2:
                                dropExisting = true;
                                createShell = true;
                                createDatabase = true;
                                break;
                            case DialogExResult.Button3:
                                createDatabase = true;
                                break;
                    }
                } else {
                    createShell = true;
                    createDatabase = true;
                }

                if (dropExisting)
                    await Task.Run(() => schemaGenerator.DropDatabase(connectionString));

                if (createShell)
                    await Task.Run(() => schemaGenerator.CreateEmptyDatabase(connectionString));

                if (createDatabase)
                    await Task.Run(() => schemaGenerator.CreateNewDatabase(connectionString, dataPolicy, databaseName));

                await _settingsControl.SaveTo(dbmsType, connectionString);

                MessageBox.Show(this, "Success", "Database Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
        }


        private void LoadFormSettings() {
            try {
                var settings = UserSettings.Get<FormSettings>();
                _databaseConnectionPanel.SelectedDBMSType = settings.DBMSType;
                _databaseConnectionPanel.ConnectionString = settings.ConnectionString;
            } catch {
                // dont propagate error (usually caused by programmer changing object type
            }
        }

        private void SaveFormSettings() {
            var settings = UserSettings.Get<FormSettings>();
            settings.DBMSType = _databaseConnectionPanel.SelectedDBMSType;
            settings.ConnectionString = _databaseConnectionPanel.ConnectionString;
            settings.Save();
        }


        #endregion

        #region Internal types
        public class FormSettings : SettingsObject {

            [DefaultValue(DBMSType.SQLServer)]
            public DBMSType DBMSType { get; set; }

            public string ConnectionString { get; set; }

            [DefaultValue("_primingRadioButton")]
            public string DataGenRadioBox { get; set; }

        }

        #endregion

    }
}
