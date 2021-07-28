
namespace BlockchainSQL.Server {
	partial class BlockchainDatabaseScreen {
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
			this._blockchainDatabaseSettingsControl = new BlockchainSQL.Server.BlockchainDatabaseSettingsControl();
			this._groupBox = new System.Windows.Forms.GroupBox();
			this._groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// _blockchainDatabaseSettingsControl
			// 
			this._blockchainDatabaseSettingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._blockchainDatabaseSettingsControl.Location = new System.Drawing.Point(10, 37);
			this._blockchainDatabaseSettingsControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
			this._blockchainDatabaseSettingsControl.Model = null;
			this._blockchainDatabaseSettingsControl.Name = "_blockchainDatabaseSettingsControl";
			this._blockchainDatabaseSettingsControl.Size = new System.Drawing.Size(751, 323);
			this._blockchainDatabaseSettingsControl.TabIndex = 0;
			// 
			// _groupBox
			// 
			this._groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._groupBox.Controls.Add(this._blockchainDatabaseSettingsControl);
			this._groupBox.Location = new System.Drawing.Point(0, 0);
			this._groupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._groupBox.Name = "_groupBox";
			this._groupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._groupBox.Size = new System.Drawing.Size(771, 373);
			this._groupBox.TabIndex = 1;
			this._groupBox.TabStop = false;
			this._groupBox.Text = "Select Blockchain Database";
			// 
			// BlockchainDatabaseScreen
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this._groupBox);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "BlockchainDatabaseScreen";
			this.Size = new System.Drawing.Size(771, 373);
			this._groupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private BlockchainDatabaseSettingsControl _blockchainDatabaseSettingsControl;
		private System.Windows.Forms.GroupBox _groupBox;
	}
}
