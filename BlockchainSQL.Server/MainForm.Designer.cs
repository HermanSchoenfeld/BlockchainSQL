using Sphere10.Framework.Windows.Forms;

namespace BlockchainSQL.Server {
    partial class MainForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._uninstallServiceButton = new System.Windows.Forms.Button();
			this._installServiceButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._appBanner = new Sphere10.Framework.Windows.Forms.ApplicationBanner();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this._uninstallServiceButton);
			this.groupBox1.Controls.Add(this._installServiceButton);
			this.groupBox1.Location = new System.Drawing.Point(13, 117);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
			this.groupBox1.Size = new System.Drawing.Size(799, 181);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Service Installer";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(208, 101);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(381, 62);
			this.label5.TabIndex = 9;
			this.label5.Text = "Remove a previously installed BlockchainSQL Windows Service.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(206, 34);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(554, 62);
			this.label4.TabIndex = 8;
			this.label4.Text = "Install a BlockchainSQL Windows Service and optional web interface.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _uninstallServiceButton
			// 
			this._uninstallServiceButton.Location = new System.Drawing.Point(9, 101);
			this._uninstallServiceButton.Margin = new System.Windows.Forms.Padding(2);
			this._uninstallServiceButton.Name = "_uninstallServiceButton";
			this._uninstallServiceButton.Size = new System.Drawing.Size(194, 62);
			this._uninstallServiceButton.TabIndex = 5;
			this._uninstallServiceButton.Text = "Uninstall Service";
			this._uninstallServiceButton.UseVisualStyleBackColor = true;
			this._uninstallServiceButton.Click += new System.EventHandler(this._uninstallServiceButton_Click);
			// 
			// _installServiceButton
			// 
			this._installServiceButton.Location = new System.Drawing.Point(9, 34);
			this._installServiceButton.Margin = new System.Windows.Forms.Padding(2);
			this._installServiceButton.Name = "_installServiceButton";
			this._installServiceButton.Size = new System.Drawing.Size(194, 62);
			this._installServiceButton.TabIndex = 4;
			this._installServiceButton.Text = "Install Service";
			this._installServiceButton.UseVisualStyleBackColor = true;
			this._installServiceButton.Click += new System.EventHandler(this._installServiceButton_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(208, 176);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.MaximumSize = new System.Drawing.Size(575, 62);
			this.label3.MinimumSize = new System.Drawing.Size(575, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(575, 62);
			this.label3.TabIndex = 7;
			this.label3.Text = "Fill database with blockchain data from a node.";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(208, 110);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(575, 62);
			this.label2.TabIndex = 3;
			this.label2.Text = "Import Bitcoin (BTC) blockchain data from Bitcoin Core BLK data files.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(206, 44);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(575, 62);
			this.label1.TabIndex = 1;
			this.label1.Text = "Generate or repair an empty BlockchainSQL database.";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _appBanner
			// 
			this._appBanner.CompanyName = "{CompanyName}";
			this._appBanner.Dock = System.Windows.Forms.DockStyle.Top;
			this._appBanner.EnableStateChangeEvent = false;
			this._appBanner.FromColor = System.Drawing.Color.RoyalBlue;
			this._appBanner.Location = new System.Drawing.Point(0, 0);
			this._appBanner.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
			this._appBanner.Name = "_appBanner";
			this._appBanner.Size = new System.Drawing.Size(825, 111);
			this._appBanner.TabIndex = 1;
			this._appBanner.Title = "{ProductName}";
			this._appBanner.ToColor = System.Drawing.Color.LightBlue;
			this._appBanner.Version = "Version {ProductVersion}";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(13, 303);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(800, 240);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Tools";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(10, 97);
			this.button3.Margin = new System.Windows.Forms.Padding(2);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(194, 62);
			this.button3.TabIndex = 2;
			this.button3.Text = "Block File Scanner";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this._blockFileScannerButton_Click);
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.Location = new System.Drawing.Point(206, 97);
			this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(461, 62);
			this.label9.TabIndex = 3;
			this.label9.Text = "Fill database with blockchain via rapid import of Bitcoin Core node BLK data file" +
    "s. ";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(9, 163);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(194, 62);
			this.button2.TabIndex = 6;
			this.button2.Text = "Network Scanner";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this._networkButton_Click);
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.Location = new System.Drawing.Point(206, 163);
			this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(299, 62);
			this.label8.TabIndex = 7;
			this.label8.Text = "Fill database with blockchain data from a node.";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(9, 31);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(194, 62);
			this.button1.TabIndex = 0;
			this.button1.Text = "Generate Database";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this._generateDatabaseButton_Click);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.Location = new System.Drawing.Point(207, 31);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(367, 62);
			this.label7.TabIndex = 1;
			this.label7.Text = "Generate an empty or repair an existing BlockchainSQL database.";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(825, 555);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this._appBanner);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "MainForm";
			this.Text = "BlockchainSQL Management Studio";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private ApplicationBanner _appBanner;
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
	}
}

