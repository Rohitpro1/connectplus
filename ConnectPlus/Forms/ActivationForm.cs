using System;
using System.Drawing;
using System.Windows.Forms;
using ConnectPlus.Services;

namespace ConnectPlus.Forms
{
    public partial class ActivationForm : Form
    {
        public ActivationForm()
        {
            InitializeComponent();
            this.Paint += ActivationForm_Paint;
        }

        private void ActivationForm_Paint(object sender, PaintEventArgs e)
        {
            // Draw neon border
            using (Pen borderPen = new Pen(Color.FromArgb(123, 47, 247), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            string key = txtLicenseKey.Text.Trim();

            if (string.IsNullOrEmpty(key))
            {
                lblStatus.Text = "Please enter a license key";
                lblStatus.ForeColor = Color.Red;
                return;
            }

            string hwid = HwidService.GetHwid();
            bool activated = LicenseService.ActivateLocal(key, hwid);

            if (activated)
            {
                lblStatus.Text = "Activation successful!";
                lblStatus.ForeColor = Color.FromArgb(0, 234, 255);

                // Close and open MainForm
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Close();
            }
            else
            {
                lblStatus.Text = "Invalid license key. Use: DEMO-KEY-1234";
                lblStatus.ForeColor = Color.Red;
            }
        }
    }
}

