using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windows_proxy_management
{
    static public class Reg
    {
        public static string Name = "Proxy Management";

        public static void Register()
        {
            string MyPath = Util.GetCurrentPath();

            RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key != null)
            {
                key.SetValue("Proxy Management", "\"" + MyPath + "\"");
                key.Close();
            }
        }

        public static bool Exist()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key == null)
                return false;

            try
            {
                object? value = key.GetValue("Proxy Management");
                if (value is string)
                    return true;
            }
            finally
            {
                key.Close();
            }

            return false;
        }



        public static string? Get()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if (key == null)
                return null;

            try
            {
                object? value = key.GetValue("Proxy Management");
                if (value is string)
                    return value as string;
            }
            finally
            {
                key.Close();
            }

            return null;
        }

        public static void UnRegister()
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key != null)
            {
                if (key.GetValue("ProxyManagement") != null)
                {
                    key.DeleteValue("Proxy Management");
                }
                key.Close();
            }
        }




    }
}
