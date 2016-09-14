using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
=======
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using RedCorona.Net;
>>>>>>> origin/master
using NETBoggle.Networking.Bytecode;

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
        const string WORD_LIST_LOCATION = "words.lst";

<<<<<<< HEAD
        public List<BoggleDie> DiceLetters = new List<BoggleDie>(16); // List of dice
=======
        public List<BoggleDie> DiceLetters = new List<BoggleDie>(16);

        List<string> WordList = new List<string>();
>>>>>>> origin/master

        List<string> WordList = new List<string>();

        public float DeltaTime; // Current server time.

        IBoggleState CurrentState;

        NetworkTools.NetServer GameServer;

        /// <summary>
        /// Broadcast a message to all clients connected (ie. debug message, shutdown notify)
        /// </summary>
        /// <param name="ins"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        public void BroadcastInstruction(BoggleInstructions ins, string param1, string param2 = "")
        {
            if (GameServer != null)
            {
                GameServer.Server_Broadcast(Bytecode.Bytecode.Generate(ins, param1, param2));
            }
        }

        /// <summary>
        /// Get current server state.
        /// </summary>
        /// <returns></returns>
        public IBoggleState GetState()
        {
            return CurrentState;
        }
        
        /// <summary>
        /// Set dice positions to random values
        /// </summary>
        public void RandomiseDicePositions()
        {
            DiceLetters.Shuffle();

            for (int i = 0; i < DiceLetters.Count; i++)
            {
                DiceLetters.ElementAt(i).SetPosition((int)Math.Floor((double)i / 4), i % 4);
            }
        }

        /// <summary>
        /// Initialise a new server
        /// </summary>
        /// <param name="name">the hosted server name</param>
        /// <param name="password">optional server password</param>
        public Server(string name, string password)
        {
            GameServer = new NetworkTools.NetServer(this);
            ServerName = name;
            ServerPassword = password;

            #region Bind to Events from Bytecode
            Bytecode.Bytecode.ServerReveiveWord += PlayerSendWord;
            Bytecode.Bytecode.ClientClickReady += Bytecode_ClientClickReady;
            Bytecode.Bytecode.SetClientName += Bytecode_SetClientName;
            #endregion

            string dice_string = "";
            try
            {
                using (StreamReader sr = new StreamReader(DICE_LOCATION)) //Load dice letters
                {
                    dice_string = sr.ReadToEnd();
                }

                using (StreamReader sr = new StreamReader(WORD_LIST_LOCATION)) //Load dictionary
                {
                    string[] Words = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string s in Words)
                    {
                        WordList.Add(s);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString()); //Very super serious problem. Dice list/word list is missing.
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

        private void Bytecode_SetClientName(string param, Player exec)
        {
            if (Players.Contains(exec))
            {
<<<<<<< HEAD
                string newname = param + "{0}";
                string lastname = param;
                int name_counter = 0;
                foreach (Player name in Players)
                {
                    if (name.PlayerName == lastname) //Don't allow duplicate names, add a number to them.
                    {
                        lastname = string.Format(newname, name_counter);
                        name_counter++;
                    }
                }

                if (name_counter != 0)
                {
                    newname = string.Format(newname, name_counter);
                }
                else
                {
                    newname = param;
                }

                exec.PlayerName = newname;
                //PlayerNames.Add(newname);
                BroadcastInstruction(BoggleInstructions.PLAYER_JOINED, newname);
=======
                exec.PlayerName = param;
>>>>>>> origin/master
            }
        }

        private void Bytecode_ClientClickReady(Player exec)
        {
            if (Players.Contains(exec))
            {
                exec.Ready = true;
            }
        }

<<<<<<< HEAD
        public void PlayerConnected(Player joined)
        {
            foreach (Player p in Players)
            {
                if (p == joined) continue;
                NetMSG_Send(joined, Bytecode.Bytecode.Generate(BoggleInstructions.PLAYER_JOINED, p.PlayerName));
                NetMSG_Send(joined, Bytecode.Bytecode.Generate(BoggleInstructions.PLAYER_SCORE, p.PlayerName, p.Score.ToString()));
            }
        }

        public void PlayerDisconnect(Player left)
        {
            BroadcastInstruction(BoggleInstructions.PLAYER_LEFT, left.PlayerName);
        }

=======
>>>>>>> origin/master
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
        string ServerPassword = string.Empty;

        public List<Player> Players
        {
            get
            {
                if (GameServer != null)
                {
                    return GameServer.PlayerLookup.Values.ToList(); //Get the server-side list of clients
                }
                else
                {
                    return new List<Player>();
                }
            }
        }

        /// <summary>
        /// Check to see if the server is full, and add us if it isn't
        /// </summary>
        [Obsolete("Send a message instead")]
        public void ConnectPlayer(Player new_player)
        {
            try
            {
                Players.Add(new_player);
            }

            catch (Exception e)
            {
                //throw new ServerFullException(string.Format("Current server {0} is full", ServerName), e);
            }
        }

        /// <summary>
        /// Start game
        /// </summary>
        public void Start()
        {
            CurrentState = new BoggleWaitReady();
<<<<<<< HEAD
        }

        public void Init()
        {
            GameServer.StartServer();
        }

        public void Shutdown()
        {
            ShuttingDown = true;
            NetMSG_SetFormState("buttonReadyRound", false);
            NetMSG_SetFormState("wordsBox", false);
            GameServer.CloseServer();
        }

        bool ShuttingDown = false;

=======
        }

        public void Init()
        {
            GameServer.StartServer();
        }
        
>>>>>>> origin/master
        /// <summary>
        /// Process the server state
        /// </summary>
        public void Tick(float tick)
        {
<<<<<<< HEAD
            if (ShuttingDown) return;
=======
>>>>>>> origin/master
            //BroadcastInstruction(BoggleInstructions.SERVER_CLIENT_MESSAGE, string.Format("Hello world! Tick time: {0}", tick.ToString()));
            DeltaTime = tick;
            IBoggleState state = CurrentState.Handle(this);
            if (state != null)
            {
                CurrentState = state;
                CurrentState.Construct(this);
            }
        }

        /// <summary>
        /// Check word validity
        /// </summary>
        [Obsolete]
        public void PlayerSendWord(Player player, string word)
        {
            if (!player.TypedWords.Contains(word) && WordList.Contains(word) && CheckWordInPlay(word))
            {
                Debug.Log("Actually valid");
                player.TypedWords.Add(word);
            }
        }

        /// <summary>
        /// When we're sent a word.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="p"></param>
        public void PlayerSendWord(string w, Player p)
        {
            if (!p.TypedWords.Contains(w) && WordList.Contains(w) && CheckWordInPlay(w))
            {
                Debug.Log("Actually valid");
                p.TypedWords.Add(w);
            }
        }

        public void NetMSG_SetFormState(string element, bool state, Player p)
        {
            NetMSG_Send(p, Bytecode.Bytecode.Generate(BoggleInstructions.FORMSTATE, element, state.ToString()));
        }
<<<<<<< HEAD

        public void NetMSG_SetFormState(string element, bool state)
        {
            NetMSG_Send(Bytecode.Bytecode.Generate(BoggleInstructions.FORMSTATE, element, state.ToString()));
        }

        public void NetMSG_SetFormText(string element, string text, Player p)
        {
            NetMSG_Send(p, Bytecode.Bytecode.Generate(BoggleInstructions.FORMTEXT, element, text));
        }

        /// <summary>
        /// Broadcast version
        /// </summary>
        public void NetMSG_SetFormText(string element, string text)
        {
            NetMSG_Send(Bytecode.Bytecode.Generate(BoggleInstructions.FORMTEXT, element, text));
        }

        /// <summary>
        /// Global broadcast
        /// </summary>
        void NetMSG_Send(string packet)
        {
            if (GameServer != null)
            {
                GameServer.Server_Broadcast(packet);
            }
        }

        /// <summary>
        /// Send to specific client.
        /// </summary>
        void NetMSG_Send(Player pl, string packet)
        {
            if (GameServer != null)
            {
                GameServer.Server_MessageClient(packet, pl);
            }
        }

        /// <summary>
        /// Exec a player-sent instruction.
        /// </summary>
        /// <param name="sender">Player class abstracted from server system.</param>
        /// <param name="instruction"></param>
        public void ProcessInstruction(Player sender, string instruction)
        {
            Bytecode.Bytecode.Parse(instruction, sender);
        }

        /// <summary>
        /// Check if word is on board
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool CheckWordInPlay(string word)
        {
            word = word.ToUpper();
            bool isConnected = false;
            string currentChar = "";
            List<Tuple<int, int>> labelPositions = new List<Tuple<int, int>>();
            List<Tuple<int, int>> lastLabelPositions = null;
            for (int i = 0; i < word.Length; i++)
            {
                currentChar = word[i].ToString();
                if (currentChar == "Q")
                {
                    currentChar = "Qu";
                }
                else if (currentChar == "U")
                {
                    isConnected = true;
                    continue;
                }
                isConnected = false;
                foreach (BoggleDie l in DiceLetters)
                {
                    if (l.CurrentLetter == currentChar)
                    {
                        //labelPositions.Add(ExtensionMethods.CoordinatesOf<BoggleDie>(DiceLetters, l)); //Dead
                        labelPositions.Add(l.Position);
                        break; // Try this for searching if it exists
                    }
                }
                foreach (Tuple<int, int> t in labelPositions)
                {
                    if (lastLabelPositions == null)
                    {
                        isConnected = true;
                        continue;
                    }
                    foreach (Tuple<int, int> n in lastLabelPositions)
                    {
                        int tupleDistanceX = Math.Abs(n.Item1 - t.Item1);
                        int tupleDistanceY = Math.Abs(n.Item2 - t.Item2);
                        if (tupleDistanceX <= 1 && tupleDistanceY <= 1)
                        {
                            isConnected = true;
                        }
                    }
                }
                Debug.Log(string.Format("Letter: {0} Connected: {1}", currentChar, isConnected));
                if (!isConnected)
                {
                    break;
                }
                lastLabelPositions = labelPositions;
            }
            return isConnected;
        }

        public bool IsAttached(BoggleDie die, string letter)
        {
            foreach (BoggleDie around in DiceLetters)
            {
                if (around.Position == new Tuple<int, int>(die.Position.Item1 - 1, die.Position.Item2 - 1))
                {
                    if (around.CurrentLetter == letter)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //public bool CheckValidWord(string word)
        //{
        //    word = word.ToUpper();

        //    string letter = "";
        //    string nextletter = "";

        //    BoggleDie current = null;
        //    BoggleDie last = null;

        //    bool valid = false;

        //    int iter = 0;

        //    foreach (BoggleDie die in DiceLetters)
        //    {
        //        iter++;
        //        last = current;
        //        current = die;

        //        letter = die.CurrentLetter;
        //        try
        //        {
        //            nextletter = word[iter].ToString();
        //        }
        //        catch (IndexOutOfRangeException)
        //        {
        //            //No more letters
        //            return valid;
        //        }

        //        // Iterate each, test if it is next to something
        //        if (die.Position.Item1 - 1 > 0 && die.Position.Item2 - 1 > 0) // TOP LEFT
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1 -1, die.Position.Item2 -1 ))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (die.Position.Item2 - 1 > 0) // TOP
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1, die.Position.Item2 - 1))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (die.Position.Item1 + 1 < 5 && die.Position.Item2 -1 > 0) //Top right
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1 + 1, die.Position.Item2 - 1))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (die.Position.Item1 + 1 < 5) // right
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1 + 1, die.Position.Item2))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (die.Position.Item1 + 1 < 5 && die.Position.Item2 + 1 < 5) // Bottom right
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1 + 1, die.Position.Item2 + 1))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (die.Position.Item2 + 1 < 5) // Bottom
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1, die.Position.Item2 + 1))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (die.Position.Item1 - 1 > 0 && die.Position.Item2 + 1 < 5) //Bottom left
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1 - 1, die.Position.Item2 + 1))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //        if (die.Position.Item1 - 1 > 0) // left
        //        {
        //            foreach (BoggleDie around in DiceLetters)
        //            {
        //                if (around.Position == new Tuple<int, int>(die.Position.Item1 - 1, die.Position.Item2))
        //                {
        //                    if (around.CurrentLetter == letter)
        //                    {
        //                        valid = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

