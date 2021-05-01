namespace BlockchainSQL.Server {
    partial class ServiceStatusControl {
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
            this._trafficLightLabel = new System.Windows.Forms.Label();
            this._trafficLight = new Sphere10.Framework.Windows.Forms.PanelEx();
            this._serviceButton = new System.Windows.Forms.Button();
            this._serviceDetailLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _trafficLightLabel
            // 
            this._trafficLightLabel.AutoSize = true;
            this._trafficLightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._trafficLightLabel.Location = new System.Drawing.Point(28, 6);
            this._trafficLightLabel.Name = "_trafficLightLabel";
            this._trafficLightLabel.Size = new System.Drawing.Size(78, 13);
            this._trafficLightLabel.TabIndex = 16;
            this._trafficLightLabel.Text = "Not Running";
            // 
            // _trafficLight
            // 
            this._trafficLight.AutoDetectChildStateChanges = false;
            this._trafficLight.BackColor = System.Drawing.Color.Red;
            this._trafficLight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._trafficLight.Location = new System.Drawing.Point(-2, 0);
            this._trafficLight.Name = "_trafficLight";
            this._trafficLight.Size = new System.Drawing.Size(24, 24);
            this._trafficLight.TabIndex = 15;
            // 
            // _serviceButton
            // 
            this._serviceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._serviceButton.Location = new System.Drawing.Point(725, 0);
            this._serviceButton.Margin = new System.Windows.Forms.Padding(2);
            this._serviceButton.Name = "_serviceButton";
            this._serviceButton.Size = new System.Drawing.Size(71, 24);
            this._serviceButton.TabIndex = 14;
            this._serviceButton.Text = "Stop";
            this._serviceButton.UseVisualStyleBackColor = true;
            // 
            // _serviceDetailLabel
            // 
            this._serviceDetailLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._serviceDetailLabel.AutoEllipsis = true;
            this._serviceDetailLabel.Location = new System.Drawing.Point(112, 6);
            this._serviceDetailLabel.Name = "_serviceDetailLabel";
            this._serviceDetailLabel.Size = new System.Drawing.Size(608, 13);
            this._serviceDetailLabel.TabIndex = 17;
            this._serviceDetailLabel.Text = "Service Detail";
            // 
            // ServiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._serviceDetailLabel);
            this.Controls.Add(this._trafficLightLabel);
            this.Controls.Add(this._trafficLight);
            this.Controls.Add(this._serviceButton);
            this.Name = "ServiceStatusControl";
            this.Size = new System.Drawing.Size(796, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _trafficLightLabel;
        private Sphere10.Framework.Windows.Forms.PanelEx _trafficLight;
        private System.Windows.Forms.Button _serviceButton;
        private System.Windows.Forms.Label _serviceDetailLabel;
    }
}
