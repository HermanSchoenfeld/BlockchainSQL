
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
            this._groupBox = new System.Windows.Forms.GroupBox();
            this._groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _scannerSettingsControl
            // 
            this._scannerSettingsControl.AutoDetectChildStateChanges = false;
            this._scannerSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._scannerSettingsControl.Location = new System.Drawing.Point(3, 19);
            this._scannerSettingsControl.Model = null;
            this._scannerSettingsControl.Name = "_scannerSettingsControl";
            this._scannerSettingsControl.Size = new System.Drawing.Size(626, 125);
            this._scannerSettingsControl.TabIndex = 0;
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._scannerSettingsControl);
            this._groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox.Location = new System.Drawing.Point(0, 0);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(632, 147);
            this._groupBox.TabIndex = 1;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "Blockchain Scanner Settings";
            // 
            // ScannerScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupBox);
            this.Name = "ScannerScreen";
            this.Size = new System.Drawing.Size(632, 147);
            this._groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private Controls.ScannerSettingsControl _scannerSettingsControl;
		private System.Windows.Forms.GroupBox _groupBox;
	}
}
