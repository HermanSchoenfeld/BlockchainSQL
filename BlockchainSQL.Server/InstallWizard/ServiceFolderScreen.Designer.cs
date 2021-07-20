
namespace BlockchainSQL.Server {
	partial class ServiceFolderScreen {
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
            this._pathSelector = new Sphere10.Framework.Windows.Forms.PathSelectorControl();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _pathSelector
            // 
            this._pathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._pathSelector.Location = new System.Drawing.Point(121, 7);
            this._pathSelector.Margin = new System.Windows.Forms.Padding(7);
            this._pathSelector.Mode = Sphere10.Framework.Windows.Forms.PathSelectionMode.Folder;
            this._pathSelector.Name = "_pathSelector";
            this._pathSelector.Path = "";
            this._pathSelector.PlaceHolderText = "Select destination folder to install BlockchainSQL service to";
            this._pathSelector.Size = new System.Drawing.Size(531, 23);
            this._pathSelector.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Destination Folder";
            // 
            // ServiceFolderStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pathSelector);
            this.Controls.Add(this.label7);
            this.Name = "ServiceFolderStep";
            this.Size = new System.Drawing.Size(652, 84);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Sphere10.Framework.Windows.Forms.PathSelectorControl _pathSelector;
		private System.Windows.Forms.Label label7;
	}
}
