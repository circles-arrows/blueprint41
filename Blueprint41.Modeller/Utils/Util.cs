using Blueprint41.Modeller.Utils;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

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

        internal static Task<ModellerVersion> CheckForUpdates()
        {
            return Task.Run(() =>
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load("https://www.blueprint41.com/download/updater.xml");

                    XmlNode modellerNode = doc.DocumentElement.SelectSingleNode("/modeller");
                    string version = modellerNode.SelectSingleNode("version").InnerText;
                    string downloadUrl = modellerNode.SelectSingleNode("downloadUrl").InnerText;

                    string[] versions = version.Split('.');

                    long.TryParse(versions[0], out long major);
                    long.TryParse(versions[1], out long minor);
                    long.TryParse(versions[2], out long patch);

                    ModellerVersion updatedVersion = new ModellerVersion(major, minor, patch, downloadUrl);

                    return updatedVersion;
                }
                catch (XmlException ex)
                {
                    return ModellerVersion.Empty;
                }
            });
        }

        internal static void Goto(string url)
        {
            Process.Start(new ProcessStartInfo(url));
        }
    }
}
