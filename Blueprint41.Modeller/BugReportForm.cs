using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public partial class BugReportForm : Form
    {
        public BugReportForm()
        {
            InitializeComponent();
        }

        public Exception Exception { get; private set; }

        public string ErrorMessage => GetExceptionMessage(Exception);

        public static DialogResult Show(Exception e)
        {
            using (var bugReportForm = new BugReportForm())
            {
                bugReportForm.Exception = e;
                return bugReportForm.ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ErrorForm.Show(ErrorMessage);
        }

        string GetExceptionMessage(Exception e)
        {
            string innerExceptionMessage = string.Empty;

            if (e.InnerException != null)
                innerExceptionMessage = GetExceptionMessage(e.InnerException);

            return e.Message + Environment.NewLine + Environment.NewLine + "Stack Trace:" + Environment.NewLine + e.StackTrace + Environment.NewLine + innerExceptionMessage;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendBugReport();
            }
            catch (SmtpException)
            {
                MessageBox.Show("We are unable to send the bug report at this time. Please ensure you have internet connection.", "Bug Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSend.Text = "Send Bug Report";
                btnSend.Enabled = true;
            }
        }

        void SendBugReport()
        {
            btnSend.Text = "Sending...";
            btnSend.Enabled = false;

            //SmtpClient smtpClient = new SmtpClient("mail.blueprint41.com");
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.Credentials = new NetworkCredential("mailer@blueprint41.com", "");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("mailer@blueprint41.com", "Blueprint41");
            mail.To.Add(new MailAddress("support@circles-arrows.com", "Support"));
            mail.Subject = "Bug Report";

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(Application.ProductName);
            builder.AppendLine(Application.ProductVersion);
            builder.AppendLine();

            if (string.IsNullOrEmpty(txtEmail.Text?.Trim()) == false)
                builder.AppendLine();

            if (string.IsNullOrEmpty(txtDetails.Text?.Trim()) == false)
                builder.AppendLine($"Bug Details: \n{txtDetails.Text}");

            if (cbIncludeException.Checked)
            {
                builder.AppendLine("Exception details:");
                builder.AppendLine(ErrorMessage);
            }

            mail.Body = builder.ToString();

            HttpUtility.UrlEncode(builder.ToString());
            string body = HttpUtility.UrlEncode(builder.ToString());

            // Solution 1
            string mailer = $"mailto:support@circles-arrows.com?subject={mail.Subject}&body={body}";
            Process.Start(mailer);

            // Solution 2: Involves smptclient, is it "ok" to have the network credentials readily available on git?
            //smtpClient.Send(mail);

            btnSend.Text = "Send Bug Report";
            btnSend.Enabled = true;
            DialogResult = DialogResult.Abort;
        }
    }
}
