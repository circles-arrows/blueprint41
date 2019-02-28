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
    public partial class DeleteFilesCustomAction : System.Configuration.Install.Installer
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
                // MessageBox.Show("Deleting: " + pathtodelete);
                if (pathtodelete != null && Directory.Exists(pathtodelete))
                {
                    // delete all the file inside this folder except SID.SetupSupport
                    foreach (var file in Directory.GetFiles(pathtodelete))
                    {
                        // MessageBox.Show(file);
                        if (!file.Contains(System.Reflection.Assembly.GetAssembly(typeof(DeleteFilesCustomAction)).GetName().Name))
                            SafeDeleteFile(file);
                    }
                    foreach (var directory in Directory.GetDirectories(pathtodelete))
                        SafeDeleteDirectory(directory);
                }
            }
            catch { }
        }

        private static void SafeDeleteFile(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
            }
        }

        private static void SafeDeleteDirectory(string directory)
        {
            try
            {
                Directory.Delete(directory, true);
            }
            catch (Exception)
            {
            }
        }
    }
}
