using System;
using System.Windows.Forms;

namespace NETBoggle.Client
{
    /// <summary>
    /// Form for editing player preferences, such as username and server details.
    /// </summary>
    public partial class SettingsDialog : Form
    {

        public SettingsDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load in the player preferences.
        /// </summary>
        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();

            textboxPlayerName.Text = PlayerSettings.Settings.PlayerName;
            textBoxServerName.Text = PlayerSettings.Settings.Host_ServerName;
            textBoxServerPassword.Text = PlayerSettings.Settings.Host_ServerPassword;
        }

        /// <summary>
        /// Save the player preferences.
        /// </summary>
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

        /// <summary>
        /// Refresh the player settings and close.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();
            Close();
        }
    }
}