=======

        public void NetMSG_SetFormState(string element, bool state)
        {
            NetMSG_Send(Bytecode.Bytecode.Generate(BoggleInstructions.FORMSTATE, element, state.ToString()));
        }

        public void NetMSG_SetFormText(string element, string text, Player p)
        {
            NetMSG_Send(p, Bytecode.Bytecode.Generate(BoggleInstructions.FORMTEXT, element, text));
        }

        /// <summary>
        /// Broadcast version
        /// </summary>
        public void NetMSG_SetFormText(string element, string text)
        {
            NetMSG_Send(Bytecode.Bytecode.Generate(BoggleInstructions.FORMTEXT, element, text));
        }

        /// <summary>
        /// Global broadcast
        /// </summary>
        void NetMSG_Send(string packet)
        {
            if (GameServer != null)
            {
                GameServer.Server_Broadcast(packet);
            }
        }

        /// <summary>
        /// Send to specific client.
        /// </summary>
        void NetMSG_Send(Player pl, string packet)
        {
            if (GameServer != null)
            {
                GameServer.Server_MessageClient(packet, pl);
            }
        }

        /// <summary>
        /// Exec a player-sent instruction.
        /// </summary>
        /// <param name="sender">Player class abstracted from server system.</param>
        /// <param name="instruction"></param>
        public void ProcessInstruction(Player sender, string instruction)
        {
            Bytecode.Bytecode.Parse(instruction, sender);
        }

        /// <summary>
        /// Check if word is on board
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool CheckWordInPlay(string word)
        {
            word = word.ToUpper();
            bool isConnected = false;
            string currentChar = "";
            List<Tuple<int, int>> labelPositions = new List<Tuple<int, int>>();
            List<Tuple<int, int>> lastLabelPositions = null;
            for (int i = 0; i < word.Length; i++)
            {
                currentChar = word[i].ToString();
                if (currentChar == "Q")
                {
                    currentChar = "Qu";
                }
                else if (currentChar == "U")
                {
                    isConnected = true;
                    continue;
                }
                isConnected = false;
                foreach (BoggleDie l in DiceLetters)
                {
                    if (l.CurrentLetter == currentChar)
                    {
                        //labelPositions.Add(ExtensionMethods.CoordinatesOf<BoggleDie>(DiceLetters, l)); //Dead
                        labelPositions.Add(l.Position);
                        break; // Try this for searching if it exists
                    }
                }
                foreach (Tuple<int, int> t in labelPositions)
                {
                    if (lastLabelPositions == null)
                    {
                        isConnected = true;
                        continue;
                    }
                    foreach (Tuple<int, int> n in lastLabelPositions)
                    {
                        int tupleDistanceX = Math.Abs(n.Item1 - t.Item1);
                        int tupleDistanceY = Math.Abs(n.Item2 - t.Item2);
                        if (tupleDistanceX <= 1 && tupleDistanceY <= 1)
                        {
                            isConnected = true;
                        }
                    }
                }
                Debug.Log(string.Format("Letter: {0} Connected: {1}", currentChar, isConnected));
                if (!isConnected)
                {
                    break;
                }
                lastLabelPositions = labelPositions;
            }
            return isConnected;
        }
