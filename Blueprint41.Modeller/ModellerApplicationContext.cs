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
            ShowMainForm();
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
            main.Shown += (s, e) => loader.Close();
        }
    }
}
