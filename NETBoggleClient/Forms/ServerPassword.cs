using System;
using System.Windows.Forms;

namespace NETBoggle.Client
{
    public partial class ServerPassword : Form
    {
        MainMenu Sender_;

        public ServerPassword(MainMenu sender)
        {
            InitializeComponent();
            Sender_ = sender;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            Close();
            if (Sender_ != null)
            {
                Sender_.Connect(textBoxIP.Text, textBoxPassword.Text);
            }
        }
    }
}
