using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ConnectPlus.Services;

namespace ConnectPlus.Forms
{
    public partial class SplashForm : Form
    {
        private System.Windows.Forms.Timer? fadeTimer;
        private DateTime startTime;
        private int fadeDurationMs = 1500; // fade duration (ms)

        public SplashForm()
        {
            try
            {
                InitializeComponent();

                // Defensive form settings
                this.FormBorderStyle = FormBorderStyle.None;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.BackColor = Color.FromArgb(13, 15, 26); // #0d0f1a
                this.DoubleBuffered = true;

                // Ensure AppData dir exists for logging
                try { Directory.CreateDirectory(AppConfig.AppDataDir); } catch { }

                InitializeFadeAnimation();
            }
            catch (Exception ex)
            {
                // If anything fails here, log and show a messagebox then open ActivationForm to keep app alive
                Logger.LogFatal("SplashForm ctor error: " + ex);
                MessageBox.Show("Error initializing splash. Opening activation window.\n\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SafeOpenActivation();
            }
        }

        private void InitializeFadeAnimation()
        {
            // start invisible
            this.Opacity = 0.0;
            startTime = DateTime.Now;

            // Create timer safely
            fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 16; // ~60 FPS
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();
        }

        private void FadeTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                if (fadeTimer == null) return;

                double elapsed = (DateTime.Now - startTime).TotalMilliseconds;

                // Fade in first half, fade out second half
                if (elapsed < fadeDurationMs)
                {
                    // linear fade-in
                    double progress = elapsed / fadeDurationMs; // 0..1
                    this.Opacity = Math.Min(1.0, progress);
                }
                else if (elapsed < fadeDurationMs * 2)
                {
                    double progress = (elapsed - fadeDurationMs) / fadeDurationMs;
                    this.Opacity = Math.Max(0.0, 1.0 - progress);
                }
                else
                {
                    // stop and navigate
                    fadeTimer.Stop();
                    fadeTimer.Tick -= FadeTimer_Tick;
                    fadeTimer.Dispose();
                    fadeTimer = null;

                    SafeNavigateNext();
                }
            }
            catch (Exception ex)
            {
                // Log and move on to next screen so app doesn't exit
                Logger.LogFatal("FadeTimer_Tick error: " + ex);
                SafeNavigateNext();
            }
        }

        private void SafeNavigateNext()
        {
            try
            {
                bool activated = LicenseService.IsActivated();

                Form nextForm;

                if (activated)
                {
                    nextForm = new MainForm();
                }
                else
                {
                    nextForm = new ActivationForm();
                }

                nextForm.Show();
                this.Hide();  // NEVER CLOSE HERE
            }
            catch (Exception ex)
            {
                Logger.LogFatal("SafeNavigateNext error: " + ex);
                SafeOpenActivation();
            }
        }


        private void SafeOpenActivation()
        {
            try
            {
                var act = new ActivationForm();
                act.Show();
                this.Hide();   // DO NOT CLOSE HERE
            }
            catch (Exception ex)
            {
                Logger.LogFatal("SafeOpenActivation error: " + ex);
                MessageBox.Show("Critical UI error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Use a defensive OnPaint that never throws
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);

                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int w = Math.Max(1, this.ClientSize.Width);
                int h = Math.Max(1, this.ClientSize.Height);

                // Calculate centered rectangle safely
                int rectW = Math.Min(400, w - 40);
                int rectH = Math.Min(120, h - 40);
                int centerX = w / 2;
                int centerY = h / 2;

                // Neon glow ellipse behind text (safe sizes)
                using (var glowPen = new Pen(Color.FromArgb(100, 123, 47, 247), Math.Max(2, rectH / 8)))
                {
                    glowPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    var rx = Math.Max(10, centerX - rectW / 2);
                    var ry = Math.Max(10, centerY - rectH / 2);
                    g.DrawEllipse(glowPen, rx, ry, rectW, rectH);
                }

                // Draw CONNECT+ text with layered glow
                using (var font = new Font("Segoe UI", Math.Max(18, rectH / 3), FontStyle.Bold))
                {
                    string title = "CONNECT+";
                    // Glow layers
                    for (int i = 0; i < 4; i++)
                    {
                        var alpha = Math.Max(5, 50 - i * 10);
                        using (var glowBrush = new SolidBrush(Color.FromArgb(alpha, 123, 47, 247)))
                        {
                            g.DrawString(title, font, glowBrush, centerX - rectW / 2 + i, centerY - rectH / 4 + i);
                        }
                    }

                    // Main text
                    using (var brush = new SolidBrush(Color.FromArgb(123, 47, 247)))
                    {
                        g.DrawString(title, font, brush, centerX - rectW / 2, centerY - rectH / 4);
                    }
                }
            }
            catch (Exception ex)
            {
                // If painting fails, log and swallow — do not crash the app
                Logger.Log("Splash OnPaint error: " + ex);
            }
        }
    }
}
