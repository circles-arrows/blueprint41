using Blueprint41.Licensing.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public class ModellerApplicationContext : ApplicationContext
    {
        Loader loader;

        public ModellerApplicationContext()
        {
            loader = new Loader();
            loader.Show();

            string connectorFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Blueprint41.Licensing.Client.dll");
            using (WebClient client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("http://localhost:14564/api/License/Connector"), connectorFileName);
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
            }
        }

        private async void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            await ConnectorLoader.GetConnectorClient().VerifyLicense();

            loader.Close();

            MainForm main = new MainForm();
            main.FormClosed += OnFormClosed;
            main.Show();
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                ExitThread();
        }
    }
}
