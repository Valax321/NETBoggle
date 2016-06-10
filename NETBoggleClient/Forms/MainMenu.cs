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

            try
            {
                HostServer.GameChanged += new Server.GameStateChangedHandler(Debug.DebugLog.UpdateStateLog);
            }
            catch
            {
                Console.WriteLine("No log to attach event to.");
            }
        }

        public bool ConnectPlayer()
        {
            try
            {
                us = new Player() { PlayerName = PlayerSettings.Settings.PlayerName };
                HostServer.ConnectPlayer(us); //Connect us to the server
                return true;
            }

            catch (ServerFullException e)
            {
                MessageBox.Show(e.ToString(), "Error connecting to server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

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

        private void hostNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HostServer = new Server(PlayerSettings.Settings.Host_ServerName, PlayerSettings.Settings.Host_ServerPassword);
            Debug.Log(string.Format("Opened new server {0}", HostServer.ServerName));
            ConnectPlayer();
            StartServer();
            ServerTick.Start();
            Text = string.Format("NET Boggle on {0}", HostServer.ServerName);
        }

        private void ServerTick_Tick(object sender, EventArgs e)
        {
            if (HostServer != null)
            {
                HostServer.Tick(1/ServerTick.Interval);
                dataGridScoreboard.DataSource = HostServer.Players;
            }
        }
    }
}
