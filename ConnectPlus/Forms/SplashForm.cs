using System;
using System.Drawing;
using System.Windows.Forms;
using ConnectPlus.Services;

namespace ConnectPlus.Forms
{
    public partial class SplashForm : Form
    {
        // FIX: Explicitly use System.Windows.Forms.Timer
        private System.Windows.Forms.Timer fadeTimer;

        private double opacityIncrement = 0.05;
        private int fadeDuration = 1500; // milliseconds
        private DateTime startTime;

        public SplashForm()
        {
            InitializeComponent();
            InitializeFadeAnimation();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(13, 15, 26); // #0D0F1A dark background
            this.DoubleBuffered = true; // reduce flicker
        }

        private void InitializeFadeAnimation()
        {
            this.Opacity = 0.0;
            startTime = DateTime.Now;

            // FIX: Use explicit type
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 16; // ~60 FPS
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            double elapsed = (DateTime.Now - startTime).TotalMilliseconds;

            // Fade in
            if (elapsed < fadeDuration)
            {
                this.Opacity = Math.Min(1.0, this.Opacity + opacityIncrement);
            }
            else
            {
                // Fade complete, navigate to next form
                fadeTimer.Stop();
                fadeTimer.Dispose();

                // Check activation status
                if (LicenseService.IsActivated())
                {
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    this.Hide();
                    ActivationForm activationForm = new ActivationForm();
                    activationForm.Show();
                }

                // Do NOT close immediately — let new form take control
                this.BeginInvoke(new Action(() => this.Close()));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Neon glow ellipse behind CONNECT+
            using (Pen glowPen = new Pen(Color.FromArgb(100, 123, 47, 247), 20))
            {
                glowPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                g.DrawEllipse(glowPen, this.Width / 2 - 100, this.Height / 2 - 50, 200, 100);
            }

            // Draw CONNECT+ neon text
            using (Font font = new Font("Segoe UI", 36, FontStyle.Bold))
            {
                // Glow effect layers
                for (int i = 0; i < 5; i++)
                {
                    using (SolidBrush glowBrush =
                        new SolidBrush(Color.FromArgb(50 - i * 10, 123, 47, 247)))
                    {
                        g.DrawString("CONNECT+", font, glowBrush,
                            (this.Width / 2 - 150) + i,
                            (this.Height / 2 - 20) + i);
                    }
                }

                // Main text
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(123, 47, 247)))
                {
                    g.DrawString("CONNECT+", font, brush,
                        this.Width / 2 - 150, this.Height / 2 - 20);
                }
            }
        }
    }
}
