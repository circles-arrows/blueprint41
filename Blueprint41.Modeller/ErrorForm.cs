using System;
using System.Windows.Forms;

namespace Blueprint41.Modeller
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
            ActiveControl = null;
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
