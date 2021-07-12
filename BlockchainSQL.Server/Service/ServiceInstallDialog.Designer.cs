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
            this._webUIPort = new System.Windows.Forms.NumericUpDown();
            this.webUiPortLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._installWebUICheckbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._pathSelector = new Sphere10.Framework.Windows.Forms.PathSelectorControl();
            this._installButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._generateNewDatabaseButton = new System.Windows.Forms.Button();
            this._testConnectionButon = new System.Windows.Forms.Button();
            this._databaseConnectionPanel = new Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel();
            this._loadingCircle = new Sphere10.Framework.Windows.Forms.LoadingCircle();
            this._installOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._webUIPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _installOptionsGroupBox
            // 
            this._installOptionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._installOptionsGroupBox.Controls.Add(this._webUIPort);
            this._installOptionsGroupBox.Controls.Add(this.webUiPortLabel);
            this._installOptionsGroupBox.Controls.Add(this.label3);
            this._installOptionsGroupBox.Controls.Add(this._installWebUICheckbox);
            this._installOptionsGroupBox.Controls.Add(this.label1);
            this._installOptionsGroupBox.Controls.Add(this._pathSelector);
            this._installOptionsGroupBox.Location = new System.Drawing.Point(14, 14);
            this._installOptionsGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._installOptionsGroupBox.Name = "_installOptionsGroupBox";
            this._installOptionsGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._installOptionsGroupBox.Size = new System.Drawing.Size(642, 114);
            this._installOptionsGroupBox.TabIndex = 1;
            this._installOptionsGroupBox.TabStop = false;
            this._installOptionsGroupBox.Text = "Options";
            // 
            // _webUIPort
            // 
            this._webUIPort.Location = new System.Drawing.Point(121, 83);
            this._webUIPort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._webUIPort.Name = "_webUIPort";
            this._webUIPort.Size = new System.Drawing.Size(52, 23);
            this._webUIPort.TabIndex = 9;
            this._webUIPort.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // webUiPortLabel
            // 
            this.webUiPortLabel.AutoSize = true;
            this.webUiPortLabel.Location = new System.Drawing.Point(40, 85);
            this.webUiPortLabel.Name = "webUiPortLabel";
            this.webUiPortLabel.Size = new System.Drawing.Size(70, 15);
            this.webUiPortLabel.TabIndex = 8;
            this.webUiPortLabel.Text = "Web UI Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Install Web UI";
            // 
            // _installWebUICheckbox
            // 
            this._installWebUICheckbox.AutoSize = true;
            this._installWebUICheckbox.Location = new System.Drawing.Point(121, 56);
            this._installWebUICheckbox.Name = "_installWebUICheckbox";
            this._installWebUICheckbox.Size = new System.Drawing.Size(15, 14);
            this._installWebUICheckbox.TabIndex = 5;
            this._installWebUICheckbox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Destination Folder";
            // 
            // _pathSelector
            // 
            this._pathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pathSelector.Location = new System.Drawing.Point(121, 22);
            this._pathSelector.Margin = new System.Windows.Forms.Padding(7);
            this._pathSelector.Mode = Sphere10.Framework.Windows.Forms.PathSelectionMode.Folder;
            this._pathSelector.Name = "_pathSelector";
            this._pathSelector.Path = "";
            this._pathSelector.PlaceHolderText = "Select destination folder to install BlockchainSQL service to";
            this._pathSelector.Size = new System.Drawing.Size(510, 23);
            this._pathSelector.TabIndex = 1;
            // 
            // _installButton
            // 
            this._installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._installButton.Location = new System.Drawing.Point(569, 372);
            this._installButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._installButton.Name = "_installButton";
            this._installButton.Size = new System.Drawing.Size(88, 27);
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
            this.groupBox1.Location = new System.Drawing.Point(13, 129);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(642, 239);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // _generateNewDatabaseButton
            // 
            this._generateNewDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._generateNewDatabaseButton.Location = new System.Drawing.Point(10, 205);
            this._generateNewDatabaseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._generateNewDatabaseButton.Name = "_generateNewDatabaseButton";
            this._generateNewDatabaseButton.Size = new System.Drawing.Size(164, 27);
            this._generateNewDatabaseButton.TabIndex = 4;
            this._generateNewDatabaseButton.Text = "Generate New Database";
            this._generateNewDatabaseButton.UseVisualStyleBackColor = true;
            this._generateNewDatabaseButton.Click += new System.EventHandler(this._generateNewDatabaseButton_Click);
            // 
            // _testConnectionButon
            // 
            this._testConnectionButon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._testConnectionButon.Location = new System.Drawing.Point(495, 205);
            this._testConnectionButon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._testConnectionButon.Name = "_testConnectionButon";
            this._testConnectionButon.Size = new System.Drawing.Size(140, 27);
            this._testConnectionButon.TabIndex = 3;
            this._testConnectionButon.Text = "Test Connection";
            this._testConnectionButon.UseVisualStyleBackColor = true;
            this._testConnectionButon.Click += new System.EventHandler(this._testConnectionButon_Click);
            // 
            // _databaseConnectionPanel
            // 
            this._databaseConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._databaseConnectionPanel.Location = new System.Drawing.Point(42, 22);
            this._databaseConnectionPanel.Margin = new System.Windows.Forms.Padding(14);
            this._databaseConnectionPanel.Name = "_databaseConnectionPanel";
            this._databaseConnectionPanel.Size = new System.Drawing.Size(593, 174);
            this._databaseConnectionPanel.TabIndex = 0;
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = false;
            this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 8;
            this._loadingCircle.Location = new System.Drawing.Point(534, 372);
            this._loadingCircle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 10;
            this._loadingCircle.OuterCircleRadius = 10;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(27, 27);
            this._loadingCircle.SpokeThickness = 4;
            this._loadingCircle.TabIndex = 4;
            this._loadingCircle.Text = "loadingCircle1";
            this._loadingCircle.Visible = false;
            // 
            // ServiceInstallDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 406);
            this.Controls.Add(this._loadingCircle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._installButton);
            this.Controls.Add(this._installOptionsGroupBox);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ServiceInstallDialog";
            this.Text = "Install BlockchainSQL Server";
            this._installOptionsGroupBox.ResumeLayout(false);
            this._installOptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._webUIPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _installOptionsGroupBox;
        private System.Windows.Forms.Label label1;
        private Sphere10.Framework.Windows.Forms.PathSelectorControl _pathSelector;
        private System.Windows.Forms.Button _installButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _generateNewDatabaseButton;
        private System.Windows.Forms.Button _testConnectionButon;
        private Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel _databaseConnectionPanel;
        private Sphere10.Framework.Windows.Forms.LoadingCircle _loadingCircle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox _installWebUICheckbox;
		private System.Windows.Forms.NumericUpDown _webUIPort;
		private System.Windows.Forms.Label webUiPortLabel;
	}
}