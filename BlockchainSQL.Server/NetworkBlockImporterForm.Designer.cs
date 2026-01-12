namespace BlockchainSQL.Server {
	partial class NetworkBlockImporterForm {
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
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkBlockImporterForm));
			groupBox1 = new System.Windows.Forms.GroupBox();
			_logBox = new System.Windows.Forms.TextBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			_nodePortBox = new Sphere10.Framework.Windows.Forms.IntBox();
			_nodeIPTextBox = new Sphere10.Framework.Windows.Forms.TextBoxEx();
			_testNodeButton = new System.Windows.Forms.Button();
			_BLKDataFolderValidator = new Sphere10.Framework.Windows.Forms.ValidationIndicator();
			groupBox3 = new System.Windows.Forms.GroupBox();
			_testDatabaseButton = new System.Windows.Forms.Button();
			_dbConnectionBar = new Sphere10.Framework.Windows.Forms.DatabaseConnectionBar();
			_startButton = new System.Windows.Forms.Button();
			_loadingCircle = new Sphere10.Framework.Windows.Forms.LoadingCircle();
			_progressBar = new Sphere10.Framework.Windows.Forms.ProgressBarEx();
			groupBox4 = new System.Windows.Forms.GroupBox();
			_blocksPerBatchIntBox = new Sphere10.Framework.Windows.Forms.IntBox();
			_blocksPerBatch = new System.Windows.Forms.Label();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox4.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBox1.Controls.Add(_logBox);
			groupBox1.Location = new System.Drawing.Point(14, 281);
			groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupBox1.Size = new System.Drawing.Size(966, 124);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Log";
			// 
			// _logBox
			// 
			_logBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			_logBox.Location = new System.Drawing.Point(7, 22);
			_logBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_logBox.Multiline = true;
			_logBox.Name = "_logBox";
			_logBox.ReadOnly = true;
			_logBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			_logBox.Size = new System.Drawing.Size(951, 96);
			_logBox.TabIndex = 3;
			// 
			// groupBox2
			// 
			groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBox2.Controls.Add(label3);
			groupBox2.Controls.Add(label2);
			groupBox2.Controls.Add(_nodePortBox);
			groupBox2.Controls.Add(_nodeIPTextBox);
			groupBox2.Controls.Add(_testNodeButton);
			groupBox2.Controls.Add(_BLKDataFolderValidator);
			groupBox2.Location = new System.Drawing.Point(14, 14);
			groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupBox2.Name = "groupBox2";
			groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupBox2.Size = new System.Drawing.Size(966, 75);
			groupBox2.TabIndex = 4;
			groupBox2.TabStop = false;
			groupBox2.Text = "Node";
			// 
			// label3
			// 
			label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(674, 23);
			label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(29, 15);
			label3.TabIndex = 14;
			label3.Text = "Port";
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(7, 23);
			label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(62, 15);
			label2.TabIndex = 13;
			label2.Text = "IP Address";
			// 
			// _nodePortBox
			// 
			_nodePortBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			_nodePortBox.Location = new System.Drawing.Point(678, 42);
			_nodePortBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_nodePortBox.Name = "_nodePortBox";
			_nodePortBox.Size = new System.Drawing.Size(116, 23);
			_nodePortBox.TabIndex = 12;
			// 
			// _nodeIPTextBox
			// 
			_nodeIPTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			_nodeIPTextBox.Location = new System.Drawing.Point(7, 42);
			_nodeIPTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_nodeIPTextBox.Name = "_nodeIPTextBox";
			_nodeIPTextBox.Size = new System.Drawing.Size(663, 23);
			_nodeIPTextBox.TabIndex = 10;
			// 
			// _testNodeButton
			// 
			_testNodeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			_testNodeButton.Location = new System.Drawing.Point(802, 42);
			_testNodeButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_testNodeButton.Name = "_testNodeButton";
			_testNodeButton.Size = new System.Drawing.Size(130, 27);
			_testNodeButton.TabIndex = 9;
			_testNodeButton.Text = "Test Connection";
			_testNodeButton.UseVisualStyleBackColor = true;
			_testNodeButton.Click += _testNodeButton_Click;
			// 
			// _BLKDataFolderValidator
			// 
			_BLKDataFolderValidator.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			_BLKDataFolderValidator.BackgroundImage = (System.Drawing.Image)resources.GetObject("_BLKDataFolderValidator.BackgroundImage");
			_BLKDataFolderValidator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			_BLKDataFolderValidator.Location = new System.Drawing.Point(934, 44);
			_BLKDataFolderValidator.Margin = new System.Windows.Forms.Padding(0);
			_BLKDataFolderValidator.Name = "_BLKDataFolderValidator";
			_BLKDataFolderValidator.RunValidatorWhenEnabled = false;
			_BLKDataFolderValidator.Size = new System.Drawing.Size(23, 23);
			_BLKDataFolderValidator.State = Sphere10.Framework.Windows.Forms.ValidationState.Error;
			_BLKDataFolderValidator.TabIndex = 7;
			// 
			// groupBox3
			// 
			groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBox3.Controls.Add(_testDatabaseButton);
			groupBox3.Controls.Add(_dbConnectionBar);
			groupBox3.Location = new System.Drawing.Point(14, 96);
			groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupBox3.Name = "groupBox3";
			groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			groupBox3.Size = new System.Drawing.Size(966, 112);
			groupBox3.TabIndex = 6;
			groupBox3.TabStop = false;
			groupBox3.Text = "Database Output";
			// 
			// _testDatabaseButton
			// 
			_testDatabaseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			_testDatabaseButton.Location = new System.Drawing.Point(830, 78);
			_testDatabaseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_testDatabaseButton.Name = "_testDatabaseButton";
			_testDatabaseButton.Size = new System.Drawing.Size(130, 27);
			_testDatabaseButton.TabIndex = 7;
			_testDatabaseButton.Text = "Test Connection";
			_testDatabaseButton.UseVisualStyleBackColor = true;
			_testDatabaseButton.Click += _testDatabaseButton_Click;
			// 
			// _dbConnectionBar
			// 
			_dbConnectionBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			_dbConnectionBar.ArtificialKeysFile = null;
			_dbConnectionBar.Location = new System.Drawing.Point(9, 24);
			_dbConnectionBar.Margin = new System.Windows.Forms.Padding(0);
			_dbConnectionBar.MinimumSize = new System.Drawing.Size(583, 46);
			_dbConnectionBar.Name = "_dbConnectionBar";
			_dbConnectionBar.Size = new System.Drawing.Size(950, 47);
			_dbConnectionBar.TabIndex = 6;
			// 
			// _startButton
			// 
			_startButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			_startButton.Location = new System.Drawing.Point(850, 412);
			_startButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_startButton.Name = "_startButton";
			_startButton.Size = new System.Drawing.Size(130, 27);
			_startButton.TabIndex = 4;
			_startButton.Text = "Start";
			_startButton.UseVisualStyleBackColor = true;
			_startButton.Click += _startButton_Click;
			// 
			// _loadingCircle
			// 
			_loadingCircle.Active = false;
			_loadingCircle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			_loadingCircle.BackColor = System.Drawing.Color.Transparent;
			_loadingCircle.Color = System.Drawing.Color.DarkGray;
			_loadingCircle.HideStopControl = null;
			_loadingCircle.InnerCircleRadius = 8;
			_loadingCircle.Location = new System.Drawing.Point(803, 412);
			_loadingCircle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_loadingCircle.Name = "_loadingCircle";
			_loadingCircle.NumberSpoke = 10;
			_loadingCircle.OuterCircleRadius = 10;
			_loadingCircle.RotationSpeed = 100;
			_loadingCircle.Size = new System.Drawing.Size(41, 25);
			_loadingCircle.SpokeThickness = 4;
			_loadingCircle.TabIndex = 7;
			_loadingCircle.Text = "_loadingCircle";
			_loadingCircle.Visible = false;
			// 
			// _progressBar
			// 
			_progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			_progressBar.CustomText = null;
			_progressBar.DisplayStyle = Sphere10.Framework.Windows.Forms.ProgressBarDisplayText.Percentage;
			_progressBar.Location = new System.Drawing.Point(21, 412);
			_progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			_progressBar.Name = "_progressBar";
			_progressBar.Size = new System.Drawing.Size(782, 27);
			_progressBar.TabIndex = 9;
			_progressBar.Visible = false;
			// 
			// groupBox4
			// 
			groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBox4.Controls.Add(_blocksPerBatch);
			groupBox4.Controls.Add(_blocksPerBatchIntBox);
			groupBox4.Location = new System.Drawing.Point(14, 214);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new System.Drawing.Size(966, 61);
			groupBox4.TabIndex = 10;
			groupBox4.TabStop = false;
			groupBox4.Text = "Options";
			// 
			// _blocksPerBatchIntBox
			// 
			_blocksPerBatchIntBox.Location = new System.Drawing.Point(130, 22);
			_blocksPerBatchIntBox.Name = "_blocksPerBatchIntBox";
			_blocksPerBatchIntBox.Size = new System.Drawing.Size(100, 23);
			_blocksPerBatchIntBox.TabIndex = 0;
			// 
			// _blocksPerBatch
			// 
			_blocksPerBatch.AutoSize = true;
			_blocksPerBatch.Location = new System.Drawing.Point(30, 25);
			_blocksPerBatch.Name = "_blocksPerBatch";
			_blocksPerBatch.Size = new System.Drawing.Size(94, 15);
			_blocksPerBatch.TabIndex = 1;
			_blocksPerBatch.Text = "Blocks Per Batch";
			// 
			// NetworkBlockImporterForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(994, 452);
			Controls.Add(groupBox4);
			Controls.Add(_progressBar);
			Controls.Add(_loadingCircle);
			Controls.Add(_startButton);
			Controls.Add(groupBox3);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "NetworkBlockImporterForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Network Block Importer";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox4.ResumeLayout(false);
			groupBox4.PerformLayout();
			ResumeLayout(false);
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
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label _blocksPerBatch;
		private Sphere10.Framework.Windows.Forms.IntBox _blocksPerBatchIntBox;
	}
}