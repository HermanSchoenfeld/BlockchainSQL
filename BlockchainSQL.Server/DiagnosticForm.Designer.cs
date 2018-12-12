using BlockchainSQL.Server;

namespace BlockchainSQL.Server {
    partial class DiagnosticForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this._optionsGroupBox = new System.Windows.Forms.GroupBox();
            this._settingsControl = new BlockchainSQL.Server.SettingsControl();
            this._serviceStatusGroupBox = new System.Windows.Forms.GroupBox();
            this._serviceStatusControl = new BlockchainSQL.Server.ServiceStatusControl();
            this._dbConnectionBar = new Sphere10.Windows.WinForms.DatabaseConnectionBar();
            this._connectButton = new System.Windows.Forms.Button();
            this._databaseGroupBox = new System.Windows.Forms.GroupBox();
            this._toolsGroupBox = new System.Windows.Forms.GroupBox();
            this._saveSettingsButton = new System.Windows.Forms.Button();
            this._logsButton = new System.Windows.Forms.Button();
            this._disableIndexesButton = new System.Windows.Forms.Button();
            this._enableIndexesButton = new System.Windows.Forms.Button();
            this._shrinkDatabaseButton = new System.Windows.Forms.Button();
            this._postProcessButton = new System.Windows.Forms.Button();
            this._loadingCircle = new Sphere10.Windows.WinForms.LoadingCircle();
            this._optionsGroupBox.SuspendLayout();
            this._serviceStatusGroupBox.SuspendLayout();
            this._databaseGroupBox.SuspendLayout();
            this._toolsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _optionsGroupBox
            // 
            this._optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsGroupBox.Controls.Add(this._settingsControl);
            this._optionsGroupBox.Location = new System.Drawing.Point(6, 157);
            this._optionsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this._optionsGroupBox.Name = "_optionsGroupBox";
            this._optionsGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this._optionsGroupBox.Size = new System.Drawing.Size(731, 216);
            this._optionsGroupBox.TabIndex = 5;
            this._optionsGroupBox.TabStop = false;
            this._optionsGroupBox.Text = "Options";
            // 
            // _settingsControl
            // 
            this._settingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._settingsControl.Location = new System.Drawing.Point(2, 15);
            this._settingsControl.Name = "_settingsControl";
            this._settingsControl.Padding = new System.Windows.Forms.Padding(5);
            this._settingsControl.Size = new System.Drawing.Size(724, 196);
            this._settingsControl.TabIndex = 0;
            // 
            // _serviceStatusGroupBox
            // 
            this._serviceStatusGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._serviceStatusGroupBox.Controls.Add(this._serviceStatusControl);
            this._serviceStatusGroupBox.Location = new System.Drawing.Point(5, 12);
            this._serviceStatusGroupBox.Name = "_serviceStatusGroupBox";
            this._serviceStatusGroupBox.Size = new System.Drawing.Size(732, 53);
            this._serviceStatusGroupBox.TabIndex = 9;
            this._serviceStatusGroupBox.TabStop = false;
            this._serviceStatusGroupBox.Text = "Service Status";
            // 
            // _serviceStatusControl
            // 
            this._serviceStatusControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._serviceStatusControl.AutoDetectChildStateChanges = false;
            this._serviceStatusControl.Location = new System.Drawing.Point(9, 19);
            this._serviceStatusControl.Name = "_serviceStatusControl";
            this._serviceStatusControl.Size = new System.Drawing.Size(717, 23);
            this._serviceStatusControl.Status = BlockchainSQL.Server.ServiceStatus.NotInstalled;
            this._serviceStatusControl.TabIndex = 0;
            // 
            // _dbConnectionBar
            // 
            this._dbConnectionBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dbConnectionBar.ArtificialKeysFile = null;
            this._dbConnectionBar.Location = new System.Drawing.Point(9, 23);
            this._dbConnectionBar.Margin = new System.Windows.Forms.Padding(0);
            this._dbConnectionBar.MinimumSize = new System.Drawing.Size(500, 40);
            this._dbConnectionBar.Name = "_dbConnectionBar";
            this._dbConnectionBar.Size = new System.Drawing.Size(635, 40);
            this._dbConnectionBar.TabIndex = 6;
            // 
            // _connectButton
            // 
            this._connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._connectButton.Location = new System.Drawing.Point(651, 38);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(75, 23);
            this._connectButton.TabIndex = 10;
            this._connectButton.Text = "&Connect";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // _databaseGroupBox
            // 
            this._databaseGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._databaseGroupBox.Controls.Add(this._dbConnectionBar);
            this._databaseGroupBox.Controls.Add(this._connectButton);
            this._databaseGroupBox.Location = new System.Drawing.Point(5, 71);
            this._databaseGroupBox.Name = "_databaseGroupBox";
            this._databaseGroupBox.Size = new System.Drawing.Size(732, 81);
            this._databaseGroupBox.TabIndex = 11;
            this._databaseGroupBox.TabStop = false;
            this._databaseGroupBox.Text = "Database";
            // 
            // _toolsGroupBox
            // 
            this._toolsGroupBox.Controls.Add(this._saveSettingsButton);
            this._toolsGroupBox.Controls.Add(this._logsButton);
            this._toolsGroupBox.Controls.Add(this._disableIndexesButton);
            this._toolsGroupBox.Controls.Add(this._enableIndexesButton);
            this._toolsGroupBox.Controls.Add(this._shrinkDatabaseButton);
            this._toolsGroupBox.Controls.Add(this._postProcessButton);
            this._toolsGroupBox.Location = new System.Drawing.Point(8, 378);
            this._toolsGroupBox.Name = "_toolsGroupBox";
            this._toolsGroupBox.Size = new System.Drawing.Size(642, 53);
            this._toolsGroupBox.TabIndex = 12;
            this._toolsGroupBox.TabStop = false;
            this._toolsGroupBox.Text = "Tools";
            // 
            // _saveSettingsButton
            // 
            this._saveSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._saveSettingsButton.Location = new System.Drawing.Point(433, 18);
            this._saveSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this._saveSettingsButton.Name = "_saveSettingsButton";
            this._saveSettingsButton.Size = new System.Drawing.Size(100, 24);
            this._saveSettingsButton.TabIndex = 15;
            this._saveSettingsButton.Text = "Save Settings";
            this._saveSettingsButton.UseVisualStyleBackColor = true;
            this._saveSettingsButton.Click += new System.EventHandler(this._saveSettingsButton_Click);
            // 
            // _logsButton
            // 
            this._logsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._logsButton.Location = new System.Drawing.Point(537, 18);
            this._logsButton.Margin = new System.Windows.Forms.Padding(2);
            this._logsButton.Name = "_logsButton";
            this._logsButton.Size = new System.Drawing.Size(100, 24);
            this._logsButton.TabIndex = 14;
            this._logsButton.Text = "Logs";
            this._logsButton.UseVisualStyleBackColor = true;
            // 
            // _disableIndexesButton
            // 
            this._disableIndexesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._disableIndexesButton.Location = new System.Drawing.Point(17, 18);
            this._disableIndexesButton.Margin = new System.Windows.Forms.Padding(2);
            this._disableIndexesButton.Name = "_disableIndexesButton";
            this._disableIndexesButton.Size = new System.Drawing.Size(100, 24);
            this._disableIndexesButton.TabIndex = 13;
            this._disableIndexesButton.Text = "Disable Indexes";
            this._disableIndexesButton.UseVisualStyleBackColor = true;
            // 
            // _enableIndexesButton
            // 
            this._enableIndexesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._enableIndexesButton.Location = new System.Drawing.Point(121, 18);
            this._enableIndexesButton.Margin = new System.Windows.Forms.Padding(2);
            this._enableIndexesButton.Name = "_enableIndexesButton";
            this._enableIndexesButton.Size = new System.Drawing.Size(100, 24);
            this._enableIndexesButton.TabIndex = 12;
            this._enableIndexesButton.Text = "Enable Indexes";
            this._enableIndexesButton.UseVisualStyleBackColor = true;
            // 
            // _shrinkDatabaseButton
            // 
            this._shrinkDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._shrinkDatabaseButton.Location = new System.Drawing.Point(225, 18);
            this._shrinkDatabaseButton.Margin = new System.Windows.Forms.Padding(2);
            this._shrinkDatabaseButton.Name = "_shrinkDatabaseButton";
            this._shrinkDatabaseButton.Size = new System.Drawing.Size(100, 24);
            this._shrinkDatabaseButton.TabIndex = 11;
            this._shrinkDatabaseButton.Text = "Shrink Database";
            this._shrinkDatabaseButton.UseVisualStyleBackColor = true;
            // 
            // _postProcessButton
            // 
            this._postProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._postProcessButton.Location = new System.Drawing.Point(329, 18);
            this._postProcessButton.Margin = new System.Windows.Forms.Padding(2);
            this._postProcessButton.Name = "_postProcessButton";
            this._postProcessButton.Size = new System.Drawing.Size(100, 24);
            this._postProcessButton.TabIndex = 10;
            this._postProcessButton.Text = "Post-Processing";
            this._postProcessButton.UseVisualStyleBackColor = true;
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = false;
            this._loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 5;
            this._loadingCircle.Location = new System.Drawing.Point(704, 398);
            this._loadingCircle.Margin = new System.Windows.Forms.Padding(2);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 12;
            this._loadingCircle.OuterCircleRadius = 11;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(27, 22);
            this._loadingCircle.SpokeThickness = 2;
            this._loadingCircle.StylePreset = Sphere10.Windows.WinForms.LoadingCircle.StylePresets.MacOSX;
            this._loadingCircle.TabIndex = 13;
            this._loadingCircle.Text = "_loadingCircle";
            this._loadingCircle.Visible = false;
            // 
            // DiagnosticForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 443);
            this.Controls.Add(this._loadingCircle);
            this.Controls.Add(this._toolsGroupBox);
            this.Controls.Add(this._databaseGroupBox);
            this.Controls.Add(this._serviceStatusGroupBox);
            this.Controls.Add(this._optionsGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiagnosticForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Diagnostic Tool";
            this._optionsGroupBox.ResumeLayout(false);
            this._serviceStatusGroupBox.ResumeLayout(false);
            this._databaseGroupBox.ResumeLayout(false);
            this._toolsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox _optionsGroupBox;
        private SettingsControl _settingsControl;
        private System.Windows.Forms.GroupBox _serviceStatusGroupBox;
        private Sphere10.Windows.WinForms.DatabaseConnectionBar _dbConnectionBar;
        private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.GroupBox _databaseGroupBox;
        private ServiceStatusControl _serviceStatusControl;
        private System.Windows.Forms.GroupBox _toolsGroupBox;
        private System.Windows.Forms.Button _logsButton;
        private System.Windows.Forms.Button _disableIndexesButton;
        private System.Windows.Forms.Button _enableIndexesButton;
        private System.Windows.Forms.Button _shrinkDatabaseButton;
        private System.Windows.Forms.Button _postProcessButton;
        private Sphere10.Windows.WinForms.LoadingCircle _loadingCircle;
        private System.Windows.Forms.Button _saveSettingsButton;
    }
}