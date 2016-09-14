using System;
using System.Linq;
using System.Windows.Forms;
using NETBoggle.Networking;
using NETBoggle.Networking.Bytecode;
using System.ComponentModel;

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

        BindingList<PlayerContainer> PlayerList = new BindingList<PlayerContainer>();

        /// <summary>
        /// Binds events and constructs debug log if specified. Also binds data source for chart.
        /// </summary>
        /// <param name="progargs"></param>
        public MainMenu(string[] progargs)
        {
            InitializeComponent();

            playerContainerBindingSource.DataSource = PlayerList;

            foreach (string s in progargs)
            {
                if (s == "-debug")
                {
                    Debug.SetupLog(new Debugger());
                    Debugging = true;
                }               
            }

            Bytecode.SendMessage += ClientReceiveServerMessage; //Bind to message receive event.
            Bytecode.ClientSetFormState += Bytecode_ClientSetFormState; //Bind to form state change
            Bytecode.ClientSetFormText += Bytecode_ClientSetFormText; //Bind to form text change
            Bytecode.ServerShutDown += Bytecode_ServerShutDown; //Bind to server shutdown
          
            //Player list
            Bytecode.ClientAddList += Bytecode_ClientAddList; //Bind to client list update
            Bytecode.ClientRemoveList += Bytecode_ClientRemoveList; //Bind to client list remove.
            Bytecode.ClientSetScore += Bytecode_ClientSetScore; //Bind to client score update.

        }

        // Bound method for ClientSetScore event
        private void Bytecode_ClientSetScore(string p1, uint p2)
        {
            foreach (PlayerContainer pc in PlayerList)
            {
                if (pc.PlayerName == p1)
                {
                    pc.Score = p2;
                }
            }
        }

        // Bound method for ClientRemoveList event
        private void Bytecode_ClientRemoveList(string param)
        {
            foreach (PlayerContainer pc in PlayerList)
            {
                if (pc.PlayerName == param)
                {
                    lock(PlayerList)
                    PlayerList.Remove(pc);
                }
            }
        }

        // Bound method for ClientAddList event
        private void Bytecode_ClientAddList(string param)
        {
            try
            {
                lock(PlayerList)
                PlayerList.Add(new PlayerContainer(param));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Bound method for ServerShutDown event
        private void Bytecode_ServerShutDown()
        {
            MessageBox.Show("Server has shut down");
            GameClient = new NetworkTools.NetClient();
        }

        // Bound method for ClientSetFormText event
        private void Bytecode_ClientSetFormText(string p1, string p2)
        {
            Player.SetElementText(p1, p2, this);
        }

        // Bound method for ClientSetFormState event
        private void Bytecode_ClientSetFormState(string p1, bool p2)
        {
            //Debug.Log(string.Format("Setting {0} to {1}", p1, p2));
            Player.SetElementEnabled(p1, p2, this);
        }

        /// <summary>
        /// Connect to a server (given by IP address) and send our name with it. Logs an error if we can't connect.
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="password"></param>
        public void Connect(string ip, string password)
        {
            try
            {
                GameClient.Init(ip);
                GameClient.Connect();
                ClientSendMessage(BoggleInstructions.SET_NAME, PlayerSettings.Settings.PlayerName);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        // Send a message to the server.
        void ClientSendMessage(BoggleInstructions b, string param1, string param2 = "")
        {
            if (GameClient != null)
            {
                GameClient.SendMessageToServer(b, param1, param2, 1);
            }
        }

        //Receive log from server
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
            boxServerClose.Enabled = true;
            Debug.Log("Server started");
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Server_Shutdown();
        }

        // Shut down this server
        private void Server_Shutdown()
        {
            ServerTick.Stop();
            boxServerClose.Enabled = false;
            if (HostServer != null)
            {
                HostServer.Shutdown();
            }
        }

        

        /// <summary>
        /// This starts the server when we click File->New Server->Host New Server.
        /// </summary>
        private void hostNewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Server_Shutdown();
            HostServer = new Server(PlayerSettings.Settings.Host_ServerName, PlayerSettings.Settings.Host_ServerPassword);
            HostServer.Init();
            Debug.Log(string.Format("Opened new server {0}", HostServer.ServerName));
            MessageBox.Show(NetworkTools.GetLocalIPAddress());
            Connect(string.Empty, PlayerSettings.Settings.Host_ServerPassword);
            //ClientSendMessage(BoggleInstructions.SET_NAME, PlayerSettings.Settings.Host_ServerPassword);
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
            }
        }

        /// <summary>
        /// When the player types a word and hits the enter key.
        /// </summary>
        private void textBoxWordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (string.IsNullOrWhiteSpace(textBoxWordInput.Text)) //Empty the box if we try and send a blank word
                {
                    textBoxWordInput.Text = string.Empty;
                    return;
                }

                ClientSendMessage(BoggleInstructions.SENDWORD, textBoxWordInput.Text); //Send message
                textBoxWordHistory.Text += textBoxWordInput.Text + Environment.NewLine; //Clear the box
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

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options.ShowDialog();
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

        private void boxServerClose_Click(object sender, EventArgs e)
        {
            Server_Shutdown();
        }

        private void joinGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerPassword sp = new ServerPassword(this);
            sp.ShowDialog();
        }
        #endregion

       
    }

    /// <summary>
    /// Client-side player implementation that only stores score and name, for efficiency.
    /// </summary>
    public class PlayerContainer : INotifyPropertyChanged
    {
        /// <summary>
        /// Event for value change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when a property's value updates.
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name"></param>
        public PlayerContainer(string name)
        {
            PlayerName = name;
        }

        private uint score_internal = 0;

        /// <summary>
        /// The string name of the player.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// The score for the player. Calls changed when modified.
        /// </summary>
        public uint Score
        {
            get { return score_internal; }

            set
            {
                score_internal = value;
                OnPropertyChanged("Score");
            }
        }

    }

}
