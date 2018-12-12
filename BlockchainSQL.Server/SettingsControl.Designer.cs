namespace BlockchainSQL.Server {
    partial class SettingsControl {
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._nodePollRateIntBox = new Sphere10.Windows.WinForms.IntBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._nodeCustomPortIntBox = new Sphere10.Windows.WinForms.IntBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this._node1TextBox = new Sphere10.Windows.WinForms.TextBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this._maxMemoryIntBox = new Sphere10.Windows.WinForms.IntBox();
            this.label4 = new System.Windows.Forms.Label();
            this._optionsListBox = new Sphere10.Windows.WinForms.CheckedListBoxEx();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this._nodePollRateIntBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this._nodeCustomPortIntBox);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this._node1TextBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(627, 104);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Network Peer Options";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.Location = new System.Drawing.Point(305, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "How often to poll a node for block updates";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Node Poll Rate (sec)";
            // 
            // _nodePollRateIntBox
            // 
            this._nodePollRateIntBox.Location = new System.Drawing.Point(117, 71);
            this._nodePollRateIntBox.Name = "_nodePollRateIntBox";
            this._nodePollRateIntBox.PlaceHolderText = "Enter IP address";
            this._nodePollRateIntBox.Size = new System.Drawing.Size(182, 20);
            this._nodePollRateIntBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(305, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Custom port of your trusted Bitcoin node (leave blank for default)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Trusted Node Port";
            // 
            // _nodeCustomPortIntBox
            // 
            this._nodeCustomPortIntBox.Location = new System.Drawing.Point(116, 45);
            this._nodeCustomPortIntBox.Name = "_nodeCustomPortIntBox";
            this._nodeCustomPortIntBox.PlaceHolderText = "Enter IP address";
            this._nodeCustomPortIntBox.Size = new System.Drawing.Size(183, 20);
            this._nodeCustomPortIntBox.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoEllipsis = true;
            this.label12.Location = new System.Drawing.Point(305, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(282, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "IP address of your trusted Bitcoin node";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(26, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Trusted Node IP";
            // 
            // _node1TextBox
            // 
            this._node1TextBox.Location = new System.Drawing.Point(117, 19);
            this._node1TextBox.Name = "_node1TextBox";
            this._node1TextBox.PlaceHolderText = "Enter IP address";
            this._node1TextBox.Size = new System.Drawing.Size(182, 20);
            this._node1TextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this._maxMemoryIntBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(0, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 58);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Misc Options";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoEllipsis = true;
            this.label8.Location = new System.Drawing.Point(305, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(282, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "The maximum memory to allocate in processing buffers";
            // 
            // _maxMemoryIntBox
            // 
            this._maxMemoryIntBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._maxMemoryIntBox.Location = new System.Drawing.Point(113, 25);
            this._maxMemoryIntBox.Name = "_maxMemoryIntBox";
            this._maxMemoryIntBox.PlaceHolderText = "Enter number of megabytes";
            this._maxMemoryIntBox.Size = new System.Drawing.Size(186, 20);
            this._maxMemoryIntBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Max Memory (MB)";
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
            "Save transaction script data (useful for layered protocols but results in signifi" +
                "cantly larger database)"});
            this._optionsListBox.Location = new System.Drawing.Point(0, 3);
            this._optionsListBox.Name = "_optionsListBox";
            this._optionsListBox.Size = new System.Drawing.Size(627, 15);
            this._optionsListBox.TabIndex = 11;
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._optionsListBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(627, 195);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Sphere10.Windows.WinForms.IntBox _nodePollRateIntBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Sphere10.Windows.WinForms.IntBox _nodeCustomPortIntBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private Sphere10.Windows.WinForms.TextBoxEx _node1TextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private Sphere10.Windows.WinForms.IntBox _maxMemoryIntBox;
        private System.Windows.Forms.Label label4;
        private Sphere10.Windows.WinForms.CheckedListBoxEx _optionsListBox;
    }
}
