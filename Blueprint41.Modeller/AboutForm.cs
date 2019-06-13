using Blueprint41.Modeller.Utils;
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
            Util.Goto("https://www.blueprint41.com");
            llSite.LinkVisited = true;
        }

        private void llLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.Goto("https://mit-license.org/");
            llLicense.LinkVisited = true;
        }

        void GetApplicationVersion()
        {
            lblVersion.Text = ModellerVersion.CurrentVersion().ToString();
        }
    }
}
