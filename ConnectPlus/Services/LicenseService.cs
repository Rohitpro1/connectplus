using System;
using System.IO;
using Newtonsoft.Json;

namespace ConnectPlus.Services
{
    public static class LicenseService
    {
        public class ActivationData
        {
            public string LicenseKey { get; set; } = string.Empty;
            public string Hwid { get; set; } = string.Empty;
            public DateTime ActivatedDate { get; set; }
            public bool IsActivated { get; set; }
        }

        /// <summary>
        /// Checks if activation file exists and is valid.
        /// </summary>
        public static bool IsActivated()
        {
            try
            {
                if (!File.Exists(AppConfig.ActivationFilePath))
                    return false;

                string json = File.ReadAllText(AppConfig.ActivationFilePath);
                ActivationData? data = JsonConvert.DeserializeObject<ActivationData>(json);

                return data?.IsActivated == true;
            }
            catch (Exception ex)
            {
                Logger.Log("IsActivated error: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Activates using a local demo key. Creates activation file safely.
        /// </summary>
        public static bool ActivateLocal(string key, string hwid)
        {
            try
            {
                // Only allow demo key
                if (key != "DEMO-KEY-1234")
                    return false;

                // Ensure directory exists
                try
                {
                    Directory.CreateDirectory(AppConfig.AppDataDir);
                }
                catch (Exception dirEx)
                {
                    Logger.LogFatal("Failed to create AppData directory: " + dirEx);
                    throw;
                }

                ActivationData data = new ActivationData
                {
                    LicenseKey = key,
                    Hwid = hwid,
                    ActivatedDate = DateTime.UtcNow,
                    IsActivated = true
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                try
                {
                    File.WriteAllText(AppConfig.ActivationFilePath, json);
                }
                catch (Exception writeEx)
                {
                    Logger.LogFatal("Failed writing activation file: " + writeEx);
                    throw;
                }

                Logger.Log("Activation successful. Saved file: " + AppConfig.ActivationFilePath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogFatal("ActivateLocal error: " + ex);
                return false;
            }
        }

        /// <summary>
        /// Returns full activation data, or null if missing or corrupted.
        /// </summary>
        public static ActivationData? GetActivationData()
        {
            try
            {
                if (!File.Exists(AppConfig.ActivationFilePath))
                    return null;

                string json = File.ReadAllText(AppConfig.ActivationFilePath);
                return JsonConvert.DeserializeObject<ActivationData>(json);
            }
            catch (Exception ex)
            {
                Logger.Log("GetActivationData error: " + ex);
                return null;
            }
        }
    }
}