>>>>>>> origin/master

    }

    /// <summary>
<<<<<<< HEAD
    /// Wrapper for extension method List<T>.Shuffle()
    /// </summary>
    public static class ExtensionMethods
=======
    /// Exception for when the server is full.
    /// </summary>
    public class ServerFullException : Exception
>>>>>>> origin/master
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    /// <summary>
    /// Wrapper for extension method List<T>.Shuffle()
    /// </summary>
    public static class ExtensionMethods
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
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

        public Form ClientInterface;

        public int PlayerIndex;

        public Player(int index)
        {
            PlayerIndex = index;
        }

        /// <summary>
        /// Find a control on a form.
        /// </summary>
        /// <param name="element">Element name. Use periods (.) to look in child objects.</param>
        /// <returns>The control, if found. Else returns null.</returns>
        public static Control FindControlRecursive(Form form, string element)
        {
            string[] FormChildSearch = element.Split('.');

            if (FormChildSearch.Length == 1)
            {
                return form.Controls.Find(element, false)[0];
            }
            else if (FormChildSearch.Length > 1)
            {
                Control current_parent = form;

                for (int i = 0; i < FormChildSearch.Length; i++)
                {
                    current_parent = current_parent.Controls.Find(FormChildSearch[i], false)[0];
                }

                if (current_parent == form)
                {
                    MessageBox.Show("Invalid form element " + element);
                    return null;
                }

                return current_parent;
            }
            else
            {
                MessageBox.Show("Invalid form element " + element);
                return null;
            }
        }

        /// <summary>
        /// Set a Control's Text field to a specified value.
        /// </summary>
        /// <param name="label">the control to change</param>
        /// <param name="text_val">value to set</param>
        static public void SetElementText(string label, string text_val, Form f = null)
        {
            if (f != null)
            {
                Control c = FindControlRecursive(f, label);

                if (c != null)
                {
                    if (c.InvokeRequired)
                    {
                        c.Invoke(new MethodInvoker(delegate { c.Text = text_val; }));
                    }
                    else
                        c.Text = text_val;
                    return;
                }
            }

            //MessageBox.Show("ClientInterface is null. Player is connected with no form!");            
        }

        //UNUSED
        static public void SetTextBoxReadOnly(string element, bool read_only, Form f = null)
        {
            if (f != null)
            {
                Control[] Controls = f.Controls.Find(element, false);

                if (Controls.Length < 1)
                {
                    MessageBox.Show("Invalid form element " + element);
                }

                foreach (Control c in Controls)
                {
                    TextBox intermediate = (TextBox)c;
                    if (c.InvokeRequired)
                    {
                        c.Invoke(new MethodInvoker(delegate { intermediate.ReadOnly = read_only; }));
                    }
                    else
                    intermediate.ReadOnly = read_only;
                }
            }

            else
            {
                //MessageBox.Show("ClientInterface is null. Player is connected with no form!");
            }
        }

        /// <summary>
        /// Set a Control's enabled field to a specified value
        /// </summary>
        /// <param name="element">Control to set</param>
        /// <param name="enabled">State to set</param>
        static public void SetElementEnabled(string element, bool enabled, Form f = null)
        {
            if (f != null)
            {
                Control c = FindControlRecursive(f, element);

                if (c != null)
                {
                    if (c.InvokeRequired)
                    {
                        c.Invoke(new MethodInvoker(delegate { c.Enabled = enabled; }));
                    }
                    else
                        c.Enabled = enabled;
                    return;
                }
            }

           // MessageBox.Show("ClientInterface is null. Player is connected with no form!");
        }
    }
}
