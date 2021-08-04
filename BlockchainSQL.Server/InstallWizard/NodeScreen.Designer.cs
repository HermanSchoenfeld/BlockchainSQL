
namespace BlockchainSQL.Server {
	partial class NodeScreen {
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
            this._nodeSettingsControl = new BlockchainSQL.Server.Controls.NodeSettingsControl();
            this._groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._nodeSettingsControl);
            this._groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox.Location = new System.Drawing.Point(0, 0);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(713, 136);
            this._groupBox.TabIndex = 1;
            this._groupBox.TabStop = false;
            this._groupBox.Text = "groupBox1";
            // 
            // _nodeSettingsControl
            // 
            this._nodeSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nodeSettingsControl.Location = new System.Drawing.Point(3, 19);
            this._nodeSettingsControl.Model = null;
            this._nodeSettingsControl.Name = "_nodeSettingsControl";
            this._nodeSettingsControl.Size = new System.Drawing.Size(707, 114);
            this._nodeSettingsControl.TabIndex = 1;
            // 
            // NodeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupBox);
            this.Name = "NodeScreen";
            this.Size = new System.Drawing.Size(713, 136);
            this._groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _groupBox;
		private Controls.NodeSettingsControl _nodeSettingsControl;
	}
}
