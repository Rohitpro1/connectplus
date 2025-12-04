namespace ConnectPlus.Forms
{
    partial class ActivationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblKeyLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.btnActivate = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblKeyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.BackColor = System.Drawing.Color.FromArgb(20, 22, 35);
            this.txtLicenseKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicenseKey.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLicenseKey.ForeColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.txtLicenseKey.Location = new System.Drawing.Point(50, 120);
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.Size = new System.Drawing.Size(400, 29);
            this.txtLicenseKey.TabIndex = 0;
            // 
            // btnActivate
            // 
            this.btnActivate.BackColor = System.Drawing.Color.FromArgb(123, 47, 247);
            this.btnActivate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.btnActivate.FlatAppearance.BorderSize = 2;
            this.btnActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnActivate.ForeColor = System.Drawing.Color.White;
            this.btnActivate.Location = new System.Drawing.Point(50, 180);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(400, 45);
            this.btnActivate.TabIndex = 1;
            this.btnActivate.Text = "ACTIVATE";
            this.btnActivate.UseVisualStyleBackColor = false;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(123, 47, 247);
            this.lblTitle.Location = new System.Drawing.Point(50, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(320, 45);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Connect+ Activation";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.lblStatus.Location = new System.Drawing.Point(50, 250);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 19);
            this.lblStatus.TabIndex = 3;
            // 
            // lblKeyLabel
            // 
            this.lblKeyLabel.AutoSize = true;
            this.lblKeyLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblKeyLabel.ForeColor = System.Drawing.Color.FromArgb(0, 234, 255);
            this.lblKeyLabel.Location = new System.Drawing.Point(50, 95);
            this.lblKeyLabel.Name = "lblKeyLabel";
            this.lblKeyLabel.Size = new System.Drawing.Size(78, 19);
            this.lblKeyLabel.TabIndex = 4;
            this.lblKeyLabel.Text = "License Key:";
            // 
            // ActivationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(13, 15, 26);
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.lblKeyLabel);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txtLicenseKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActivationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect+ Activation";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

