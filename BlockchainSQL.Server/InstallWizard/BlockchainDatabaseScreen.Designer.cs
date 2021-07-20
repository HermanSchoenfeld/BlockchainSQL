
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
            this.SuspendLayout();
            // 
            // _blockchainDatabaseSettingsControl
            // 
            this._blockchainDatabaseSettingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._blockchainDatabaseSettingsControl.Location = new System.Drawing.Point(0, 3);
            this._blockchainDatabaseSettingsControl.Model = null;
            this._blockchainDatabaseSettingsControl.Name = "_blockchainDatabaseSettingsControl";
            this._blockchainDatabaseSettingsControl.Size = new System.Drawing.Size(518, 200);
            this._blockchainDatabaseSettingsControl.TabIndex = 0;
            // 
            // BlockchainDatabaseStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._blockchainDatabaseSettingsControl);
            this.Name = "BlockchainDatabaseStep";
            this.Size = new System.Drawing.Size(521, 202);
            this.ResumeLayout(false);

		}

		#endregion

		private BlockchainDatabaseSettingsControl _blockchainDatabaseSettingsControl;
	}
}
