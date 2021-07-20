
namespace BlockchainSQL.Server {
	partial class ScannerScreen {
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
            this._scannerSettingsControl = new BlockchainSQL.Server.Controls.ScannerSettingsControl();
            this.SuspendLayout();
            // 
            // _scannerSettingsControl
            // 
            this._scannerSettingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._scannerSettingsControl.AutoDetectChildStateChanges = false;
            this._scannerSettingsControl.Location = new System.Drawing.Point(3, 3);
            this._scannerSettingsControl.Model = null;
            this._scannerSettingsControl.Name = "_scannerSettingsControl";
            this._scannerSettingsControl.Size = new System.Drawing.Size(680, 86);
            this._scannerSettingsControl.TabIndex = 0;
            // 
            // ScannerStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._scannerSettingsControl);
            this.Name = "ScannerStep";
            this.Size = new System.Drawing.Size(686, 87);
            this.ResumeLayout(false);

		}

		#endregion

		private Controls.ScannerSettingsControl _scannerSettingsControl;
	}
}
