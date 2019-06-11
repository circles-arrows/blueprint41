using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            GetApplicationVersion();
        }

        private void llSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Goto("https://www.blueprint41.com");
            llSite.LinkVisited = true;
        }

        private void llLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Goto("https://mit-license.org/");
            llLicense.LinkVisited = true;
        }

        void Goto(string url)
        {
            Process.Start(new ProcessStartInfo(url));
        }

        void GetApplicationVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            lblVersion.Text = version;
        }
    }
}
