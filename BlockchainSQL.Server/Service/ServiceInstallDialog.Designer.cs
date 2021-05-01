namespace BlockchainSQL.Server {
    partial class ServiceInstallDialog {
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
            this._installOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this._passwordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._pathSelector = new Sphere10.Framework.Windows.Forms.PathSelectorControl();
            this._installButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._generateNewDatabaseButton = new System.Windows.Forms.Button();
            this._testConnectionButon = new System.Windows.Forms.Button();
            this._databaseConnectionPanel = new Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel();
            this._loadingCircle = new Sphere10.Framework.Windows.Forms.LoadingCircle();
            this._installOptionsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _installOptionsGroupBox
            // 
            this._installOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._installOptionsGroupBox.Controls.Add(this.label2);
            this._installOptionsGroupBox.Controls.Add(this._passwordTextBox);
            this._installOptionsGroupBox.Controls.Add(this.label1);
            this._installOptionsGroupBox.Controls.Add(this._pathSelector);
            this._installOptionsGroupBox.Location = new System.Drawing.Point(12, 12);
            this._installOptionsGroupBox.Name = "_installOptionsGroupBox";
            this._installOptionsGroupBox.Size = new System.Drawing.Size(550, 86);
            this._installOptionsGroupBox.TabIndex = 1;
            this._installOptionsGroupBox.TabStop = false;
            this._installOptionsGroupBox.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // _passwordTextBox
            // 
            this._passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._passwordTextBox.Location = new System.Drawing.Point(104, 45);
            this._passwordTextBox.Name = "_passwordTextBox";
            this._passwordTextBox.Size = new System.Drawing.Size(148, 20);
            this._passwordTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Destination Folder";
            // 
            // _pathSelector
            // 
            this._pathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pathSelector.Location = new System.Drawing.Point(104, 19);
            this._pathSelector.Margin = new System.Windows.Forms.Padding(6);
            this._pathSelector.Mode = Sphere10.Framework.Windows.Forms.PathSelectionMode.Folder;
            this._pathSelector.Name = "_pathSelector";
            this._pathSelector.Path = "";
            this._pathSelector.PlaceHolderText = "Select destination folder to install BlockchainSQL service to";
            this._pathSelector.Size = new System.Drawing.Size(437, 20);
            this._pathSelector.TabIndex = 1;
            // 
            // _installButton
            // 
            this._installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._installButton.Location = new System.Drawing.Point(487, 317);
            this._installButton.Name = "_installButton";
            this._installButton.Size = new System.Drawing.Size(75, 23);
            this._installButton.TabIndex = 2;
            this._installButton.Text = "Install";
            this._installButton.UseVisualStyleBackColor = true;
            this._installButton.Click += new System.EventHandler(this._installButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._generateNewDatabaseButton);
            this.groupBox1.Controls.Add(this._testConnectionButon);
            this.groupBox1.Controls.Add(this._databaseConnectionPanel);
            this.groupBox1.Location = new System.Drawing.Point(12, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(550, 207);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // _generateNewDatabaseButton
            // 
            this._generateNewDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._generateNewDatabaseButton.Location = new System.Drawing.Point(9, 178);
            this._generateNewDatabaseButton.Name = "_generateNewDatabaseButton";
            this._generateNewDatabaseButton.Size = new System.Drawing.Size(141, 23);
            this._generateNewDatabaseButton.TabIndex = 4;
            this._generateNewDatabaseButton.Text = "Generate New Database";
            this._generateNewDatabaseButton.UseVisualStyleBackColor = true;
            this._generateNewDatabaseButton.Click += new System.EventHandler(this._generateNewDatabaseButton_Click);
            // 
            // _testConnectionButon
            // 
            this._testConnectionButon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._testConnectionButon.Location = new System.Drawing.Point(424, 178);
            this._testConnectionButon.Name = "_testConnectionButon";
            this._testConnectionButon.Size = new System.Drawing.Size(120, 23);
            this._testConnectionButon.TabIndex = 3;
            this._testConnectionButon.Text = "Test Connection";
            this._testConnectionButon.UseVisualStyleBackColor = true;
            this._testConnectionButon.Click += new System.EventHandler(this._testConnectionButon_Click);
            // 
            // _databaseConnectionPanel
            // 
            this._databaseConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._databaseConnectionPanel.Location = new System.Drawing.Point(36, 19);
            this._databaseConnectionPanel.Margin = new System.Windows.Forms.Padding(12);
            this._databaseConnectionPanel.Name = "_databaseConnectionPanel";
            this._databaseConnectionPanel.Size = new System.Drawing.Size(508, 151);
            this._databaseConnectionPanel.TabIndex = 0;
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = false;
            this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 8;
            this._loadingCircle.Location = new System.Drawing.Point(455, 317);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 10;
            this._loadingCircle.OuterCircleRadius = 10;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(23, 23);
            this._loadingCircle.SpokeThickness = 4;
            this._loadingCircle.TabIndex = 4;
            this._loadingCircle.Text = "loadingCircle1";
            this._loadingCircle.Visible = false;
            // 
            // ServiceInstallDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 352);
            this.Controls.Add(this._loadingCircle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._installButton);
            this.Controls.Add(this._installOptionsGroupBox);
            this.Name = "ServiceInstallDialog";
            this.Text = "Install BlockchainSQL Server";
            this._installOptionsGroupBox.ResumeLayout(false);
            this._installOptionsGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _installOptionsGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _passwordTextBox;
        private System.Windows.Forms.Label label1;
        private Sphere10.Framework.Windows.Forms.PathSelectorControl _pathSelector;
        private System.Windows.Forms.Button _installButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _generateNewDatabaseButton;
        private System.Windows.Forms.Button _testConnectionButon;
        private Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel _databaseConnectionPanel;
        private Sphere10.Framework.Windows.Forms.LoadingCircle _loadingCircle;

    }
}