using System;
using System.IO;
using Newtonsoft.Json;
using ConnectPlus.Services;

namespace ConnectPlus.Services
{
    public class LicenseService
    {
        public class ActivationData
        {
            public string LicenseKey { get; set; } = string.Empty;
            public string Hwid { get; set; } = string.Empty;
            public DateTime ActivatedDate { get; set; }
            public bool IsActivated { get; set; }
        }

        public static bool IsActivated()
        {
            try
            {
                if (!File.Exists(AppConfig.ActivationFilePath))
                {
                    return false;
                }

                string json = File.ReadAllText(AppConfig.ActivationFilePath);
                ActivationData data = JsonConvert.DeserializeObject<ActivationData>(json);

                return data != null && data.IsActivated;
            }
            catch
            {
                return false;
            }
        }

        public static bool ActivateLocal(string key, string hwid)
        {
            try
            {
                // Demo key validation
                if (key == "DEMO-KEY-1234")
                {
                    ActivationData data = new ActivationData
                    {
                        LicenseKey = key,
                        Hwid = hwid,
                        ActivatedDate = DateTime.Now,
                        IsActivated = true
                    };

                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                    File.WriteAllText(AppConfig.ActivationFilePath, json);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Activation error: {ex.Message}");
                return false;
            }
        }

        public static ActivationData GetActivationData()
        {
            try
            {
                if (!File.Exists(AppConfig.ActivationFilePath))
                {
                    return null;
                }

                string json = File.ReadAllText(AppConfig.ActivationFilePath);
                return JsonConvert.DeserializeObject<ActivationData>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}

