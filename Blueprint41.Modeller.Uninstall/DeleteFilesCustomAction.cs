using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blueprint41.Modeller.Uninstall
{
    [RunInstaller(true)]
    public partial class DeleteFilesCustomAction : Installer
    {
        public DeleteFilesCustomAction()
        {
            InitializeComponent();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            try
            {
                // delete any addictional files (or comepletely remove the folder)
                string pathtodelete = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                if (pathtodelete != null && Directory.Exists(pathtodelete))
                {
                    // delete all the file inside this folder except SID.SetupSupport
                    foreach (var file in Directory.GetFiles(pathtodelete))
                    {
                        if (!file.Contains(System.Reflection.Assembly.GetAssembly(typeof(DeleteFilesCustomAction)).GetName().Name))
                            SafeDeleteFile(file);
                    }

                    // delete all directories
                    // optional "Modules" if update
                    foreach (var directory in Directory.GetDirectories(pathtodelete))
                    {
                        if (RegistryHandler.IsInstallUpgrade && new DirectoryInfo(directory).Name == "Modules")
                        {
                            RegistryHandler.IsInstallUpgrade = false;

                            continue;
                        }

                        SafeDeleteDirectory(directory);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private static void SafeDeleteFile(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // ignored
            }
        }

        private static void SafeDeleteDirectory(string directory)
        {
            try
            {
                Directory.Delete(directory, true);
            }
            catch
            {
                // ignored
            }
        }
    }
}
