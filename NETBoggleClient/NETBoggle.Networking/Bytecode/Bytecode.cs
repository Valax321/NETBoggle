using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NETBoggle.Networking.Bytecode
{
    /// <summary>
    /// Bytecode instructions for the Boggle networking.
    /// </summary>
    public enum BoggleInstructions
    {
        /// <summary>
        /// When we want to connect to the server, sent by client
        /// </summary>
        CONNECT = 0, 
        /// <summary>
        /// When the player clicks ready, to the server
        /// </summary>
        READY = 1, 
        /// <summary>
        /// When the server aknowedges a ready click
        /// </summary>
        READY_RESPONSE = 100,
        /// <summary>
        /// When we shuffle the dice, to the clients
        /// </summary>
        SHUFFLE = 2, 
        /// <summary>
        /// When we want to leave the server, to the player
        /// </summary>
        DISCONNECT = 3, 
        /// <summary>
        /// Set form text element, to the client
        /// </summary>
        FORMTEXT = 4, 
        /// <summary>
        /// Set the enabled state of a form, to the client
        /// </summary>
        FORMSTATE = 5, 
        /// <summary>
        /// Send a word to the server, to the server
        /// </summary>
        SENDWORD = 6, 
        /// <summary>
        /// Shut down the server, disconnect clients
        /// </summary>
        SHUTDOWN = 7, 
        /// <summary>
        /// Tick the client's form
        /// </summary>
        TICK = 8, 
        /// <summary>
        /// request a password from the client to connect
        /// </summary>
        PASSWORD_REQUEST = 9, 
        /// <summary>
        /// Send our password to the server
        /// </summary>
        PASSWORD_REPLY = 10,        
        /// <summary>
        /// Set/init our player name.
        /// </summary>
        SET_NAME = 11, 
        /// <summary>
        /// When the server closes
        /// </summary>
        SERVER_SHUTDOWN = 12,
        /// <summary>
        /// When a player joins the server.
        /// </summary>
        PLAYER_JOINED = 13,
        /// <summary>
        /// When a player leaves the server
        /// </summary>
        PLAYER_LEFT = 14,
        /// <summary>
        /// Update the player's score.
        /// </summary>
        PLAYER_SCORE = 15,
        /// <summary>
        /// Send a message to the client's debugger.
        /// </summary>
        SERVER_CLIENT_MESSAGE = 253, 
        /// <summary>
        /// UNUSED, error catching.
        /// </summary>
        INVALID = 255 
    }

    /// <summary>
    /// Bytecode generator/interpreter
    /// </summary>
    public static class Bytecode
    {       
        const char StringTerminator = (char)0x03; //ETX character (end of text), hex 0x03

        /// <summary>
        /// Here we bind a method with parameters of string and string to a parsed instruction. When bound, <see cref="Parse(string, Player)"/> will call the appropriate method.
        /// These methods will call an appropriate <see cref="BytecodeEventHandler"/> or <see cref="ServerEventHandler"/> (or their parameterised equivalents) that will execute on the client or server, respectively.
        /// </summary>
        /// <param name="b">Instruction to bind.</param>
        /// <param name="method">Method to bind to.</param>
        static void BindInstruction(BoggleInstructions b, Action<string, string> method)
        {
            try
            {
                InstructionBindings.Add(b, method);
            }
            catch
            {
                Debug.Log(string.Format("Failed to add binding {0} with method {1}", b, nameof(method)));
            }
        }
        /// <summary>
        /// Here we bind a method with parameters of string, string and <see cref="Player"/> to a parsed instruction. When bound, <see cref="Parse(string, Player)"/> will call the appropriate method.
        /// These methods will call an appropriate <see cref="BytecodeEventHandler"/> or <see cref="ServerEventHandler"/> (or their parameterised equivalents) that will execute on the client or server, respectively.
        /// </summary>
        /// <param name="b">Instruction to bind.</param>
        /// <param name="method">Method to bind to.</param>
        static void BindInstruction(BoggleInstructions b, Action<string, string, Player> method)
        {
            try
            {
                InstructionBindings_Server.Add(b, method);
            }
            catch
            {
                Debug.Log(string.Format("Failed to add binding {0} with method {1}", b, nameof(method)));
            }
        }

        static Bytecode() //Binds everything at launch. Not very efficient, but it works.
        {
            BindInstruction(BoggleInstructions.CONNECT, Connect);
            BindInstruction(BoggleInstructions.SERVER_CLIENT_MESSAGE, ClientServerMessage);
            BindInstruction(BoggleInstructions.SENDWORD, ReceiveWord);
            BindInstruction(BoggleInstructions.FORMSTATE, FormSetVal);
            BindInstruction(BoggleInstructions.FORMTEXT, FormSetText);
            BindInstruction(BoggleInstructions.READY, ClientReady);
            BindInstruction(BoggleInstructions.READY_RESPONSE, ServerRespondReady);
            BindInstruction(BoggleInstructions.SET_NAME, ClientSetName);
            BindInstruction(BoggleInstructions.SERVER_SHUTDOWN, ServerClose);
            BindInstruction(BoggleInstructions.PLAYER_JOINED, ServerAddClientList);
            BindInstruction(BoggleInstructions.PLAYER_LEFT, ServerRemoveClientList);
            BindInstruction(BoggleInstructions.PLAYER_SCORE, ServerSetClientScore);
        }

        #region Binding Methods

        static void Connect(string p1, string p2)
        {
            ConnectClient();
        }

        static void ServerClose(string p1, string p2)
        {
            ServerShutDown();
        }

        static void ServerAddClientList(string p1, string p2)
        {
            ClientAddList(p1);
        }

        static void ServerRemoveClientList(string p1, string p2)
        {
            ClientRemoveList(p2);
        }

        static void ServerSetClientScore(string p1, string p2)
        {
            ClientSetScore(p1, p2.ConvertToType<uint>());
        }

        static void ClientServerMessage(string p1, string p2)
        {
            SendMessage(p1);
        }

        static void ClientReady(string p1, string p2, Player p)
        {
            ClientClickReady(p);
        }

        static void ServerRespondReady(string p1, string p2)
        {
            ServerRespondClickReady();
        }

        static void ClientSetName(string p1, string p2, Player p)
        {
            SetClientName(p1, p);
        }

        static void ReceiveWord(string word, string dumb, Player p)
        {
            ServerReveiveWord(word, p);
        }

        static void FormSetVal(string elem, string bState)
        {
            //Debug.Log(string.Format("Setting {0} to value {1}", elem, bState));
            bool State = bState.ConvertToType<bool>();
            ClientSetFormState(elem, State);
        }

        static void FormSetText(string elem, string text_val)
        {
            ClientSetFormText(elem, text_val);
        }

        #endregion

        #region Client Bindings

        /// <summary>
        /// Event handler for two parameter message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public delegate void Bytecode_TwoParam_EventHandler<T, T2>(T p1, T2 p2);

        /// <summary>
        /// Event handler for one parameter message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        public delegate void Bytecode_OneParam_EventHandler<T>(T param);

        /// <summary>
        /// Event handler for no parameter message
        /// </summary>
        public delegate void BytecodeEventHandler();

        #endregion

        #region Server-Specific Bindings
        /// <summary>
        /// Server binding two param (see Bytecode_TwoParam_EventHandler)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="exec"></param>
        public delegate void Server_TwoParam_EventHandler<T, T2>(T p1, T2 p2, Player exec);
        /// <summary>
        /// Server binding one param (see Bytecode_OneParam_EventHandler)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="exec"></param>
        public delegate void Server_OneParam_EventHandler<T>(T param, Player exec);
        /// <summary>
        /// Server binding no params (see <see cref="BytecodeEventHandler"/>)
        /// </summary>
        /// <param name="exec"></param>
        public delegate void ServerEventHandler(Player exec);

        #endregion

        #region Bound events

        /// <summary>
        /// Called when we connect to a client/server
        /// </summary>
        public static event BytecodeEventHandler ConnectClient;
        /// <summary>
        /// Called when we receive a text message from the server
        /// </summary>
        public static event Bytecode_OneParam_EventHandler<string> SendMessage;
        /// <summary>
        /// Called when we receive a new word from the client
        /// </summary>
        public static event Server_OneParam_EventHandler<string> ServerReveiveWord;
        /// <summary>
        /// Called when a client clicks their ready button.
        /// </summary>
        public static event ServerEventHandler ClientClickReady;
        /// <summary>
        /// Called on client when the server aknowledges their ready state
        /// </summary>
        public static event BytecodeEventHandler ServerRespondClickReady;
        /// <summary>
        /// Called on client when a new player arrives
        /// </summary>
        public static event Bytecode_OneParam_EventHandler<string> ClientAddList;
        /// <summary>
        /// Called on a client when a player leaves
        /// </summary>
        public static event Bytecode_OneParam_EventHandler<string> ClientRemoveList;
        /// <summary>
        /// Called on a client when the scores change
        /// </summary>
        public static event Bytecode_TwoParam_EventHandler<string, uint> ClientSetScore;
        /// <summary>
        /// Called when a client changes their name
        /// </summary>
        public static event Server_OneParam_EventHandler<string> SetClientName;
        /// <summary>
        /// Called on a client when their form should set the <see cref="System.Windows.Forms.Control.Enabled"/> value.
        /// </summary>
        public static event Bytecode_TwoParam_EventHandler<string, bool> ClientSetFormState;
        /// <summary>
        /// Called on a client when their form should set the <see cref="System.Windows.Forms.Control.Text"/> value.
        /// </summary>
        public static event Bytecode_TwoParam_EventHandler<string, string> ClientSetFormText;
        /// <summary>
        /// Called on a client when their server closes
        /// </summary>
        public static event BytecodeEventHandler ServerShutDown;

        #endregion

        static Dictionary<BoggleInstructions, Action<string, string>> InstructionBindings = new Dictionary<BoggleInstructions, Action<string, string>>(); // Client-side bindings
        static Dictionary<BoggleInstructions, Action<string, string, Player>> InstructionBindings_Server = new Dictionary<BoggleInstructions, Action<string, string, Player>>(); //Server-side bindings

        /// <summary>
        /// Convert a string to the specified type
        /// </summary>
        /// <typeparam name="RT"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        static RT ConvertToType<RT>(this string param)
        {
            return (RT)TypeDescriptor.GetConverter(typeof(RT)).ConvertFromString(param);
        }

        /// <summary>
        /// Paramater types must correspond to the types that the binding was declared with
        /// </summary>
        /// <param name="raw_instruction"></param>
        /// <param name="p"></param>
        public static void Parse(string raw_instruction, Player p = null)
        {
            byte player_index = (byte)char.GetNumericValue(raw_instruction[0]); //deprecated??
            BoggleInstructions instruction = BoggleInstructions.INVALID;
            try
            {
                instruction = (BoggleInstructions)Enum.ToObject(typeof(BoggleInstructions), (byte)raw_instruction[1]);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
           
            string s2 = raw_instruction.Remove(0, 2);

            string instruction1 = string.Empty;
            string instruction2 = string.Empty;

            try
            {
                instruction1 = s2.Split(new char[] { StringTerminator })[0]; // Get the first parameter
                instruction2 = s2.Split(new char[] { StringTerminator })[1]; // Get the second parameter
            }

            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Exception Thrown", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); // Serious error, interrupt us.
                return;
            }

            try
            {
                if (p == null) //Null because we're a client and don't have a player ref.
                {
                    InstructionBindings[instruction].Invoke(instruction1, instruction2); //Client-side binding.
                }
                else
                {
                    InstructionBindings_Server[instruction].Invoke(instruction1, instruction2, p); //Server-side binding
                }
            }

            catch //(Exception e)
            {
                //Debug.Log(e.ToString());
                Debug.Log(string.Format("Key not found: {0}", instruction.ToString()));
            }
        }        

        /// <summary>
        /// Generate Boggle network bytecode for sending over packets to clients and/or servers.
        /// </summary>
        /// <param name="ins">Instruction to pack into the data.</param>
        /// <param name="param1">Parameter 1 to pack into the data. Can be anything.</param>
        /// <param name="param2">Parameter 2 to pack into the data. Can be anything.</param>
        /// <param name="player_index">The player index to pack into the data (mainly for client->server interaction).</param>
        /// <returns></returns>
        public static string Generate(BoggleInstructions ins, string param1, string param2 = "", int player_index = 0)
        {
            string output = string.Empty;
            output += player_index;
            output += (char)ins; //needs to be char, or else we get the number.
            output += param1 + StringTerminator;
            output += param2 + StringTerminator;
            output += '\n';
            return output;
        }
    }
}
