using Blueprint41.Licensing.Connector;
using System;
using System.IO;
using System.Net;
using System.Security;
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

            DownloadConnectorClient();
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
                ExitThread();
        }

        private void ShowMainForm()
        {
            MainForm main = new MainForm();
            main.FormClosed += OnFormClosed;
            main.Show();
        }

        private async void VerifyLicense()
        {
            ConnectorLoader connectorClientLoader = ConnectorLoader.GetConnectorClient();

            try
            {
                // wrapping it to try/catch will catch any exception from the verifylicense method
                if (connectorClientLoader != null)
                    await connectorClientLoader.VerifyLicense();
            }
            catch (SecurityException ex)
            {
                MessageBox.Show(ex.Message, "License", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            loader.Close();
            ShowMainForm();
        }

        private async void DownloadConnectorClient()
        {
            // Checking the server first eliminates the connector client dll to be overriden (if downloaded) by the code below 
            // when the server is offline.

            bool isOnline = await UriConfig.CheckServerIsOnline();

            if (isOnline == false)
            {
                VerifyLicense();
                return;
            }

            using (WebClient client = new WebClient())
            {
                string connectorFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Blueprint41.Licensing.Client.dll");
                client.DownloadFileAsync(UriConfig.ConnectorUri, connectorFileName);
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
            }
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            VerifyLicense();
        }
    }
}
