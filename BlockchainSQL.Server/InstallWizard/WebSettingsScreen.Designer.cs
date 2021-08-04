
namespace BlockchainSQL.Server {
	partial class WebSettingsScreen {
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
            this._groupBox = new System.Windows.Forms.GroupBox();
            this._webSettingsControl = new BlockchainSQL.Server.Controls.WebSettingsControl();
            this._groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._webSettingsControl);
            this._groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox.Location = new System.Drawing.Point(0, 0);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(602, 247);
            this._groupBox.TabIndex = 0;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "Web Settings";
            // 
            // _webSettingsControl
            // 
            this._webSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._webSettingsControl.Location = new System.Drawing.Point(3, 19);
            this._webSettingsControl.Model = null;
            this._webSettingsControl.Name = "_webSettingsControl";
            this._webSettingsControl.Size = new System.Drawing.Size(596, 225);
            this._webSettingsControl.TabIndex = 0;
            // 
            // WebSettingsScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupBox);
            this.Name = "WebSettingsScreen";
            this.Size = new System.Drawing.Size(598, 247);
            this._groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _groupBox;
		private Controls.WebSettingsControl _webSettingsControl;
	}
}
