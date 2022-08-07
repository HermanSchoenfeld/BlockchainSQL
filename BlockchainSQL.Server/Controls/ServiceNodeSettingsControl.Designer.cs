
namespace BlockchainSQL.Server.Controls {
	partial class ServiceNodeSettingsControl {
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
            this.label5 = new System.Windows.Forms.Label();
            this._pollRateIntBox = new Hydrogen.Windows.Forms.IntBox();
            this.label2 = new System.Windows.Forms.Label();
            this._portIntBox = new Hydrogen.Windows.Forms.IntBox();
            this.label16 = new System.Windows.Forms.Label();
            this._ipTextBox = new Hydrogen.Windows.Forms.TextBoxEx();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 15);
            this.label5.TabIndex = 22;
            this.label5.Text = "Node Poll Rate (sec)";
            // 
            // _pollRateIntBox
            // 
            this._pollRateIntBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pollRateIntBox.Location = new System.Drawing.Point(128, 63);
            this._pollRateIntBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this._pollRateIntBox.Name = "_pollRateIntBox";
            this._pollRateIntBox.PlaceHolderText = "How often to poll the node for block updates via NetProtocol";
            this._pollRateIntBox.Size = new System.Drawing.Size(493, 23);
            this._pollRateIntBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Trusted Node Port";
            // 
            // _portIntBox
            // 
            this._portIntBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._portIntBox.Location = new System.Drawing.Point(128, 33);
            this._portIntBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this._portIntBox.Name = "_portIntBox";
            this._portIntBox.PlaceHolderText = "Enter port number";
            this._portIntBox.Size = new System.Drawing.Size(493, 23);
            this._portIntBox.TabIndex = 18;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 6);
            this.label16.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 15);
            this.label16.TabIndex = 16;
            this.label16.Text = "Trusted Node IP";
            // 
            // _ipTextBox
            // 
            this._ipTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ipTextBox.Location = new System.Drawing.Point(128, 3);
            this._ipTextBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this._ipTextBox.Name = "_ipTextBox";
            this._ipTextBox.PlaceHolderText = "Enter IP address of your trusted Bitcoin Core (BTC) node";
            this._ipTextBox.Size = new System.Drawing.Size(493, 23);
            this._ipTextBox.TabIndex = 15;
            // 
            // NodeSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this._pollRateIntBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._portIntBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this._ipTextBox);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "NodeSettingsControl";
            this.Size = new System.Drawing.Size(628, 89);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label5;
		private Hydrogen.Windows.Forms.IntBox _pollRateIntBox;
		private System.Windows.Forms.Label label2;
		private Hydrogen.Windows.Forms.IntBox _portIntBox;
		private System.Windows.Forms.Label label16;
		private Hydrogen.Windows.Forms.TextBoxEx _ipTextBox;
	}
}
