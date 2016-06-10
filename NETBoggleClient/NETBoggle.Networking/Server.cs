using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.IO;

namespace NETBoggle.Networking
{
    /// <summary>
    /// Server running the game backend.
    /// </summary>
    public class Server
    {
        public const int PLAYER_CAP = 50; // Max players per server.
        public const int GameLength = 60; //Length in seconds

        const string DICE_LOCATION = "dice.json";

        List<BoggleDie> DiceLetters = new List<BoggleDie>(16);

        public float DeltaTime;

        IBoggleState CurrentState;

        public IBoggleState GetState()
        {
            return CurrentState;
        }

        /// <summary>
        /// Initialise a new server
        /// </summary>
        /// <param name="name">the hosted server name</param>
        /// <param name="password">optional server password</param>
        public Server(string name, string password)
        {
            ServerName = name;
            ServerPassword = password;

            string dice_string = "";
            try
            {
                using (StreamReader sr = new StreamReader(DICE_LOCATION))
                {
                    dice_string = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Debug.Assert(true, e.ToString()); //Very super serious problem. Dice list is missing.
            }

            DiceWrapper dr = JsonConvert.DeserializeObject<DiceWrapper>(dice_string);

#region Ungodly hack for dice
            DiceLetters.Add(new BoggleDie(dr.Die0));
            DiceLetters.Add(new BoggleDie(dr.Die1));
            DiceLetters.Add(new BoggleDie(dr.Die2));
            DiceLetters.Add(new BoggleDie(dr.Die3));
            DiceLetters.Add(new BoggleDie(dr.Die4));
            DiceLetters.Add(new BoggleDie(dr.Die5));
            DiceLetters.Add(new BoggleDie(dr.Die6));
            DiceLetters.Add(new BoggleDie(dr.Die7));
            DiceLetters.Add(new BoggleDie(dr.Die8));
            DiceLetters.Add(new BoggleDie(dr.Die9));
            DiceLetters.Add(new BoggleDie(dr.Die10));
            DiceLetters.Add(new BoggleDie(dr.Die11));
            DiceLetters.Add(new BoggleDie(dr.Die12));
            DiceLetters.Add(new BoggleDie(dr.Die13));
            DiceLetters.Add(new BoggleDie(dr.Die14));
            DiceLetters.Add(new BoggleDie(dr.Die15));
#endregion
        }

        /// <summary>
        /// Get the number of players on the server.
        /// </summary>
        public int PlayerCount
        {
            get
            {
                return Players.Count;
            }
        }

        public string ServerName = "Boggle Server";
        public string ServerPassword = string.Empty;

        public List<Player> Players = new List<Player>(PLAYER_CAP);

        public void ConnectPlayer(Player new_player)
        {
            try
            {
                Players.Add(new_player);
            }

            catch (Exception e)
            {
                throw new ServerFullException(string.Format("Current server {0} is full", ServerName), e);
            }
        }

        public void RoundEnd()
        {

        }

        public void Start()
        {
            CurrentState = new BoggleWaitReady();
        }
        
        public void Tick(float tick)
        {
            DeltaTime = tick;
            IBoggleState state = CurrentState.Handle(this);
            if (state != null)
            {
                CurrentState = state;
                GameChanged.Invoke(state, EventArgs.Empty);
            }
        }

        public delegate void GameStateChangedHandler(IBoggleState newstate, EventArgs e);

        public event GameStateChangedHandler GameChanged; 

    }

    public class ServerFullException : Exception
    {
        public ServerFullException() { }

        public ServerFullException(string message) : base(message) { }

        public ServerFullException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Server client
    /// </summary>
    public class Player
    {
        public string PlayerName { get; set; }

        public uint Score { get; set; }

        public List<string> TypedWords = new List<string>();

        public bool Ready = false;

        //Lock the ready button
        //public delegate void ServerRecieveClickReadyHandler();
        //public event ServerRecieveClickReadyHandler ServerRecieveClickReady;

        //Unlock the ready button
        //public delegate void ServerUnlockClientReadyHandler();
        //public event ServerUnlockClientReadyHandler ServerUnlockClient;
    }
}
