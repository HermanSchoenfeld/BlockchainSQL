using BlockchainSQL.Server;

namespace BlockchainSQL.Server {
    partial class GenerateDatabaseForm {
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
            this._testButton = new System.Windows.Forms.Button();
            this._optionsGroupBox = new System.Windows.Forms.GroupBox();
            this._settingsControl = new BlockchainSQL.Server.SettingsControl();
            this._databaseGroupBox = new System.Windows.Forms.GroupBox();
            this._databaseConnectionPanel = new Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel();
            this._loadingCircle = new Sphere10.Framework.Windows.Forms.LoadingCircle();
            this._generateDatabaseButton = new System.Windows.Forms.Button();
            this._optionsGroupBox.SuspendLayout();
            this._databaseGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _testButton
            // 
            this._testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._testButton.Location = new System.Drawing.Point(548, 188);
            this._testButton.Margin = new System.Windows.Forms.Padding(2);
            this._testButton.Name = "_testButton";
            this._testButton.Size = new System.Drawing.Size(98, 23);
            this._testButton.TabIndex = 3;
            this._testButton.Text = "Test Connection";
            this._testButton.UseVisualStyleBackColor = true;
            this._testButton.Click += new System.EventHandler(this._testButton_Click);
            // 
            // _optionsGroupBox
            // 
            this._optionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsGroupBox.Controls.Add(this._settingsControl);
            this._optionsGroupBox.Location = new System.Drawing.Point(6, 234);
            this._optionsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this._optionsGroupBox.Name = "_optionsGroupBox";
            this._optionsGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this._optionsGroupBox.Size = new System.Drawing.Size(656, 215);
            this._optionsGroupBox.TabIndex = 5;
            this._optionsGroupBox.TabStop = false;
            this._optionsGroupBox.Text = "Options";
            // 
            // _settingsControl
            // 
            this._settingsControl.Location = new System.Drawing.Point(2, 15);
            this._settingsControl.Name = "_settingsControl";
            this._settingsControl.Padding = new System.Windows.Forms.Padding(5);
            this._settingsControl.Size = new System.Drawing.Size(652, 196);
            this._settingsControl.TabIndex = 0;
            // 
            // _databaseGroupBox
            // 
            this._databaseGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._databaseGroupBox.Controls.Add(this._databaseConnectionPanel);
            this._databaseGroupBox.Controls.Add(this._testButton);
            this._databaseGroupBox.Location = new System.Drawing.Point(6, 6);
            this._databaseGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this._databaseGroupBox.Name = "_databaseGroupBox";
            this._databaseGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this._databaseGroupBox.Size = new System.Drawing.Size(656, 224);
            this._databaseGroupBox.TabIndex = 6;
            this._databaseGroupBox.TabStop = false;
            this._databaseGroupBox.Text = "Database Server";
            // 
            // _databaseConnectionPanel
            // 
            this._databaseConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._databaseConnectionPanel.Location = new System.Drawing.Point(8, 21);
            this._databaseConnectionPanel.Margin = new System.Windows.Forms.Padding(6);
            this._databaseConnectionPanel.Name = "_databaseConnectionPanel";
            this._databaseConnectionPanel.Size = new System.Drawing.Size(638, 159);
            this._databaseConnectionPanel.TabIndex = 0;
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = false;
            this._loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 5;
            this._loadingCircle.Location = new System.Drawing.Point(533, 453);
            this._loadingCircle.Margin = new System.Windows.Forms.Padding(2);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 12;
            this._loadingCircle.OuterCircleRadius = 11;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(27, 22);
            this._loadingCircle.SpokeThickness = 2;
            this._loadingCircle.StylePreset = Sphere10.Framework.Windows.Forms.LoadingCircle.StylePresets.MacOSX;
            this._loadingCircle.TabIndex = 1;
            this._loadingCircle.Text = "_loadingCircle";
            this._loadingCircle.Visible = false;
            // 
            // _generateDatabaseButton
            // 
            this._generateDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._generateDatabaseButton.Location = new System.Drawing.Point(564, 454);
            this._generateDatabaseButton.Margin = new System.Windows.Forms.Padding(2);
            this._generateDatabaseButton.Name = "_generateDatabaseButton";
            this._generateDatabaseButton.Size = new System.Drawing.Size(98, 23);
            this._generateDatabaseButton.TabIndex = 7;
            this._generateDatabaseButton.Text = "Generate Database";
            this._generateDatabaseButton.UseVisualStyleBackColor = true;
            this._generateDatabaseButton.Click += new System.EventHandler(this._generateDatabaseButton_Click);
            // 
            // GenerateDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 488);
            this.Controls.Add(this._loadingCircle);
            this.Controls.Add(this._generateDatabaseButton);
            this.Controls.Add(this._databaseGroupBox);
            this.Controls.Add(this._optionsGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateDatabaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Generator";
            this._optionsGroupBox.ResumeLayout(false);
            this._databaseGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel _databaseConnectionPanel;
        private Sphere10.Framework.Windows.Forms.LoadingCircle _loadingCircle;
        private System.Windows.Forms.Button _testButton;
        private System.Windows.Forms.GroupBox _optionsGroupBox;
        private System.Windows.Forms.GroupBox _databaseGroupBox;
        private System.Windows.Forms.Button _generateDatabaseButton;
        private SettingsControl _settingsControl;
    }
}