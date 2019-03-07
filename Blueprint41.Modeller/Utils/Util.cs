using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blueprint41.Modeller
{
    internal class Util
    {
        internal static string DefaultFilePath
        {
            get
            {
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Blueprint41");

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }
    }
}
