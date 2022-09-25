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
			this._serviceStatusGroupBox = new System.Windows.Forms.GroupBox();
			this._serviceStatusControl = new Hydrogen.Windows.Forms.ServiceStatusControl();
			this._dbConnectionBar = new Hydrogen.Windows.Forms.DatabaseConnectionBar();
			this._connectButton = new System.Windows.Forms.Button();
			this._databaseGroupBox = new System.Windows.Forms.GroupBox();
			this._toolsGroupBox = new System.Windows.Forms.GroupBox();
			this._saveSettingsButton = new System.Windows.Forms.Button();
			this._logsButton = new System.Windows.Forms.Button();
			this._disableIndexesButton = new System.Windows.Forms.Button();
			this._enableIndexesButton = new System.Windows.Forms.Button();
			this._shrinkDatabaseButton = new System.Windows.Forms.Button();
			this._postProcessButton = new System.Windows.Forms.Button();
			this._loadingCircle = new Hydrogen.Windows.Forms.LoadingCircle();
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
			this._optionsGroupBox.Location = new System.Drawing.Point(7, 181);
			this._optionsGroupBox.Margin = new System.Windows.Forms.Padding(2);
			this._optionsGroupBox.Name = "_optionsGroupBox";
			this._optionsGroupBox.Padding = new System.Windows.Forms.Padding(2);
			this._optionsGroupBox.Size = new System.Drawing.Size(853, 249);
			this._optionsGroupBox.TabIndex = 5;
			this._optionsGroupBox.TabStop = false;
			this._optionsGroupBox.Text = "Options";
			// 
			// _serviceStatusGroupBox
			// 
			this._serviceStatusGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._serviceStatusGroupBox.Controls.Add(this._serviceStatusControl);
			this._serviceStatusGroupBox.Location = new System.Drawing.Point(6, 14);
			this._serviceStatusGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._serviceStatusGroupBox.Name = "_serviceStatusGroupBox";
			this._serviceStatusGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._serviceStatusGroupBox.Size = new System.Drawing.Size(854, 61);
			this._serviceStatusGroupBox.TabIndex = 9;
			this._serviceStatusGroupBox.TabStop = false;
			this._serviceStatusGroupBox.Text = "Service Status";
			// 
			// _serviceStatusControl
			// 
			this._serviceStatusControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._serviceStatusControl.EnableStateChangeEvent = false;
			this._serviceStatusControl.Location = new System.Drawing.Point(10, 22);
			this._serviceStatusControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this._serviceStatusControl.Name = "_serviceStatusControl";
			this._serviceStatusControl.Size = new System.Drawing.Size(838, 27);
			this._serviceStatusControl.Status = Hydrogen.Windows.ServiceStatus.NotInstalled;
			this._serviceStatusControl.TabIndex = 0;
			// 
			// _dbConnectionBar
			// 
			this._dbConnectionBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dbConnectionBar.ArtificialKeysFile = null;
			this._dbConnectionBar.Location = new System.Drawing.Point(10, 27);
			this._dbConnectionBar.Margin = new System.Windows.Forms.Padding(0);
			this._dbConnectionBar.MinimumSize = new System.Drawing.Size(583, 46);
			this._dbConnectionBar.Name = "_dbConnectionBar";
			this._dbConnectionBar.Size = new System.Drawing.Size(741, 46);
			this._dbConnectionBar.TabIndex = 6;
			// 
			// _connectButton
			// 
			this._connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._connectButton.Location = new System.Drawing.Point(760, 44);
			this._connectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._connectButton.Name = "_connectButton";
			this._connectButton.Size = new System.Drawing.Size(88, 27);
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
			this._databaseGroupBox.Location = new System.Drawing.Point(6, 82);
			this._databaseGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._databaseGroupBox.Name = "_databaseGroupBox";
			this._databaseGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._databaseGroupBox.Size = new System.Drawing.Size(854, 93);
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
			this._toolsGroupBox.Location = new System.Drawing.Point(9, 436);
			this._toolsGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._toolsGroupBox.Name = "_toolsGroupBox";
			this._toolsGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._toolsGroupBox.Size = new System.Drawing.Size(748, 61);
			this._toolsGroupBox.TabIndex = 12;
			this._toolsGroupBox.TabStop = false;
			this._toolsGroupBox.Text = "Tools";
			// 
			// _saveSettingsButton
			// 
			this._saveSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._saveSettingsButton.Location = new System.Drawing.Point(504, 21);
			this._saveSettingsButton.Margin = new System.Windows.Forms.Padding(2);
			this._saveSettingsButton.Name = "_saveSettingsButton";
			this._saveSettingsButton.Size = new System.Drawing.Size(117, 28);
			this._saveSettingsButton.TabIndex = 15;
			this._saveSettingsButton.Text = "Save Settings";
			this._saveSettingsButton.UseVisualStyleBackColor = true;
			this._saveSettingsButton.Click += new System.EventHandler(this._saveSettingsButton_Click);
			// 
			// _logsButton
			// 
			this._logsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._logsButton.Location = new System.Drawing.Point(625, 21);
			this._logsButton.Margin = new System.Windows.Forms.Padding(2);
			this._logsButton.Name = "_logsButton";
			this._logsButton.Size = new System.Drawing.Size(117, 28);
			this._logsButton.TabIndex = 14;
			this._logsButton.Text = "Logs";
			this._logsButton.UseVisualStyleBackColor = true;
			this._logsButton.Click += new System.EventHandler(this._logsButton_Click);
			// 
			// _disableIndexesButton
			// 
			this._disableIndexesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._disableIndexesButton.Location = new System.Drawing.Point(19, 21);
			this._disableIndexesButton.Margin = new System.Windows.Forms.Padding(2);
			this._disableIndexesButton.Name = "_disableIndexesButton";
			this._disableIndexesButton.Size = new System.Drawing.Size(117, 28);
			this._disableIndexesButton.TabIndex = 13;
			this._disableIndexesButton.Text = "Disable Indexes";
			this._disableIndexesButton.UseVisualStyleBackColor = true;
			this._disableIndexesButton.Click += new System.EventHandler(this._disableIndexesButton_Click);
			// 
			// _enableIndexesButton
			// 
			this._enableIndexesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._enableIndexesButton.Location = new System.Drawing.Point(140, 21);
			this._enableIndexesButton.Margin = new System.Windows.Forms.Padding(2);
			this._enableIndexesButton.Name = "_enableIndexesButton";
			this._enableIndexesButton.Size = new System.Drawing.Size(117, 28);
			this._enableIndexesButton.TabIndex = 12;
			this._enableIndexesButton.Text = "Enable Indexes";
			this._enableIndexesButton.UseVisualStyleBackColor = true;
			this._enableIndexesButton.Click += new System.EventHandler(this._enableIndexesButton_Click);
			// 
			// _shrinkDatabaseButton
			// 
			this._shrinkDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._shrinkDatabaseButton.Location = new System.Drawing.Point(261, 21);
			this._shrinkDatabaseButton.Margin = new System.Windows.Forms.Padding(2);
			this._shrinkDatabaseButton.Name = "_shrinkDatabaseButton";
			this._shrinkDatabaseButton.Size = new System.Drawing.Size(117, 28);
			this._shrinkDatabaseButton.TabIndex = 11;
			this._shrinkDatabaseButton.Text = "Shrink Database";
			this._shrinkDatabaseButton.UseVisualStyleBackColor = true;
			this._shrinkDatabaseButton.Click += new System.EventHandler(this._shrinkDatabaseButton_Click);
			// 
			// _postProcessButton
			// 
			this._postProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._postProcessButton.Location = new System.Drawing.Point(383, 21);
			this._postProcessButton.Margin = new System.Windows.Forms.Padding(2);
			this._postProcessButton.Name = "_postProcessButton";
			this._postProcessButton.Size = new System.Drawing.Size(117, 28);
			this._postProcessButton.TabIndex = 10;
			this._postProcessButton.Text = "Post-Processing";
			this._postProcessButton.UseVisualStyleBackColor = true;
			this._postProcessButton.Click += new System.EventHandler(this._postProcessButton_Click);
			// 
			// _loadingCircle
			// 
			this._loadingCircle.Active = false;
			this._loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
			this._loadingCircle.Color = System.Drawing.Color.DarkGray;
			this._loadingCircle.InnerCircleRadius = 5;
			this._loadingCircle.Location = new System.Drawing.Point(821, 459);
			this._loadingCircle.Margin = new System.Windows.Forms.Padding(2);
			this._loadingCircle.Name = "_loadingCircle";
			this._loadingCircle.NumberSpoke = 12;
			this._loadingCircle.OuterCircleRadius = 11;
			this._loadingCircle.RotationSpeed = 100;
			this._loadingCircle.Size = new System.Drawing.Size(31, 25);
			this._loadingCircle.SpokeThickness = 2;
			this._loadingCircle.StylePreset = Hydrogen.Windows.Forms.LoadingCircle.StylePresets.MacOSX;
			this._loadingCircle.TabIndex = 13;
			this._loadingCircle.Text = "_loadingCircle";
			this._loadingCircle.Visible = false;
			// 
			// DiagnosticForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(867, 511);
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
			this._serviceStatusGroupBox.ResumeLayout(false);
			this._databaseGroupBox.ResumeLayout(false);
			this._toolsGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox _optionsGroupBox;
        private System.Windows.Forms.GroupBox _serviceStatusGroupBox;
        private Hydrogen.Windows.Forms.DatabaseConnectionBar _dbConnectionBar;
        private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.GroupBox _databaseGroupBox;
        private Hydrogen.Windows.Forms.ServiceStatusControl _serviceStatusControl;
        private System.Windows.Forms.GroupBox _toolsGroupBox;
        private System.Windows.Forms.Button _logsButton;
        private System.Windows.Forms.Button _disableIndexesButton;
        private System.Windows.Forms.Button _enableIndexesButton;
        private System.Windows.Forms.Button _shrinkDatabaseButton;
        private System.Windows.Forms.Button _postProcessButton;
        private Hydrogen.Windows.Forms.LoadingCircle _loadingCircle;
        private System.Windows.Forms.Button _saveSettingsButton;
    }
}