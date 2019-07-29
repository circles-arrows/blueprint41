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
            FormClosing += UpdaterForm_FormClosing;
        }

        private void UpdaterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Yes)
                return;

            if (webClient != null)
            {
                if (CancelUpdate() == DialogResult.Yes)
                {
                    try
                    {
                        webClient?.CancelAsync();
                    }
                    catch (System.ObjectDisposedException) { }
                }
                else
                    e.Cancel = true;
            }
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
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.ProgressBar.Show();

            version = await Util.CheckForUpdates();

            if (progressBar.ProgressBar != null)
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.MarqueeAnimationSpeed = 0;
                progressBar.ProgressBar.Hide();
            }

            bool hasUpdates = version.IsUpdatedVersion();
            tslblStatus.Text = hasUpdates ? "Update available" : "No update available";

            if (hasUpdates && string.IsNullOrEmpty(version.DownloadUrl) == false)
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
        WebClient webClient;
        private void DownloadAndInstallUpdate()
        {
            progressBar.ProgressBar.Show();
            progressBar.Style = ProgressBarStyle.Continuous;
            tslblStatus.Text = "Downloading...";

            installerPath = Path.Combine(Path.GetTempPath(), "Blueprint41ModellerSetup.exe");

            webClient = new WebClient();

            webClient.DownloadProgressChanged += Client_DownloadProgressChanged;
            webClient.DownloadFileAsync(new Uri(version.DownloadUrl), installerPath);
            webClient.DownloadFileCompleted += Client_DownloadFileCompleted;

        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            if (e.Error != null)
            {
                MessageBox.Show("There was an error on downloading the Blueprint41 Modeller. Please try again", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Update the Blueprint41 Modeller now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RegistryHandler.IsInstallUpgrade = true;
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = installerPath;
                info.WindowStyle = ProcessWindowStyle.Normal;
                info.CreateNoWindow = false;

                this.DialogResult = DialogResult.Yes;

                Process.Start(info);

                Application.Exit();
            }
            else
            {
                this.DialogResult = DialogResult.Yes;
                Close();
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (progressBar.ProgressBar == null)
                return;

            progressBar.Value = e.ProgressPercentage;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        async Task CheckFileSize(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("Url cannot be empty");

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

        DialogResult CancelUpdate()
        {
            return MessageBox.Show("Are you sure to cancel the update?", "Cancel Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
