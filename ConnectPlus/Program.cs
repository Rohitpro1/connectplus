using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ConnectPlus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Global handlers
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            try
            {
                // inside Main()
                Application.Run(new Forms.SplashForm());


            }
            catch (Exception ex)
            {
                Logger.LogFatal("Unhandled exception in Main: " + ex);
                MessageBox.Show("Fatal error: " + ex.Message + "\n\nDetails written to log.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.LogFatal("UI Thread exception: " + e.Exception);
            MessageBox.Show("An unexpected error occurred:\n\n" + e.Exception.Message + "\n\nDetails written to log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Logger.LogFatal("Unhandled domain exception: " + ex);
            MessageBox.Show("A serious error occurred:\n\n" + (ex?.Message ?? "unknown") + "\n\nDetails written to log.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.LogFatal("Unobserved task exception: " + e.Exception);
            e.SetObserved();
        }
    }

    // Simple logger class uses AppConfig.AppDataDir
    public static class Logger
    {
        private static readonly string LogFile = Path.Combine(AppConfig.AppDataDir, "error.log");

        public static void EnsureDirectory()
        {
            try
            {
                Directory.CreateDirectory(AppConfig.AppDataDir);
            }
            catch { }
        }

        public static void Log(string text)
        {
            try
            {
                EnsureDirectory();
                File.AppendAllText(LogFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {text}{Environment.NewLine}");
            }
            catch { }
        }

        public static void LogFatal(string text)
        {
            Log("FATAL: " + text);
        }
    }
}
