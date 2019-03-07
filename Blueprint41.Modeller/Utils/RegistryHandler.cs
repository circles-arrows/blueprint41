using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprint41.Modeller
{
    public class RegistryHandler
    {
        private const string APPLICATION_NAME = "Blueprint41 Modeller";
        private const string LAST_OPENED_SUBMODEL = "LastOpenedSubmodel";
        private const string LAST_OPENED_FILE = "LastOpenedFile";

        private static RegistryKey ApplicationRegistryKey
        {
            get
            {
                RegistryKey softwareRegKey = Registry.CurrentUser.OpenSubKey("Software", true);
                return softwareRegKey.CreateSubKey(APPLICATION_NAME, true);
            }
        }

        public static string LastOpenedSubmodel {
            get
            {
                RegistryKey key = ApplicationRegistryKey;
                return key.GetValue(LAST_OPENED_SUBMODEL, string.Empty) as string;
            }
            set
            {
                SaveToRegistry(LAST_OPENED_SUBMODEL, value);
            }
        }

        public static string LastOpenedFile
        {
            get
            {
                RegistryKey key = ApplicationRegistryKey;
                return key.GetValue(LAST_OPENED_FILE, string.Empty) as string;
            }
            set
            {
                SaveToRegistry(LAST_OPENED_FILE, value);
            }
        }
        
        private static void SaveToRegistry(string name, string value)
        {
            RegistryKey key = ApplicationRegistryKey;
            key.SetValue(name, value);
            key.Close();
        }
    }
}
