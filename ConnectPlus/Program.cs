using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using ConnectPlus.Forms;
using ConnectPlus.Services;

namespace ConnectPlus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            // Initialize LibVLC
            try
            {
                Core.Initialize(AppConfig.LibVlcPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to initialize LibVLC. Please check LibVlcPath in AppConfig.cs\n\nError: {ex.Message}",
                    "LibVLC Initialization Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Ensure app data directory exists
            AppConfig.EnsureAppDataDir();

            // Start with splash screen
            Application.Run(new SplashForm());
        }
    }
}

