﻿using System;
using System.Linq;
using System.Windows.Forms;
using NETBoggle.Networking;
using NETBoggle.Networking.Bytecode;

namespace NETBoggle.Client
{
    /// <summary>
    /// Form for the main gameplay of Boggle.
    /// </summary>
    public partial class MainMenu : Form
    {
        SettingsDialog options = new SettingsDialog();

        Server HostServer;

        bool Debugging = false;

        NetworkTools.NetClient GameClient = new NetworkTools.NetClient();

        Player us; //Our player

        public MainMenu(string[] progargs)
        {
            InitializeComponent();

            foreach (string s in progargs)
            {
                if (s == "-debug")
                {
                    Debug.SetupLog(new Debugger());
                    Debugging = true;
                }

                Bytecode.SendMessage += ClientReceiveServerMessage; //Bind to message receive event.
                Bytecode.ClientSetFormState += Bytecode_ClientSetFormState; //Bind to form state change
                Bytecode.ClientSetFormText += Bytecode_ClientSetFormText; //Bind to form text change
            }
        }

        private void Bytecode_ClientSetFormText(string p1, string p2)
        {
            Player.SetElementText(p1, p2, this);
        }

        private void Bytecode_ClientSetFormState(string p1, bool p2)
        {
            //Debug.Log(string.Format("Setting {0} to {1}", p1, p2));
            Player.SetElementEnabled(p1, p2, this);
        }

        /// <summary>
        /// Attempts to connect a player to the server we own.
        /// </summary>
        /// <returns>If the player was able to connect.</returns>
        [Obsolete]
        public bool ConnectPlayer()
        {
            //GameClient.Init("192.168.1.1"); //Throws exception in RedCorona if the IP is not valid.
            try
            {
                us = new Player(0) { PlayerName = PlayerSettings.Settings.PlayerName, ClientInterface = this };
                HostServer.ConnectPlayer(us); //Connect us to the server
                return true;
            }

            catch (ServerFullException e)
            {
                MessageBox.Show(e.ToString(), "Error connecting to server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public void Connect(string ip, string password)
        {
            try
            {
                GameClient.Init(ip);
                GameClient.Connect();
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        void ClientSendMessage(BoggleInstructions b, string param1, string param2 = "")
        {
            if (GameClient != null)
            {
                GameClient.SendMessageToServer(b, param1, param2, 1);
            }
        }

        //Receive
        private void ClientReceiveServerMessage(string message)
        {
            Debug.Log(message);
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
            HostServer.Init();
            Debug.Log(string.Format("Opened new server {0}", HostServer.ServerName));
            MessageBox.Show(NetworkTools.GetLocalIPAddress());
            GameClient.Init(string.Empty);
            GameClient.Connect();
            ClientSendMessage(BoggleInstructions.SET_NAME, PlayerSettings.Settings.PlayerName);
            StartServer();
            //ConnectPlayer();
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

                //HostServer.PlayerSendWord(us, textBoxWordInput.Text);
                ClientSendMessage(BoggleInstructions.SENDWORD, textBoxWordInput.Text);
                textBoxWordHistory.Text += textBoxWordInput.Text + Environment.NewLine;
                textBoxWordInput.Text = string.Empty;
            }
        }

        

        #region Boring Boilerplate stuff

        /// <summary>
        /// Notify the server that we're ready to play.
        /// </summary>
        private void buttonReadyRound_Click(object sender, EventArgs e)
        {
            ClientSendMessage(BoggleInstructions.READY, string.Empty);
        }

        private void dumpDicePositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HostServer != null)
            {
                for (int i = 0; i < HostServer.DiceLetters.Count; i++)
                {
                    Debug.Log(string.Format("Die {0}: {1}, {2}", i, HostServer.DiceLetters.ElementAt(i).Position.Item1, HostServer.DiceLetters.ElementAt(i).Position.Item2));
                }
            }
        }

        private void dumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Log(Bytecode.Generate(BoggleInstructions.SERVER_CLIENT_MESSAGE, d_ins1.Text, d_ins2.Text));
        }

        private void interpretBytecodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string b = Bytecode.Generate(BoggleInstructions.SERVER_CLIENT_MESSAGE, d_ins1.Text, d_ins2.Text);
            Bytecode.Parse(b);
        }

        private void passwordBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerPassword sp = new ServerPassword(this);
            sp.ShowDialog();
        }

        private void MainMenu_VisibleChanged(object sender, EventArgs e)
        {
            debugToolStripMenuItem.Visible = Debugging;
        }

        private void printIPAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Debug.Log(NetworkTools.GetLocalIPAddress());
        }

        #endregion
    }
}
