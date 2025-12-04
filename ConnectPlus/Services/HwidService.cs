using System;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace ConnectPlus.Services
{
    public class HwidService
    {
        public static string GetHwid()
        {
            try
            {
                string cpuId = GetCpuId();
                string biosId = GetBiosId();
                string macAddress = GetMacAddress();

                string combined = $"{cpuId}-{biosId}-{macAddress}";

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
                }
            }
            catch (Exception ex)
            {
                // Fallback to a simple hash if WMI fails
                string fallback = Environment.MachineName + Environment.UserName;
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(fallback));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
                }
            }
        }

        private static string GetCpuId()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["ProcessorId"]?.ToString() ?? "UNKNOWN";
                    }
                }
            }
            catch
            {
                return "UNKNOWN";
            }
            return "UNKNOWN";
        }

        private static string GetBiosId()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        return obj["SerialNumber"]?.ToString() ?? "UNKNOWN";
                    }
                }
            }
            catch
            {
                return "UNKNOWN";
            }
            return "UNKNOWN";
        }

        private static string GetMacAddress()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT MACAddress FROM Win32_NetworkAdapter WHERE MACAddress IS NOT NULL"))
                {
                    var macs = searcher.Get()
                        .Cast<ManagementObject>()
                        .Select(obj => obj["MACAddress"]?.ToString())
                        .Where(mac => !string.IsNullOrEmpty(mac))
                        .ToList();

                    return string.Join("-", macs);
                }
            }
            catch
            {
                return "UNKNOWN";
            }
        }
    }
}

