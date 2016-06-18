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
    public partial class MainMenu : Form
    {
        SettingsDialog options = new SettingsDialog();

        Server HostServer;

        Player us; //Our player

        public MainMenu(string[] progargs)
        {
            InitializeComponent();

            foreach (string s in progargs)
            {
                if (s == "-debug")
                {
                    Debug.SetupLog(new Debugger());
                }
            }
        }

        /// <summary>
        /// Attempts to connect a player to the server we own.
        /// </summary>
        /// <returns>If the player was able to connect.</returns>
        public bool ConnectPlayer()
        {
            try
            {
                us = new Player() { PlayerName = PlayerSettings.Settings.PlayerName, ClientInterface = this };
                HostServer.ConnectPlayer(us); //Connect us to the server
                return true;
            }

            catch (ServerFullException e)
            {
                MessageBox.Show(e.ToString(), "Error connecting to server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        /// <summary>
        /// Start a new server
        /// </summary>
        public void StartServer()
        {
            HostServer.Start();
            Debug.Log("Server started");
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options.ShowDialog();
        }

        /// <summary>
        /// This starts the server when we click File->New Server->Host New Server.
        /// </summary>
        private void hostNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostServer = new Server(PlayerSettings.Settings.Host_ServerName, PlayerSettings.Settings.Host_ServerPassword);
            Debug.Log(string.Format("Opened new server {0}", HostServer.ServerName));
            ConnectPlayer();
            StartServer();
            ServerTick.Start();
            Text = string.Format("NET Boggle on {0}", HostServer.ServerName);
        }

        /// <summary>
        /// Updates the server by a single tick.
        /// </summary>
        private void ServerTick_Tick(object sender, EventArgs e)
        {
            if (HostServer != null)
            {
                HostServer.Tick(0.1f);
                dataGridScoreboard.DataSource = null;
                dataGridScoreboard.DataSource = HostServer.Players;
                dataGridScoreboard.Update();
            }
        }

        /// <summary>
        /// When the player types a word and hits the enter key.
        /// </summary>
        private void textBoxWordInput_KeyDown(object sender, KeyEventArgs e)
        {
            //TEMP: directly to host server right now, change to send a message to the server over net.

            if (e.KeyCode == Keys.Return)
            {
                if (HostServer == null)
                    return;

                if (string.IsNullOrWhiteSpace(textBoxWordInput.Text))
                {
                    textBoxWordInput.Text = string.Empty;
                    return;
                }

                if (HostServer.WordList.Contains(textBoxWordInput.Text))
                {
                    Debug.Log("Valid word");
                }

                HostServer.PlayerSendWord(us, textBoxWordInput.Text);
                textBoxWordHistory.Text += textBoxWordInput.Text + Environment.NewLine;
                textBoxWordInput.Text = string.Empty;
            }
        }

        private void buttonReadyRound_Click(object sender, EventArgs e)
        {
            us.Ready = true;
        }
    }
}
