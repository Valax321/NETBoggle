using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NETBoggle.Networking;

namespace NETBoggle.Client
{
    public partial class SettingsDialog : Form
    {

        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();

            textboxPlayerName.Text = PlayerSettings.Settings.PlayerName;
            textBoxServerName.Text = PlayerSettings.Settings.Host_ServerName;
            textBoxServerPassword.Text = PlayerSettings.Settings.Host_ServerPassword;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxServerName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a server name", "Invalid server name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PlayerSettings.Settings.Host_ServerName = textBoxServerName.Text;
            PlayerSettings.Settings.Host_ServerPassword = textBoxServerPassword.Text;
            PlayerSettings.Settings.PlayerName = textboxPlayerName.Text;

            PlayerSettings.SavePlayerSettings();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();
            Close();
        }
    }
}
