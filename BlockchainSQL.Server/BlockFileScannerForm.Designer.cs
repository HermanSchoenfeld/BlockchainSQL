using Sphere10.Framework.Windows.Forms;
using System.Windows.Forms;

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
			this.label1 = new System.Windows.Forms.Label();
			this._BLKDataFolderValidator = new Sphere10.Framework.Windows.Forms.ValidationIndicator();
			this._blkDataPathControl = new Sphere10.Framework.Windows.Forms.PathSelectorControl();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._disableIndexCheckBox = new System.Windows.Forms.CheckBox();
			this._testDatabaseButton = new System.Windows.Forms.Button();
			this._dbConnectionBar = new Sphere10.Framework.Windows.Forms.DatabaseConnectionBar();
			this._startButton = new System.Windows.Forms.Button();
			this._loadingCircle = new Sphere10.Framework.Windows.Forms.LoadingCircle();
			this._progressBar = new Sphere10.Framework.Windows.Forms.ProgressBarEx();
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
			this.groupBox1.Location = new System.Drawing.Point(14, 238);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(966, 234);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Log";
			// 
			// _logBox
			// 
			this._logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._logBox.Location = new System.Drawing.Point(7, 22);
			this._logBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._logBox.Multiline = true;
			this._logBox.Name = "_logBox";
			this._logBox.ReadOnly = true;
			this._logBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this._logBox.Size = new System.Drawing.Size(951, 201);
			this._logBox.TabIndex = 3;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this._BLKDataFolderValidator);
			this.groupBox2.Controls.Add(this._blkDataPathControl);
			this.groupBox2.Location = new System.Drawing.Point(14, 14);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Size = new System.Drawing.Size(966, 82);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Blockchain Source";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(7, 51);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(563, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Please make sure there are no running instances of Bitcoin Core client running wh" +
    "ilst the scanning is being performed. ";
			// 
			// _BLKDataFolderValidator
			// 
			this._BLKDataFolderValidator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._BLKDataFolderValidator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_BLKDataFolderValidator.BackgroundImage")));
			this._BLKDataFolderValidator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this._BLKDataFolderValidator.Location = new System.Drawing.Point(936, 22);
			this._BLKDataFolderValidator.Margin = new System.Windows.Forms.Padding(0);
			this._BLKDataFolderValidator.Name = "_BLKDataFolderValidator";
			this._BLKDataFolderValidator.RunValidatorWhenEnabled = false;
			this._BLKDataFolderValidator.Size = new System.Drawing.Size(23, 23);
			this._BLKDataFolderValidator.State = Sphere10.Framework.Windows.Forms.ValidationState.Error;
			this._BLKDataFolderValidator.TabIndex = 7;
			this._BLKDataFolderValidator.PerformValidation += new Sphere10.Framework.EventHandlerEx<Sphere10.Framework.Windows.Forms.ValidationIndicator, Sphere10.Framework.Windows.Forms.ValidationIndicatorEvent>(this._BLKDataFolderValidator_PerformValidation);
			// 
			// _blkDataPathControl
			// 
			this._blkDataPathControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._blkDataPathControl.Location = new System.Drawing.Point(9, 22);
			this._blkDataPathControl.Margin = new System.Windows.Forms.Padding(0);
			this._blkDataPathControl.Mode = Sphere10.Framework.Windows.Forms.PathSelectionMode.Folder;
			this._blkDataPathControl.Name = "_blkDataPathControl";
			this._blkDataPathControl.Path = "";
			this._blkDataPathControl.PlaceHolderText = "Select folder containing BLK data files";
			this._blkDataPathControl.Size = new System.Drawing.Size(923, 23);
			this._blkDataPathControl.TabIndex = 2;
			this._blkDataPathControl.PathChanged += new Sphere10.Framework.EventHandlerEx(this._blkDataPathControl_PathChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this._disableIndexCheckBox);
			this.groupBox3.Controls.Add(this._testDatabaseButton);
			this.groupBox3.Controls.Add(this._dbConnectionBar);
			this.groupBox3.Location = new System.Drawing.Point(14, 103);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Size = new System.Drawing.Size(966, 112);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Database Output";
			// 
			// _disableIndexCheckBox
			// 
			this._disableIndexCheckBox.AutoSize = true;
			this._disableIndexCheckBox.Checked = true;
			this._disableIndexCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._disableIndexCheckBox.Location = new System.Drawing.Point(637, 83);
			this._disableIndexCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._disableIndexCheckBox.Name = "_disableIndexCheckBox";
			this._disableIndexCheckBox.Size = new System.Drawing.Size(179, 19);
			this._disableIndexCheckBox.TabIndex = 8;
			this._disableIndexCheckBox.Text = "Disable indexes (temporarily)";
			this._disableIndexCheckBox.UseVisualStyleBackColor = true;
			// 
			// _testDatabaseButton
			// 
			this._testDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._testDatabaseButton.Location = new System.Drawing.Point(830, 78);
			this._testDatabaseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._testDatabaseButton.Name = "_testDatabaseButton";
			this._testDatabaseButton.Size = new System.Drawing.Size(130, 27);
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
			this._dbConnectionBar.Location = new System.Drawing.Point(9, 24);
			this._dbConnectionBar.Margin = new System.Windows.Forms.Padding(0);
			this._dbConnectionBar.MinimumSize = new System.Drawing.Size(583, 46);
			this._dbConnectionBar.Name = "_dbConnectionBar";
			this._dbConnectionBar.Size = new System.Drawing.Size(950, 47);
			this._dbConnectionBar.TabIndex = 6;
			// 
			// _startButton
			// 
			this._startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._startButton.Location = new System.Drawing.Point(850, 479);
			this._startButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._startButton.Name = "_startButton";
			this._startButton.Size = new System.Drawing.Size(130, 27);
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
			this._loadingCircle.Location = new System.Drawing.Point(803, 479);
			this._loadingCircle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._loadingCircle.Name = "_loadingCircle";
			this._loadingCircle.NumberSpoke = 10;
			this._loadingCircle.OuterCircleRadius = 10;
			this._loadingCircle.RotationSpeed = 100;
			this._loadingCircle.Size = new System.Drawing.Size(41, 25);
			this._loadingCircle.SpokeThickness = 4;
			this._loadingCircle.TabIndex = 7;
			this._loadingCircle.Text = "_loadingCircle";
			this._loadingCircle.Visible = false;
			// 
			// _progressBar
			// 
			this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._progressBar.CustomText = null;
			this._progressBar.DisplayStyle = Sphere10.Framework.Windows.Forms.ProgressBarDisplayText.Percentage;
			this._progressBar.Location = new System.Drawing.Point(14, 479);
			this._progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._progressBar.Name = "_progressBar";
			this._progressBar.Size = new System.Drawing.Size(782, 27);
			this._progressBar.TabIndex = 8;
			this._progressBar.Visible = false;
			// 
			// BlockFileScannerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(994, 519);
			this.Controls.Add(this._progressBar);
			this.Controls.Add(this._loadingCircle);
			this.Controls.Add(this._startButton);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "BlockFileScannerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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

        private GroupBox groupBox1;
        private TextBox _logBox;
        private GroupBox groupBox2;
        private PathSelectorControl _blkDataPathControl;
        private GroupBox groupBox3;
        private DatabaseConnectionBar _dbConnectionBar;
        private Button _startButton;
        private Button _testDatabaseButton;
        private ValidationIndicator _BLKDataFolderValidator;
        private LoadingCircle _loadingCircle;
        private ProgressBarEx _progressBar;
        private CheckBox _disableIndexCheckBox;
        private Label label1;
    }
}