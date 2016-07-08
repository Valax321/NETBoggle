using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

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

        static Bytecode()
        {
            BindInstruction(BoggleInstructions.CONNECT, Connect);
            BindInstruction(BoggleInstructions.SERVER_CLIENT_MESSAGE, ClientServerMessage);
            BindInstruction(BoggleInstructions.SENDWORD, ReceiveWord);
            BindInstruction(BoggleInstructions.FORMSTATE, FormSetVal);
            BindInstruction(BoggleInstructions.FORMTEXT, FormSetText);
            BindInstruction(BoggleInstructions.READY, ClientReady);
            BindInstruction(BoggleInstructions.READY_RESPONSE, ServerRespondReady);
            BindInstruction(BoggleInstructions.SET_NAME, ClientSetName);
        }

        static void Connect(string p1, string p2)
        {
            ConnectClient();
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

        /// <summary>
        /// Called when we connect to a client/server
        /// </summary>
        public static event BytecodeEventHandler ConnectClient;
        /// <summary>
        /// Called when we receive a text message from the server
        /// </summary>
        public static event Bytecode_OneParam_EventHandler<string> SendMessage;

        public static event Server_OneParam_EventHandler<string> ServerReveiveWord;

        public static event ServerEventHandler ClientClickReady;

        public static event BytecodeEventHandler ServerRespondClickReady;

        public static event Server_OneParam_EventHandler<string> SetClientName;

        public static event Bytecode_TwoParam_EventHandler<string, bool> ClientSetFormState;

        public static event Bytecode_TwoParam_EventHandler<string, string> ClientSetFormText;

        static Dictionary<BoggleInstructions, Action<string, string>> InstructionBindings = new Dictionary<BoggleInstructions, Action<string, string>>();
        static Dictionary<BoggleInstructions, Action<string, string, Player>> InstructionBindings_Server = new Dictionary<BoggleInstructions, Action<string, string, Player>>();

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
                instruction1 = s2.Split(new char[] { StringTerminator })[0];
                instruction2 = s2.Split(new char[] { StringTerminator })[1];
            }

            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Exception Thrown", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

            catch (Exception e)
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
