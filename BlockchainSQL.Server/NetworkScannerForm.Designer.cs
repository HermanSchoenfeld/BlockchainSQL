namespace BlockchainSQL.Server {
    partial class NetworkScannerForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkScannerForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._logBox = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this._nodePortBox = new Sphere10.Framework.Windows.Forms.IntBox();
			this._nodeIPTextBox = new Sphere10.Framework.Windows.Forms.TextBoxEx();
			this._testNodeButton = new System.Windows.Forms.Button();
			this._BLKDataFolderValidator = new Sphere10.Framework.Windows.Forms.ValidationIndicator();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
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
			this.groupBox1.Location = new System.Drawing.Point(14, 215);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(966, 228);
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
			this._logBox.Size = new System.Drawing.Size(951, 196);
			this._logBox.TabIndex = 3;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this._nodePortBox);
			this.groupBox2.Controls.Add(this._nodeIPTextBox);
			this.groupBox2.Controls.Add(this._testNodeButton);
			this.groupBox2.Controls.Add(this._BLKDataFolderValidator);
			this.groupBox2.Location = new System.Drawing.Point(14, 14);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Size = new System.Drawing.Size(966, 75);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Node";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(674, 23);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 15);
			this.label3.TabIndex = 14;
			this.label3.Text = "Port";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 23);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 15);
			this.label2.TabIndex = 13;
			this.label2.Text = "IP Address";
			// 
			// _nodePortBox
			// 
			this._nodePortBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._nodePortBox.Location = new System.Drawing.Point(678, 42);
			this._nodePortBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._nodePortBox.Name = "_nodePortBox";
			this._nodePortBox.Size = new System.Drawing.Size(116, 23);
			this._nodePortBox.TabIndex = 12;
			// 
			// _nodeIPTextBox
			// 
			this._nodeIPTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._nodeIPTextBox.Location = new System.Drawing.Point(7, 42);
			this._nodeIPTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._nodeIPTextBox.Name = "_nodeIPTextBox";
			this._nodeIPTextBox.Size = new System.Drawing.Size(663, 23);
			this._nodeIPTextBox.TabIndex = 10;
			// 
			// _testNodeButton
			// 
			this._testNodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._testNodeButton.Location = new System.Drawing.Point(802, 42);
			this._testNodeButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._testNodeButton.Name = "_testNodeButton";
			this._testNodeButton.Size = new System.Drawing.Size(130, 27);
			this._testNodeButton.TabIndex = 9;
			this._testNodeButton.Text = "Test Connection";
			this._testNodeButton.UseVisualStyleBackColor = true;
			this._testNodeButton.Click += new System.EventHandler(this._testNodeButton_Click);
			// 
			// _BLKDataFolderValidator
			// 
			this._BLKDataFolderValidator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._BLKDataFolderValidator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_BLKDataFolderValidator.BackgroundImage")));
			this._BLKDataFolderValidator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this._BLKDataFolderValidator.Location = new System.Drawing.Point(934, 44);
			this._BLKDataFolderValidator.Margin = new System.Windows.Forms.Padding(0);
			this._BLKDataFolderValidator.Name = "_BLKDataFolderValidator";
			this._BLKDataFolderValidator.RunValidatorWhenEnabled = false;
			this._BLKDataFolderValidator.Size = new System.Drawing.Size(23, 23);
			this._BLKDataFolderValidator.State = Sphere10.Framework.Windows.Forms.ValidationState.Error;
			this._BLKDataFolderValidator.TabIndex = 7;
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this._testDatabaseButton);
			this.groupBox3.Controls.Add(this._dbConnectionBar);
			this.groupBox3.Location = new System.Drawing.Point(14, 96);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Size = new System.Drawing.Size(966, 112);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Database Output";
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
			this._startButton.Location = new System.Drawing.Point(850, 450);
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
			this._loadingCircle.Location = new System.Drawing.Point(803, 450);
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
			this._progressBar.Location = new System.Drawing.Point(21, 450);
			this._progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._progressBar.Name = "_progressBar";
			this._progressBar.Size = new System.Drawing.Size(782, 27);
			this._progressBar.TabIndex = 9;
			this._progressBar.Visible = false;
			// 
			// NetworkScannerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(994, 490);
			this.Controls.Add(this._progressBar);
			this.Controls.Add(this._loadingCircle);
			this.Controls.Add(this._startButton);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "NetworkScannerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Block File Scanner";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox _logBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private Sphere10.Framework.Windows.Forms.DatabaseConnectionBar _dbConnectionBar;
        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.Button _testDatabaseButton;
        private Sphere10.Framework.Windows.Forms.ValidationIndicator _BLKDataFolderValidator;
        private Sphere10.Framework.Windows.Forms.LoadingCircle _loadingCircle;
        private Sphere10.Framework.Windows.Forms.TextBoxEx _nodeIPTextBox;
        private System.Windows.Forms.Button _testNodeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Sphere10.Framework.Windows.Forms.IntBox _nodePortBox;
        private Sphere10.Framework.Windows.Forms.ProgressBarEx _progressBar;
    }
}