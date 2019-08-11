using System;
using System.Windows.Forms;

namespace SmartPaster
{
    public partial class ReplaceForm : Form
    {
        public ReplaceForm()
        {
            InitializeComponent();
            labClipboard.Text = SmartPaster.ClipboardText.Trim();
        }

        private void pasteButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public string TextToReplace
            => this.findTextBox.Text;

        public string ReplaceText
            => this.replaceWithTextBox.Text;
    }
}