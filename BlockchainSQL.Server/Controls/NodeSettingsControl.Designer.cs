
namespace BlockchainSQL.Server.Controls {
	partial class NodeSettingsControl {
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
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._pollRateIntBox = new Sphere10.Framework.Windows.Forms.IntBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._portIntBox = new Sphere10.Framework.Windows.Forms.IntBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this._ipTextBox = new Sphere10.Framework.Windows.Forms.TextBoxEx();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "How often to poll the node for block updates via NetProtocol";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 66);
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
            this._pollRateIntBox.Location = new System.Drawing.Point(147, 63);
            this._pollRateIntBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this._pollRateIntBox.Name = "_pollRateIntBox";
            this._pollRateIntBox.PlaceHolderText = "Enter seconds";
            this._pollRateIntBox.Size = new System.Drawing.Size(122, 23);
            this._pollRateIntBox.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Port of your node";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 36);
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
            this._portIntBox.Location = new System.Drawing.Point(147, 33);
            this._portIntBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this._portIntBox.Name = "_portIntBox";
            this._portIntBox.PlaceHolderText = "Enter port number";
            this._portIntBox.Size = new System.Drawing.Size(122, 23);
            this._portIntBox.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(283, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(269, 15);
            this.label12.TabIndex = 17;
            this.label12.Text = "IP address of your trusted Bitcoin Core (BTC) node";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(43, 6);
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
            this._ipTextBox.Location = new System.Drawing.Point(147, 3);
            this._ipTextBox.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this._ipTextBox.Name = "_ipTextBox";
            this._ipTextBox.PlaceHolderText = "Enter IP address";
            this._ipTextBox.Size = new System.Drawing.Size(122, 23);
            this._ipTextBox.TabIndex = 15;
            // 
            // NodeSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this._pollRateIntBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._portIntBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label16);
            this.Controls.Add(this._ipTextBox);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "NodeSettingsControl";
            this.Size = new System.Drawing.Size(618, 95);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private Sphere10.Framework.Windows.Forms.IntBox _pollRateIntBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private Sphere10.Framework.Windows.Forms.IntBox _portIntBox;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label16;
		private Sphere10.Framework.Windows.Forms.TextBoxEx _ipTextBox;
	}
}
