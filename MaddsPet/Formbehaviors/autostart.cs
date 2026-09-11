using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace MaddsPet.Formbehaviors
{
    internal class autostart
    {
        private const string AppName = "MaddsPet";
        private const string RegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

        public static void Set(bool enabled)
        {
            using RegistryKey key =
                Registry.CurrentUser.OpenSubKey(RegistryPath, true);

            if (key == null)
                return;

            if (enabled)
            {
                key.SetValue(
                    AppName,
                    Application.ExecutablePath
                );
            }
            else
            {
                key.DeleteValue(AppName, false);
            }
        }

        public static bool IsEnabled()
        {
            using RegistryKey key =
                Registry.CurrentUser.OpenSubKey(RegistryPath, false);

            if (key == null)
                return false;

            return key.GetValue(AppName) != null;
        }
    }
}