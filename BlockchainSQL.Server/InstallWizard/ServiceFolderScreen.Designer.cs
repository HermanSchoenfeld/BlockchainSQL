
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
			this._groupBox = new System.Windows.Forms.GroupBox();
			this._installDirLabel = new System.Windows.Forms.Label();
			this._createServiceFolderCheckBox = new System.Windows.Forms.CheckBox();
			this._pathSelector = new Sphere10.Framework.Windows.Forms.PathSelectorControl();
			this.label7 = new System.Windows.Forms.Label();
			this._groupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// _groupBox
			// 
			this._groupBox.Controls.Add(this._installDirLabel);
			this._groupBox.Controls.Add(this._createServiceFolderCheckBox);
			this._groupBox.Controls.Add(this._pathSelector);
			this._groupBox.Controls.Add(this.label7);
			this._groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._groupBox.Location = new System.Drawing.Point(0, 0);
			this._groupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._groupBox.Name = "_groupBox";
			this._groupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this._groupBox.Size = new System.Drawing.Size(577, 202);
			this._groupBox.TabIndex = 14;
			this._groupBox.TabStop = false;
			this._groupBox.Text = "Select Service Installation Directory";
			// 
			// _installDirLabel
			// 
			this._installDirLabel.AutoSize = true;
			this._installDirLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this._installDirLabel.Location = new System.Drawing.Point(95, 131);
			this._installDirLabel.Name = "_installDirLabel";
			this._installDirLabel.Size = new System.Drawing.Size(153, 15);
			this._installDirLabel.TabIndex = 17;
			this._installDirLabel.Text = "<<installation directory>>";
			this._installDirLabel.Visible = false;
			// 
			// _createServiceFolderCheckBox
			// 
			this._createServiceFolderCheckBox.AutoSize = true;
			this._createServiceFolderCheckBox.Checked = true;
			this._createServiceFolderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this._createServiceFolderCheckBox.Location = new System.Drawing.Point(95, 86);
			this._createServiceFolderCheckBox.Name = "_createServiceFolderCheckBox";
			this._createServiceFolderCheckBox.Size = new System.Drawing.Size(254, 19);
			this._createServiceFolderCheckBox.TabIndex = 16;
			this._createServiceFolderCheckBox.Text = "Create sub-folder \'BlockchainSQL\' in above";
			this._createServiceFolderCheckBox.UseVisualStyleBackColor = true;
			this._createServiceFolderCheckBox.CheckedChanged += new System.EventHandler(this._createServiceFolderCheckBox_CheckedChanged);
			// 
			// _pathSelector
			// 
			this._pathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._pathSelector.Location = new System.Drawing.Point(95, 33);
			this._pathSelector.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
			this._pathSelector.Mode = Sphere10.Framework.Windows.Forms.PathSelectionMode.Folder;
			this._pathSelector.Name = "_pathSelector";
			this._pathSelector.Path = "";
			this._pathSelector.PlaceHolderText = "Select a directory (e.g. C:\\Program Files)";
			this._pathSelector.Size = new System.Drawing.Size(472, 38);
			this._pathSelector.TabIndex = 14;
			this._pathSelector.PathChanged += new System.EventHandler(this._pathSelector_PathChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(10, 44);
			this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(82, 15);
			this.label7.TabIndex = 15;
			this.label7.Text = "Base Directory";
			// 
			// ServiceFolderScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._groupBox);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "ServiceFolderScreen";
			this.Size = new System.Drawing.Size(577, 202);
			this._groupBox.ResumeLayout(false);
			this._groupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox _groupBox;
		private Sphere10.Framework.Windows.Forms.PathSelectorControl _pathSelector;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label _installDirLabel;
		private System.Windows.Forms.CheckBox _createServiceFolderCheckBox;
	}
}
