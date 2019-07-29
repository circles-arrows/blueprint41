using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Uninstall
{
    public class RegistryHandler
    {
        private const string APPLICATION_NAME = "Blueprint41 Modeller";
        private const string IS_INSTALL_UPGRADE = "IsInstallUpgrade";

        private static RegistryKey ApplicationRegistryKey
        {
            get
            {
                RegistryKey softwareRegKey = Registry.CurrentUser.OpenSubKey("Software", true);
                return softwareRegKey.CreateSubKey(APPLICATION_NAME, true);
            }
        }

        public static bool IsInstallUpgrade
        {
            get
            {
                RegistryKey key = ApplicationRegistryKey;

                bool.TryParse(key.GetValue(IS_INSTALL_UPGRADE, "false").ToString(), out bool isUpgrade);

                return isUpgrade;
            }
            set
            {
                SaveToRegistry(IS_INSTALL_UPGRADE, value);
            }
        }

        private static void SaveToRegistry(string name, object value)
        {
            RegistryKey key = ApplicationRegistryKey;
            key.SetValue(name, value);
            key.Close();
        }
    }
}
