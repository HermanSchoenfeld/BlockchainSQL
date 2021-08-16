
namespace BlockchainSQL.Server.Controls {
	partial class ScannerSettingsControl {
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
            this._maxMemoryIntBox = new Sphere10.Framework.Windows.Forms.IntBox();
            this.label4 = new System.Windows.Forms.Label();
            this._optionsListBox = new Sphere10.Framework.Windows.Forms.CheckedListBoxEx();
            this.SuspendLayout();
            // 
            // _maxMemoryIntBox
            // 
            this._maxMemoryIntBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._maxMemoryIntBox.Location = new System.Drawing.Point(93, 33);
            this._maxMemoryIntBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this._maxMemoryIntBox.Name = "_maxMemoryIntBox";
            this._maxMemoryIntBox.PlaceHolderText = "Enter the maximum memory to allocate in processing buffers";
            this._maxMemoryIntBox.Size = new System.Drawing.Size(460, 23);
            this._maxMemoryIntBox.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "Memory (MB)";
            // 
            // _optionsListBox
            // 
            this._optionsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsListBox.BackColor = System.Drawing.SystemColors.Control;
            this._optionsListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._optionsListBox.CheckOnClick = true;
            this._optionsListBox.FormattingEnabled = true;
            this._optionsListBox.Items.AddRange(new object[] {
            "Save Script Instructions (warning: results in much larger database and has very s" +
                "low initial build)"});
            this._optionsListBox.Location = new System.Drawing.Point(0, 5);
            this._optionsListBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this._optionsListBox.Name = "_optionsListBox";
            this._optionsListBox.Size = new System.Drawing.Size(550, 18);
            this._optionsListBox.TabIndex = 20;
            // 
            // ScannerSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._maxMemoryIntBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._optionsListBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ScannerSettingsControl";
            this.Size = new System.Drawing.Size(560, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private Sphere10.Framework.Windows.Forms.IntBox _maxMemoryIntBox;
		private System.Windows.Forms.Label label4;
		private Sphere10.Framework.Windows.Forms.CheckedListBoxEx _optionsListBox;
	}
}
