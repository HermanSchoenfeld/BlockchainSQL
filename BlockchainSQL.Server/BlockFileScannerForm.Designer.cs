namespace BlockchainSQL.Server {
    partial class BlockFileScannerForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlockFileScannerForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._logBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._BLKDataFolderValidator = new Sphere10.Windows.WinForms.ValidationIndicator();
            this._blkDataPathControl = new Sphere10.Windows.WinForms.PathSelectorControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._disableIndexCheckBox = new System.Windows.Forms.CheckBox();
            this._testDatabaseButton = new System.Windows.Forms.Button();
            this._dbConnectionBar = new Sphere10.Windows.WinForms.DatabaseConnectionBar();
            this._startButton = new System.Windows.Forms.Button();
            this._loadingCircle = new Sphere10.Windows.WinForms.LoadingCircle();
            this._progressBar = new Sphere10.Windows.WinForms.ProgressBarEx();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._logBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 206);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 203);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // _logBox
            // 
            this._logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._logBox.Location = new System.Drawing.Point(6, 19);
            this._logBox.Multiline = true;
            this._logBox.Name = "_logBox";
            this._logBox.ReadOnly = true;
            this._logBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._logBox.Size = new System.Drawing.Size(816, 175);
            this._logBox.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this._BLKDataFolderValidator);
            this.groupBox2.Controls.Add(this._blkDataPathControl);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(828, 71);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Blockchain Source";
            // 
            // _BLKDataFolderValidator
            // 
            this._BLKDataFolderValidator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._BLKDataFolderValidator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_BLKDataFolderValidator.BackgroundImage")));
            this._BLKDataFolderValidator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._BLKDataFolderValidator.Location = new System.Drawing.Point(802, 19);
            this._BLKDataFolderValidator.Margin = new System.Windows.Forms.Padding(0);
            this._BLKDataFolderValidator.Name = "_BLKDataFolderValidator";
            this._BLKDataFolderValidator.RunValidatorWhenEnabled = false;
            this._BLKDataFolderValidator.Size = new System.Drawing.Size(20, 20);
            this._BLKDataFolderValidator.State = Sphere10.Windows.WinForms.ValidationState.Error;
            this._BLKDataFolderValidator.TabIndex = 7;
            this._BLKDataFolderValidator.PerformValidation += new Sphere10.Framework.EventHandlerEx<Sphere10.Windows.WinForms.ValidationIndicator, Sphere10.Windows.WinForms.ValidationIndicatorEvent>(this._BLKDataFolderValidator_PerformValidation);
            // 
            // _blkDataPathControl
            // 
            this._blkDataPathControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._blkDataPathControl.Location = new System.Drawing.Point(8, 19);
            this._blkDataPathControl.Margin = new System.Windows.Forms.Padding(0);
            this._blkDataPathControl.Mode = Sphere10.Windows.WinForms.PathSelectionMode.Folder;
            this._blkDataPathControl.Name = "_blkDataPathControl";
            this._blkDataPathControl.Path = "";
            this._blkDataPathControl.PlaceHolderText = "Select folder containing BLK data files";
            this._blkDataPathControl.Size = new System.Drawing.Size(791, 20);
            this._blkDataPathControl.TabIndex = 2;
            this._blkDataPathControl.PathChanged += new System.EventHandler(this._blkDataPathControl_PathChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this._disableIndexCheckBox);
            this.groupBox3.Controls.Add(this._testDatabaseButton);
            this.groupBox3.Controls.Add(this._dbConnectionBar);
            this.groupBox3.Location = new System.Drawing.Point(12, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(828, 97);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database Output";
            // 
            // _disableIndexCheckBox
            // 
            this._disableIndexCheckBox.AutoSize = true;
            this._disableIndexCheckBox.Checked = true;
            this._disableIndexCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._disableIndexCheckBox.Location = new System.Drawing.Point(546, 72);
            this._disableIndexCheckBox.Name = "_disableIndexCheckBox";
            this._disableIndexCheckBox.Size = new System.Drawing.Size(159, 17);
            this._disableIndexCheckBox.TabIndex = 8;
            this._disableIndexCheckBox.Text = "Disable indexes (temporarily)";
            this._disableIndexCheckBox.UseVisualStyleBackColor = true;
            // 
            // _testDatabaseButton
            // 
            this._testDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._testDatabaseButton.Location = new System.Drawing.Point(711, 68);
            this._testDatabaseButton.Name = "_testDatabaseButton";
            this._testDatabaseButton.Size = new System.Drawing.Size(111, 23);
            this._testDatabaseButton.TabIndex = 7;
            this._testDatabaseButton.Text = "Test Connection";
            this._testDatabaseButton.UseVisualStyleBackColor = true;
            this._testDatabaseButton.Click += new System.EventHandler(this._testDatabaseButton_Click);
            // 
            // _dbConnectionBar
            // 
            this._dbConnectionBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dbConnectionBar.ArtificialKeysFile = null;
            this._dbConnectionBar.Location = new System.Drawing.Point(8, 21);
            this._dbConnectionBar.Margin = new System.Windows.Forms.Padding(0);
            this._dbConnectionBar.MinimumSize = new System.Drawing.Size(500, 40);
            this._dbConnectionBar.Name = "_dbConnectionBar";
            this._dbConnectionBar.SelectedDBMSType = Sphere10.Framework.Data.DBMSType.SQLServer;
            this._dbConnectionBar.Size = new System.Drawing.Size(814, 41);
            this._dbConnectionBar.TabIndex = 6;
            // 
            // _startButton
            // 
            this._startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._startButton.Location = new System.Drawing.Point(729, 415);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(111, 23);
            this._startButton.TabIndex = 4;
            this._startButton.Text = "Start";
            this._startButton.UseVisualStyleBackColor = true;
            this._startButton.Click += new System.EventHandler(this._startButton_Click);
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = false;
            this._loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 8;
            this._loadingCircle.Location = new System.Drawing.Point(688, 415);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 10;
            this._loadingCircle.OuterCircleRadius = 10;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(35, 22);
            this._loadingCircle.SpokeThickness = 4;
            this._loadingCircle.TabIndex = 7;
            this._loadingCircle.Text = "_loadingCircle";
            this._loadingCircle.Visible = false;
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.CustomText = null;
            this._progressBar.DisplayStyle = Sphere10.Windows.WinForms.ProgressBarDisplayText.Percentage;
            this._progressBar.Location = new System.Drawing.Point(12, 415);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(670, 23);
            this._progressBar.TabIndex = 8;
            this._progressBar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(563, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Please make sure there are no running instances of Bitcoin Core client running wh" +
    "ilst the scanning is being performed. ";
            // 
            // BlockFileScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 450);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._loadingCircle);
            this.Controls.Add(this._startButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BlockFileScannerForm";
            this.Text = "Block File Scanner";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _logBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private Sphere10.Windows.WinForms.PathSelectorControl _blkDataPathControl;
        private System.Windows.Forms.GroupBox groupBox3;
        private Sphere10.Windows.WinForms.DatabaseConnectionBar _dbConnectionBar;
        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.Button _testDatabaseButton;
        private Sphere10.Windows.WinForms.ValidationIndicator _BLKDataFolderValidator;
        private Sphere10.Windows.WinForms.LoadingCircle _loadingCircle;
        private Sphere10.Windows.WinForms.ProgressBarEx _progressBar;
        private System.Windows.Forms.CheckBox _disableIndexCheckBox;
        private System.Windows.Forms.Label label1;
    }
}