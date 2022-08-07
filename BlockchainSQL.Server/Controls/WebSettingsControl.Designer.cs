
namespace BlockchainSQL.Server.Controls {
	partial class WebSettingsControl {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this._portLabel = new System.Windows.Forms.Label();
			this._enableWebUICheckBox = new System.Windows.Forms.CheckBox();
			this._loadingCircle = new Hydrogen.Windows.Forms.LoadingCircle();
			this._portIntBox = new Hydrogen.Windows.Forms.IntBox();
			this._splitContainer = new System.Windows.Forms.SplitContainer();
			this._webDBGroupBox = new System.Windows.Forms.GroupBox();
			this._generateWebDatabaseButton = new System.Windows.Forms.Button();
			this._testWebDatabaseButton = new System.Windows.Forms.Button();
			this._webDatabaseConnectionPanel = new Hydrogen.Windows.Forms.DatabaseConnectionPanel();
			this._bsqlDBGroupBox = new System.Windows.Forms.GroupBox();
			this._testBlockchainDatabaseButton = new System.Windows.Forms.Button();
			this._blockchainDatabaseConnectionPanel = new Hydrogen.Windows.Forms.DatabaseConnectionPanel();
			((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
			this._splitContainer.Panel1.SuspendLayout();
			this._splitContainer.Panel2.SuspendLayout();
			this._splitContainer.SuspendLayout();
			this._webDBGroupBox.SuspendLayout();
			this._bsqlDBGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// _portLabel
			// 
			this._portLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._portLabel.AutoSize = true;
			this._portLabel.Location = new System.Drawing.Point(477, 4);
			this._portLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this._portLabel.Name = "_portLabel";
			this._portLabel.Size = new System.Drawing.Size(60, 15);
			this._portLabel.TabIndex = 19;
			this._portLabel.Text = "HTTP Port";
			// 
			// _enableWebUICheckBox
			// 
			this._enableWebUICheckBox.AutoSize = true;
			this._enableWebUICheckBox.Location = new System.Drawing.Point(0, 2);
			this._enableWebUICheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._enableWebUICheckBox.Name = "_enableWebUICheckBox";
			this._enableWebUICheckBox.Size = new System.Drawing.Size(232, 19);
			this._enableWebUICheckBox.TabIndex = 17;
			this._enableWebUICheckBox.Text = "Enable web user-interface (self-hosted)";
			this._enableWebUICheckBox.UseVisualStyleBackColor = true;
			this._enableWebUICheckBox.CheckedChanged += new System.EventHandler(this._enableWebUICheckBox_CheckedChanged);
			// 
			// _loadingCircle
			// 
			this._loadingCircle.Active = false;
			this._loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
			this._loadingCircle.Color = System.Drawing.Color.DarkGray;
			this._loadingCircle.InnerCircleRadius = 5;
			this._loadingCircle.Location = new System.Drawing.Point(570, 347);
			this._loadingCircle.Name = "_loadingCircle";
			this._loadingCircle.NumberSpoke = 12;
			this._loadingCircle.OuterCircleRadius = 11;
			this._loadingCircle.RotationSpeed = 100;
			this._loadingCircle.Size = new System.Drawing.Size(27, 27);
			this._loadingCircle.SpokeThickness = 2;
			this._loadingCircle.StylePreset = Hydrogen.Windows.Forms.LoadingCircle.StylePresets.MacOSX;
			this._loadingCircle.TabIndex = 21;
			this._loadingCircle.Text = "_loadingCircle";
			this._loadingCircle.Visible = false;
			// 
			// _portIntBox
			// 
			this._portIntBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._portIntBox.Location = new System.Drawing.Point(547, 0);
			this._portIntBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this._portIntBox.Name = "_portIntBox";
			this._portIntBox.PlaceHolderText = "Port #";
			this._portIntBox.Size = new System.Drawing.Size(50, 23);
			this._portIntBox.TabIndex = 24;
			// 
			// _splitContainer
			// 
			this._splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._splitContainer.Cursor = System.Windows.Forms.Cursors.VSplit;
			this._splitContainer.Location = new System.Drawing.Point(3, 31);
			this._splitContainer.Name = "_splitContainer";
			// 
			// _splitContainer.Panel1
			// 
			this._splitContainer.Panel1.Controls.Add(this._webDBGroupBox);
			// 
			// _splitContainer.Panel2
			// 
			this._splitContainer.Panel2.Controls.Add(this._bsqlDBGroupBox);
			this._splitContainer.Size = new System.Drawing.Size(609, 307);
			this._splitContainer.SplitterDistance = 304;
			this._splitContainer.TabIndex = 31;
			this._splitContainer.SizeChanged += new System.EventHandler(this._splitContainer_SizeChanged);
			// 
			// _webDBGroupBox
			// 
			this._webDBGroupBox.Controls.Add(this._generateWebDatabaseButton);
			this._webDBGroupBox.Controls.Add(this._testWebDatabaseButton);
			this._webDBGroupBox.Controls.Add(this._webDatabaseConnectionPanel);
			this._webDBGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._webDBGroupBox.Location = new System.Drawing.Point(0, 0);
			this._webDBGroupBox.Name = "_webDBGroupBox";
			this._webDBGroupBox.Size = new System.Drawing.Size(304, 307);
			this._webDBGroupBox.TabIndex = 30;
			this._webDBGroupBox.TabStop = false;
			this._webDBGroupBox.Text = "Web Database";
			// 
			// _generateWebDatabaseButton
			// 
			this._generateWebDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._generateWebDatabaseButton.Location = new System.Drawing.Point(210, 274);
			this._generateWebDatabaseButton.Name = "_generateWebDatabaseButton";
			this._generateWebDatabaseButton.Size = new System.Drawing.Size(88, 27);
			this._generateWebDatabaseButton.TabIndex = 25;
			this._generateWebDatabaseButton.Text = "Generate Database";
			this._generateWebDatabaseButton.UseVisualStyleBackColor = true;
			this._generateWebDatabaseButton.Click += new System.EventHandler(this._generateWebDatabaseButton_Click);
			// 
			// _testWebDatabaseButton
			// 
			this._testWebDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._testWebDatabaseButton.Location = new System.Drawing.Point(116, 274);
			this._testWebDatabaseButton.Name = "_testWebDatabaseButton";
			this._testWebDatabaseButton.Size = new System.Drawing.Size(88, 27);
			this._testWebDatabaseButton.TabIndex = 24;
			this._testWebDatabaseButton.Text = "Test Connection";
			this._testWebDatabaseButton.UseVisualStyleBackColor = true;
			this._testWebDatabaseButton.Click += new System.EventHandler(this._testWebDatabaseButton_Click);
			// 
			// _webDatabaseConnectionPanel
			// 
			this._webDatabaseConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._webDatabaseConnectionPanel.Location = new System.Drawing.Point(3, 19);
			this._webDatabaseConnectionPanel.Margin = new System.Windows.Forms.Padding(20, 23, 20, 23);
			this._webDatabaseConnectionPanel.Name = "_webDatabaseConnectionPanel";
			this._webDatabaseConnectionPanel.Size = new System.Drawing.Size(301, 248);
			this._webDatabaseConnectionPanel.TabIndex = 18;
			// 
			// _bsqlDBGroupBox
			// 
			this._bsqlDBGroupBox.Controls.Add(this._testBlockchainDatabaseButton);
			this._bsqlDBGroupBox.Controls.Add(this._blockchainDatabaseConnectionPanel);
			this._bsqlDBGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._bsqlDBGroupBox.Location = new System.Drawing.Point(0, 0);
			this._bsqlDBGroupBox.Name = "_bsqlDBGroupBox";
			this._bsqlDBGroupBox.Size = new System.Drawing.Size(301, 307);
			this._bsqlDBGroupBox.TabIndex = 31;
			this._bsqlDBGroupBox.TabStop = false;
			this._bsqlDBGroupBox.Text = "Blockchain Database";
			// 
			// _testBlockchainDatabaseButton
			// 
			this._testBlockchainDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._testBlockchainDatabaseButton.Location = new System.Drawing.Point(198, 274);
			this._testBlockchainDatabaseButton.Name = "_testBlockchainDatabaseButton";
			this._testBlockchainDatabaseButton.Size = new System.Drawing.Size(88, 27);
			this._testBlockchainDatabaseButton.TabIndex = 25;
			this._testBlockchainDatabaseButton.Text = "Test Connection";
			this._testBlockchainDatabaseButton.UseVisualStyleBackColor = true;
			this._testBlockchainDatabaseButton.Click += new System.EventHandler(this._testBlockchainDatabaseButton_Click);
			// 
			// _blockchainDatabaseConnectionPanel
			// 
			this._blockchainDatabaseConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._blockchainDatabaseConnectionPanel.Location = new System.Drawing.Point(3, 19);
			this._blockchainDatabaseConnectionPanel.Margin = new System.Windows.Forms.Padding(20, 23, 20, 23);
			this._blockchainDatabaseConnectionPanel.Name = "_blockchainDatabaseConnectionPanel";
			this._blockchainDatabaseConnectionPanel.Size = new System.Drawing.Size(295, 248);
			this._blockchainDatabaseConnectionPanel.TabIndex = 19;
			// 
			// WebSettingsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._splitContainer);
			this.Controls.Add(this._portIntBox);
			this.Controls.Add(this._loadingCircle);
			this.Controls.Add(this._portLabel);
			this.Controls.Add(this._enableWebUICheckBox);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "WebSettingsControl";
			this.Size = new System.Drawing.Size(607, 374);
			this._splitContainer.Panel1.ResumeLayout(false);
			this._splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
			this._splitContainer.ResumeLayout(false);
			this._webDBGroupBox.ResumeLayout(false);
			this._bsqlDBGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label _portLabel;
		private System.Windows.Forms.CheckBox _enableWebUICheckBox;
		private Hydrogen.Windows.Forms.LoadingCircle _loadingCircle;
		private Hydrogen.Windows.Forms.IntBox _portIntBox;
		private System.Windows.Forms.SplitContainer _splitContainer;
		private System.Windows.Forms.GroupBox _webDBGroupBox;
		private Hydrogen.Windows.Forms.DatabaseConnectionPanel _webDatabaseConnectionPanel;
		private System.Windows.Forms.GroupBox _bsqlDBGroupBox;
		private Hydrogen.Windows.Forms.DatabaseConnectionPanel _blockchainDatabaseConnectionPanel;
		private System.Windows.Forms.Button _generateWebDatabaseButton;
		private System.Windows.Forms.Button _testWebDatabaseButton;
		private System.Windows.Forms.Button _testBlockchainDatabaseButton;
	}
}
