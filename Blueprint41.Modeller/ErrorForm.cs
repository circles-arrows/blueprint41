using System;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
            Load += ErrorForm_Load;
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            FormClosing += ErrorForm_FormClosing;
            buttonAbort.Click += ButtonAbort_Click;
        }

        private void ButtonAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void ErrorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Abort)
                DialogResult = DialogResult.Abort;
        }

        public string ErrorMessage
        {
            get { return textBoxError.Text; }
            set { textBoxError.Text = value; }
        }

        public static DialogResult Show(string message)
        {
            using (var errorForm = new ErrorForm())
            {
                errorForm.ErrorMessage = message;
                return errorForm.ShowDialog();
            }
        }
    }
}
