using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            cbUseProxy.Checked = PlayerSettings.Settings.UseProxy;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            PlayerSettings.SavePlayerSettings();
            Close();
        }

        private void cbUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            PlayerSettings.Settings.UseProxy = cbUseProxy.Checked;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();
            Close();
        }

        private void textboxPlayerName_TextChanged(object sender, EventArgs e)
        {
            PlayerSettings.Settings.PlayerName = textboxPlayerName.Text;
        }
    }
}
