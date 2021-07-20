
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
            this._nodeSettingsControl = new BlockchainSQL.Server.Controls.NodeSettingsControl();
            this.SuspendLayout();
            // 
            // _nodeSettingsControl
            // 
            this._nodeSettingsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._nodeSettingsControl.Location = new System.Drawing.Point(3, 3);
            this._nodeSettingsControl.Model = null;
            this._nodeSettingsControl.Name = "_nodeSettingsControl";
            this._nodeSettingsControl.Size = new System.Drawing.Size(696, 87);
            this._nodeSettingsControl.TabIndex = 0;
            // 
            // NodeStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._nodeSettingsControl);
            this.Name = "NodeStep";
            this.Size = new System.Drawing.Size(699, 90);
            this.ResumeLayout(false);

		}

		#endregion

		private Controls.NodeSettingsControl _nodeSettingsControl;
	}
}
