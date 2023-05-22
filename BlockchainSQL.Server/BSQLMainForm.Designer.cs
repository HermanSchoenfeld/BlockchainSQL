using Hydrogen.Windows.Forms;

namespace BlockchainSQL.Server {
	partial class BSQLMainForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(BSQLMainForm));
			groupBox1 = new System.Windows.Forms.GroupBox();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			_uninstallServiceButton = new System.Windows.Forms.Button();
			_installServiceButton = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			groupBox2 = new System.Windows.Forms.GroupBox();
			button3 = new System.Windows.Forms.Button();
			label9 = new System.Windows.Forms.Label();
			button2 = new System.Windows.Forms.Button();
			label8 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			label7 = new System.Windows.Forms.Label();
			_appBanner = new ApplicationBanner();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			SuspendLayout();
			// 
			// groupBox1
			// 
			groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBox1.Controls.Add(label5);
			groupBox1.Controls.Add(label4);
			groupBox1.Controls.Add(_uninstallServiceButton);
			groupBox1.Controls.Add(_installServiceButton);
			groupBox1.Location = new System.Drawing.Point(11, 165);
			groupBox1.Margin = new System.Windows.Forms.Padding(2);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new System.Windows.Forms.Padding(2);
			groupBox1.Size = new System.Drawing.Size(816, 177);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Service Installer";
			// 
			// label5
			// 
			label5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label5.Location = new System.Drawing.Point(208, 101);
			label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(398, 62);
			label5.TabIndex = 9;
			label5.Text = "Remove a previously installed BlockchainSQL Windows Service.";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label4.Location = new System.Drawing.Point(206, 34);
			label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(571, 62);
			label4.TabIndex = 8;
			label4.Text = "Install a BlockchainSQL Windows Service and optional web interface.";
			label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uninstallServiceButton
			// 
			_uninstallServiceButton.Location = new System.Drawing.Point(9, 101);
			_uninstallServiceButton.Margin = new System.Windows.Forms.Padding(2);
			_uninstallServiceButton.Name = "_uninstallServiceButton";
			_uninstallServiceButton.Size = new System.Drawing.Size(194, 62);
			_uninstallServiceButton.TabIndex = 5;
			_uninstallServiceButton.Text = "Uninstall Service";
			_uninstallServiceButton.UseVisualStyleBackColor = true;
			_uninstallServiceButton.Click += _uninstallServiceButton_Click;
			// 
			// _installServiceButton
			// 
			_installServiceButton.Location = new System.Drawing.Point(9, 34);
			_installServiceButton.Margin = new System.Windows.Forms.Padding(2);
			_installServiceButton.Name = "_installServiceButton";
			_installServiceButton.Size = new System.Drawing.Size(194, 62);
			_installServiceButton.TabIndex = 4;
			_installServiceButton.Text = "Install Service";
			_installServiceButton.UseVisualStyleBackColor = true;
			_installServiceButton.Click += _installServiceButton_Click;
			// 
			// label3
			// 
			label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label3.Location = new System.Drawing.Point(208, 176);
			label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label3.MaximumSize = new System.Drawing.Size(575, 62);
			label3.MinimumSize = new System.Drawing.Size(575, 62);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(575, 62);
			label3.TabIndex = 7;
			label3.Text = "Fill database with blockchain data from a node.";
			label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label2.Location = new System.Drawing.Point(208, 110);
			label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(593, 62);
			label2.TabIndex = 3;
			label2.Text = "Import Bitcoin (BTC) blockchain data from Bitcoin Core BLK data files.";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label1.Location = new System.Drawing.Point(206, 44);
			label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(593, 62);
			label1.TabIndex = 1;
			label1.Text = "Generate or repair an empty BlockchainSQL database.";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox2
			// 
			groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBox2.Controls.Add(button3);
			groupBox2.Controls.Add(label9);
			groupBox2.Controls.Add(label2);
			groupBox2.Controls.Add(button2);
			groupBox2.Controls.Add(label8);
			groupBox2.Controls.Add(label3);
			groupBox2.Controls.Add(button1);
			groupBox2.Controls.Add(label7);
			groupBox2.Controls.Add(label1);
			groupBox2.Location = new System.Drawing.Point(9, 347);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(818, 243);
			groupBox2.TabIndex = 2;
			groupBox2.TabStop = false;
			groupBox2.Text = "Tools";
			// 
			// button3
			// 
			button3.Location = new System.Drawing.Point(10, 97);
			button3.Margin = new System.Windows.Forms.Padding(2);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(194, 62);
			button3.TabIndex = 2;
			button3.Text = "Block File Importer";
			button3.UseVisualStyleBackColor = true;
			button3.Click += _blockFileScannerButton_Click;
			// 
			// label9
			// 
			label9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label9.Location = new System.Drawing.Point(206, 97);
			label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(500, 62);
			label9.TabIndex = 3;
			label9.Text = "Rapidly import blocks into the database by scanning the raw Bitcoin Core BLK data files. ";
			label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button2
			// 
			button2.Location = new System.Drawing.Point(9, 163);
			button2.Margin = new System.Windows.Forms.Padding(2);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(194, 62);
			button2.TabIndex = 6;
			button2.Text = "Network Block Importer";
			button2.UseVisualStyleBackColor = true;
			button2.Click += _networkButton_Click;
			// 
			// label8
			// 
			label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label8.Location = new System.Drawing.Point(206, 163);
			label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(490, 62);
			label8.TabIndex = 7;
			label8.Text = "Import blocks into the database using the network protocol and a trusted Bitcoin node.";
			label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			button1.Location = new System.Drawing.Point(9, 31);
			button1.Margin = new System.Windows.Forms.Padding(2);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(194, 62);
			button1.TabIndex = 0;
			button1.Text = "Generate Database";
			button1.UseVisualStyleBackColor = true;
			button1.Click += _generateDatabaseButton_Click;
			// 
			// label7
			// 
			label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			label7.Location = new System.Drawing.Point(210, 31);
			label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(569, 62);
			label7.TabIndex = 1;
			label7.Text = "Generate an empty or repair an existing BlockchainSQL database.";
			label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _appBanner
			// 
			_appBanner.CompanyName = "{CompanyName}";
			_appBanner.Dock = System.Windows.Forms.DockStyle.Top;
			_appBanner.EnableStateChangeEvent = false;
			_appBanner.FromColor = System.Drawing.Color.RoyalBlue;
			_appBanner.Location = new System.Drawing.Point(0, 49);
			_appBanner.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			_appBanner.MaximumSize = new System.Drawing.Size(9999, 111);
			_appBanner.Name = "_appBanner";
			_appBanner.Size = new System.Drawing.Size(838, 111);
			_appBanner.TabIndex = 6;
			_appBanner.Title = "{ProductName}";
			_appBanner.ToColor = System.Drawing.Color.LightBlue;
			_appBanner.Version = "Version {ProductVersion}";
			// 
			// BSQLMainForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(838, 615);
			Controls.Add(_appBanner);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			Margin = new System.Windows.Forms.Padding(2);
			Name = "BSQLMainForm";
			Text = "BlockchainSQL Management Studio";
			Controls.SetChildIndex(groupBox1, 0);
			Controls.SetChildIndex(groupBox2, 0);
			Controls.SetChildIndex(_appBanner, 0);
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button _installServiceButton;
		private System.Windows.Forms.Button _uninstallServiceButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label7;
		private ApplicationBanner _appBanner;
	}
}

