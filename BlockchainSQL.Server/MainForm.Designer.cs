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
            this.label6 = new System.Windows.Forms.Label();
            this._databaseDiagnosticButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._networkButton = new System.Windows.Forms.Button();
            this._uninstallServiceButton = new System.Windows.Forms.Button();
            this._installServiceButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._blockFileScannerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._generateDatabaseButton = new System.Windows.Forms.Button();
            this._appBanner = new Sphere10.Framework.Windows.Forms.ApplicationBanner();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this._databaseDiagnosticButton);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._networkButton);
            this.groupBox1.Controls.Add(this._uninstallServiceButton);
            this.groupBox1.Controls.Add(this._installServiceButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this._blockFileScannerButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._generateDatabaseButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 117);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(855, 464);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Utilities";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(208, 247);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(631, 62);
            this.label6.TabIndex = 13;
            this.label6.Text = "View BlockchainSQL server logs and change settings";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _databaseDiagnosticButton
            // 
            this._databaseDiagnosticButton.Location = new System.Drawing.Point(9, 247);
            this._databaseDiagnosticButton.Margin = new System.Windows.Forms.Padding(2);
            this._databaseDiagnosticButton.Name = "_databaseDiagnosticButton";
            this._databaseDiagnosticButton.Size = new System.Drawing.Size(194, 62);
            this._databaseDiagnosticButton.TabIndex = 12;
            this._databaseDiagnosticButton.Text = "Server Diagnostic";
            this._databaseDiagnosticButton.UseVisualStyleBackColor = true;
            this._databaseDiagnosticButton.Click += new System.EventHandler(this._databaseDiagnosticButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(208, 180);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(631, 62);
            this.label5.TabIndex = 9;
            this.label5.Text = "Remove a previously installed BlockchainSQL Service.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Location = new System.Drawing.Point(206, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(631, 62);
            this.label4.TabIndex = 8;
            this.label4.Text = "Install a BlockchainSQL service that will automatically download blockchain data " +
    "into your databsae.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(208, 384);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(631, 62);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fill database with blockchain data from a node.";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _networkButton
            // 
            this._networkButton.Location = new System.Drawing.Point(9, 381);
            this._networkButton.Margin = new System.Windows.Forms.Padding(2);
            this._networkButton.Name = "_networkButton";
            this._networkButton.Size = new System.Drawing.Size(194, 62);
            this._networkButton.TabIndex = 6;
            this._networkButton.Text = "Network Scanner";
            this._networkButton.UseVisualStyleBackColor = true;
            this._networkButton.Click += new System.EventHandler(this._networkButton_Click);
            // 
            // _uninstallServiceButton
            // 
            this._uninstallServiceButton.Location = new System.Drawing.Point(9, 180);
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
            this._installServiceButton.Location = new System.Drawing.Point(9, 113);
            this._installServiceButton.Margin = new System.Windows.Forms.Padding(2);
            this._installServiceButton.Name = "_installServiceButton";
            this._installServiceButton.Size = new System.Drawing.Size(194, 62);
            this._installServiceButton.TabIndex = 4;
            this._installServiceButton.Text = "Install Service";
            this._installServiceButton.UseVisualStyleBackColor = true;
            this._installServiceButton.Click += new System.EventHandler(this._installServiceButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(206, 320);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(631, 62);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fill database with blockchain data from Bitcoin Core BLK files (very fast)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _blockFileScannerButton
            // 
            this._blockFileScannerButton.Location = new System.Drawing.Point(9, 314);
            this._blockFileScannerButton.Margin = new System.Windows.Forms.Padding(2);
            this._blockFileScannerButton.Name = "_blockFileScannerButton";
            this._blockFileScannerButton.Size = new System.Drawing.Size(194, 62);
            this._blockFileScannerButton.TabIndex = 2;
            this._blockFileScannerButton.Text = "Block File Scanner";
            this._blockFileScannerButton.UseVisualStyleBackColor = true;
            this._blockFileScannerButton.Click += new System.EventHandler(this._blockFileScannerButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(206, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(631, 62);
            this.label1.TabIndex = 1;
            this.label1.Text = "Generate an empty BlockchainSQL database.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _generateDatabaseButton
            // 
            this._generateDatabaseButton.Location = new System.Drawing.Point(9, 46);
            this._generateDatabaseButton.Margin = new System.Windows.Forms.Padding(2);
            this._generateDatabaseButton.Name = "_generateDatabaseButton";
            this._generateDatabaseButton.Size = new System.Drawing.Size(194, 62);
            this._generateDatabaseButton.TabIndex = 0;
            this._generateDatabaseButton.Text = "Generate Database";
            this._generateDatabaseButton.UseVisualStyleBackColor = true;
            this._generateDatabaseButton.Click += new System.EventHandler(this._generateDatabaseButton_Click);
            // 
            // _appBanner
            // 
            this._appBanner.AutoDetectChildStateChanges = false;
            this._appBanner.CompanyName = "{CompanyName}";
            this._appBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this._appBanner.FromColor = System.Drawing.Color.RoyalBlue;
            this._appBanner.Location = new System.Drawing.Point(0, 0);
            this._appBanner.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this._appBanner.Name = "_appBanner";
            this._appBanner.Size = new System.Drawing.Size(881, 111);
            this._appBanner.TabIndex = 1;
            this._appBanner.Title = "{ProductName}";
            this._appBanner.ToColor = System.Drawing.Color.LightBlue;
            this._appBanner.Version = "Version {ProductVersion}";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 593);
            this.Controls.Add(this._appBanner);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "BlockchainSQL Management Studio";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _generateDatabaseButton;
        private ApplicationBanner _appBanner;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button _blockFileScannerButton;
        private System.Windows.Forms.Button _installServiceButton;
        private System.Windows.Forms.Button _uninstallServiceButton;
        private System.Windows.Forms.Button _networkButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button _databaseDiagnosticButton;
    }
}

