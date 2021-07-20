
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
            this.webUiPortLabel = new System.Windows.Forms.Label();
            this._enableWebUICheckBox = new System.Windows.Forms.CheckBox();
            this._databaseConnectionPanel = new Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel();
            this._loadingCircle = new Sphere10.Framework.Windows.Forms.LoadingCircle();
            this._generateDatabaseButton = new System.Windows.Forms.Button();
            this._testButton = new System.Windows.Forms.Button();
            this._portIntBox = new Sphere10.Framework.Windows.Forms.IntBox();
            this.SuspendLayout();
            // 
            // webUiPortLabel
            // 
            this.webUiPortLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.webUiPortLabel.AutoSize = true;
            this.webUiPortLabel.Location = new System.Drawing.Point(399, 13);
            this.webUiPortLabel.Name = "webUiPortLabel";
            this.webUiPortLabel.Size = new System.Drawing.Size(60, 15);
            this.webUiPortLabel.TabIndex = 19;
            this.webUiPortLabel.Text = "HTTP Port";
            // 
            // _enableWebUICheckBox
            // 
            this._enableWebUICheckBox.AutoSize = true;
            this._enableWebUICheckBox.Location = new System.Drawing.Point(96, 12);
            this._enableWebUICheckBox.Name = "_enableWebUICheckBox";
            this._enableWebUICheckBox.Size = new System.Drawing.Size(232, 19);
            this._enableWebUICheckBox.TabIndex = 17;
            this._enableWebUICheckBox.Text = "Enable web user-interface (self-hosted)";
            this._enableWebUICheckBox.UseVisualStyleBackColor = true;
            // 
            // _databaseConnectionPanel
            // 
            this._databaseConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._databaseConnectionPanel.Location = new System.Drawing.Point(14, 33);
            this._databaseConnectionPanel.Margin = new System.Windows.Forms.Padding(14);
            this._databaseConnectionPanel.Name = "_databaseConnectionPanel";
            this._databaseConnectionPanel.Size = new System.Drawing.Size(536, 150);
            this._databaseConnectionPanel.TabIndex = 16;
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = false;
            this._loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 5;
            this._loadingCircle.Location = new System.Drawing.Point(281, 189);
            this._loadingCircle.Margin = new System.Windows.Forms.Padding(2);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 12;
            this._loadingCircle.OuterCircleRadius = 11;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(31, 25);
            this._loadingCircle.SpokeThickness = 2;
            this._loadingCircle.StylePreset = Sphere10.Framework.Windows.Forms.LoadingCircle.StylePresets.MacOSX;
            this._loadingCircle.TabIndex = 21;
            this._loadingCircle.Text = "_loadingCircle";
            this._loadingCircle.Visible = false;
            // 
            // _generateDatabaseButton
            // 
            this._generateDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._generateDatabaseButton.Location = new System.Drawing.Point(434, 189);
            this._generateDatabaseButton.Margin = new System.Windows.Forms.Padding(2);
            this._generateDatabaseButton.Name = "_generateDatabaseButton";
            this._generateDatabaseButton.Size = new System.Drawing.Size(114, 27);
            this._generateDatabaseButton.TabIndex = 23;
            this._generateDatabaseButton.Text = "Generate Database";
            this._generateDatabaseButton.UseVisualStyleBackColor = true;
            // 
            // _testButton
            // 
            this._testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._testButton.Location = new System.Drawing.Point(316, 189);
            this._testButton.Margin = new System.Windows.Forms.Padding(2);
            this._testButton.Name = "_testButton";
            this._testButton.Size = new System.Drawing.Size(114, 27);
            this._testButton.TabIndex = 22;
            this._testButton.Text = "Test Connection";
            this._testButton.UseVisualStyleBackColor = true;
            // 
            // _portIntBox
            // 
            this._portIntBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._portIntBox.Location = new System.Drawing.Point(466, 10);
            this._portIntBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._portIntBox.Name = "_portIntBox";
            this._portIntBox.PlaceHolderText = "Port #";
            this._portIntBox.Size = new System.Drawing.Size(84, 23);
            this._portIntBox.TabIndex = 24;
            // 
            // WebSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._portIntBox);
            this.Controls.Add(this._loadingCircle);
            this.Controls.Add(this._generateDatabaseButton);
            this.Controls.Add(this._testButton);
            this.Controls.Add(this.webUiPortLabel);
            this.Controls.Add(this._enableWebUICheckBox);
            this.Controls.Add(this._databaseConnectionPanel);
            this.Name = "WebSettingsControl";
            this.Size = new System.Drawing.Size(550, 218);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label webUiPortLabel;
		private System.Windows.Forms.CheckBox _enableWebUICheckBox;
		private Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel _databaseConnectionPanel;
		private Sphere10.Framework.Windows.Forms.LoadingCircle _loadingCircle;
		private System.Windows.Forms.Button _generateDatabaseButton;
		private System.Windows.Forms.Button _testButton;
		private Sphere10.Framework.Windows.Forms.IntBox _portIntBox;
	}
}
