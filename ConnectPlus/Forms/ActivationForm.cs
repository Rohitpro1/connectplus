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

        private void btnActivate_Click(object? sender, EventArgs e)
        {
            try
            {
                string key = txtLicenseKey.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(key))
                {
                    MessageBox.Show("Please enter a license key.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ensure app data dir exists before activation
                try { System.IO.Directory.CreateDirectory(AppConfig.AppDataDir); } catch { }

                // Call your existing license service (wrap any exceptions)
                var hwid = ConnectPlus.Services.HwidService.GetHwid();
                var ok = ConnectPlus.Services.LicenseService.ActivateLocal(key, hwid);

                if (ok)
                {
                    MessageBox.Show("Activation successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.Log("Activated with key: " + key + " HWID: " + hwid);

                    // Open MainForm
                    this.Hide();
                    var main = new Forms.MainForm();
                    main.Show();
                    this.BeginInvoke(new Action(() => this.Close()));
                }
                else
                {
                    MessageBox.Show("Activation failed. Check the key and try again.", "Activation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Logger.Log("Activation failed for key: " + key + " HWID: " + hwid);
                }
            }
            catch (Exception ex)
            {
                Logger.LogFatal("Activation error: " + ex);
                MessageBox.Show("Error during activation:\n\n" + ex.Message + "\n\nSee log: " + Path.Combine(AppConfig.AppDataDir, "error.log"),
                    "Activation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}