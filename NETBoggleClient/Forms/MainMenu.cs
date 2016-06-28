using System;
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

                Bytecode<string, string>.BindInstruction(BoggleInstructions.SERVER_CLIENT_MESSAGE, test_binding);
            }
        }

        public void test_binding(string o, string t)
        {
            Debug.Log(o);
            Debug.Log(t);
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

                HostServer.PlayerSendWord(us, textBoxWordInput.Text);
                textBoxWordHistory.Text += textBoxWordInput.Text + Environment.NewLine;
                textBoxWordInput.Text = string.Empty;
            }
        }

        /// <summary>
        /// Notify the server that we're ready to play.
        /// </summary>
        private void buttonReadyRound_Click(object sender, EventArgs e)
        {
            us.Ready = true;
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
            Debug.Log(Bytecode<string, string>.Generate(BoggleInstructions.SERVER_CLIENT_MESSAGE, d_ins1.Text, d_ins2.Text));
        }

        private void interpretBytecodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string b = Bytecode<string, string>.Generate(BoggleInstructions.SERVER_CLIENT_MESSAGE, d_ins1.Text, d_ins2.Text);
            Bytecode<string, string>.Parse(b);
        }

        private void passwordBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerPassword sp = new ServerPassword();
            sp.ShowDialog();
        }

        private void MainMenu_VisibleChanged(object sender, EventArgs e)
        {
            debugToolStripMenuItem.Visible = Debugging;
        }
    }
}
