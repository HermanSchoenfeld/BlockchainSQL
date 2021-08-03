
namespace BlockchainSQL.Server {
	partial class BlockchainDatabaseSettingsControl {
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
            this._databaseConnectionPanel = new Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel();
            this._loadingCircle = new Sphere10.Framework.Windows.Forms.LoadingCircle();
            this._generateDatabaseButton = new System.Windows.Forms.Button();
            this._testButton = new System.Windows.Forms.Button();
            this._line1 = new DevAge.Windows.Forms.Line();
            this.SuspendLayout();
            // 
            // _databaseConnectionPanel
            // 
            this._databaseConnectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._databaseConnectionPanel.Location = new System.Drawing.Point(0, 0);
            this._databaseConnectionPanel.Margin = new System.Windows.Forms.Padding(23, 27, 23, 27);
            this._databaseConnectionPanel.Name = "_databaseConnectionPanel";
            this._databaseConnectionPanel.Size = new System.Drawing.Size(468, 212);
            this._databaseConnectionPanel.TabIndex = 1;
            // 
            // _loadingCircle
            // 
            this._loadingCircle.Active = false;
            this._loadingCircle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._loadingCircle.BackColor = System.Drawing.Color.Transparent;
            this._loadingCircle.Color = System.Drawing.Color.DarkGray;
            this._loadingCircle.InnerCircleRadius = 5;
            this._loadingCircle.Location = new System.Drawing.Point(243, 209);
            this._loadingCircle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._loadingCircle.Name = "_loadingCircle";
            this._loadingCircle.NumberSpoke = 12;
            this._loadingCircle.OuterCircleRadius = 11;
            this._loadingCircle.RotationSpeed = 100;
            this._loadingCircle.Size = new System.Drawing.Size(33, 38);
            this._loadingCircle.SpokeThickness = 2;
            this._loadingCircle.StylePreset = Sphere10.Framework.Windows.Forms.LoadingCircle.StylePresets.MacOSX;
            this._loadingCircle.TabIndex = 8;
            this._loadingCircle.Text = "_loadingCircle";
            this._loadingCircle.Visible = false;
            // 
            // _generateDatabaseButton
            // 
            this._generateDatabaseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._generateDatabaseButton.Location = new System.Drawing.Point(380, 217);
            this._generateDatabaseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._generateDatabaseButton.Name = "_generateDatabaseButton";
            this._generateDatabaseButton.Size = new System.Drawing.Size(88, 27);
            this._generateDatabaseButton.TabIndex = 10;
            this._generateDatabaseButton.Text = "Generate Database";
            this._generateDatabaseButton.UseVisualStyleBackColor = true;
            this._generateDatabaseButton.Click += new System.EventHandler(this._generateDatabaseButton_Click);
            // 
            // _testButton
            // 
            this._testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._testButton.Location = new System.Drawing.Point(284, 217);
            this._testButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._testButton.Name = "_testButton";
            this._testButton.Size = new System.Drawing.Size(88, 27);
            this._testButton.TabIndex = 9;
            this._testButton.Text = "Test Connection";
            this._testButton.UseVisualStyleBackColor = true;
            this._testButton.Click += new System.EventHandler(this._testButton_Click);
            // 
            // _line1
            // 
            this._line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._line1.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this._line1.FirstColor = System.Drawing.SystemColors.ControlDark;
            this._line1.LineStyle = DevAge.Windows.Forms.LineStyle.Horizontal;
            this._line1.Location = new System.Drawing.Point(0, 210);
            this._line1.Name = "_line1";
            this._line1.SecondColor = System.Drawing.SystemColors.ControlLightLight;
            this._line1.Size = new System.Drawing.Size(465, 2);
            this._line1.TabIndex = 11;
            this._line1.TabStop = false;
            // 
            // BlockchainDatabaseSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._line1);
            this.Controls.Add(this._loadingCircle);
            this.Controls.Add(this._generateDatabaseButton);
            this.Controls.Add(this._testButton);
            this.Controls.Add(this._databaseConnectionPanel);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "BlockchainDatabaseSettingsControl";
            this.Size = new System.Drawing.Size(468, 247);
            this.ResumeLayout(false);

		}

		#endregion

		private Sphere10.Framework.Windows.Forms.DatabaseConnectionPanel _databaseConnectionPanel;
		private Sphere10.Framework.Windows.Forms.LoadingCircle _loadingCircle;
		private System.Windows.Forms.Button _generateDatabaseButton;
		private System.Windows.Forms.Button _testButton;
		private DevAge.Windows.Forms.Line _line1;
	}
}
