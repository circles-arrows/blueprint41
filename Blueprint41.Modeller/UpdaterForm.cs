using Blueprint41.Modeller.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public partial class UpdaterForm : Form
    {
        public UpdaterForm()
        {
            InitializeComponent();
            Load += UpdaterForm_Load;
        }

        private async void UpdaterForm_Load(object sender, EventArgs e)
        {
            await CheckForUpdates();
        }

        ModellerVersion version = ModellerVersion.Empty;
        private async Task CheckForUpdates()
        {
            tslblStatus.Text = "Checking for updates....";
            ShowHideControls(false);

            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.ProgressBar.Show();

            version = await Util.CheckForUpdates();

            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = 0;
            progressBar.ProgressBar.Hide();

            bool hasUpdates = version.IsUpdatedVersion();
            tslblStatus.Text = hasUpdates ? "Update available" : "No update available";

            progressBar.ProgressBar.Hide();

            if (hasUpdates)
            {
                await CheckFileSize(version.DownloadUrl);
            }

            lblVersion.Text = version.ToString();
            ShowHideControls(hasUpdates);
        }

        private void ShowHideControls(bool show)
        {
            btnUpdate.Enabled = show;

            if (show)
            {
                lblVersionLabel.Show();
                lblVersion.Show();
                lblFileSize.Show();
                linkLblReleaseNotes.Show();
                lblInfo.Show();
                pbLogo.Show();
            }
            else
            {
                lblVersionLabel.Hide();
                lblVersion.Hide();
                lblFileSize.Hide();
                linkLblReleaseNotes.Hide();
                lblInfo.Hide();
                pbLogo.Hide();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            DownloadAndInstallUpdate();
        }

        string installerPath = null;
        private void DownloadAndInstallUpdate()
        {
            progressBar.ProgressBar.Show();
            progressBar.Style = ProgressBarStyle.Continuous;
            tslblStatus.Text = "Downloading...";

            installerPath = Path.Combine(Path.GetTempPath(), "Blueprint41ModellerSetup.exe");

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                client.DownloadFileAsync(new Uri(version.DownloadUrl), installerPath);
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
            }
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Update the Blueprint41 Modeller now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = installerPath;
                info.WindowStyle = ProcessWindowStyle.Normal;
                info.CreateNoWindow = false;

                Process.Start(info);
                Application.Exit();
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        async Task CheckFileSize(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "HEAD";
            // HttpWebRequest.GetResponse(): From MSDN: The actual instance returned
            // is an HttpWebResponse, and can be typecast to that class to access 
            // HTTP-specific properties. 
            WebResponse webResp = await req.GetResponseAsync();
            HttpWebResponse resp = (HttpWebResponse)webResp;
            lblFileSize.Text = "About " + resp.ContentLength.ToSize(Extensions.SizeUnits.MB) + "MB";
        }

        private void linkLblReleaseNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.Goto("https://github.com/circles-arrows/blueprint41/releases");
        }
    }
}
