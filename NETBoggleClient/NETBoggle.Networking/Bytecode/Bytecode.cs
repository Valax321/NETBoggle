using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Diagnostics;

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
        /// Send a message to the client's debugger.
        /// </summary>
        SERVER_CLIENT_MESSAGE = 254, 
        /// <summary>
        /// UNUSED, error catching.
        /// </summary>
        INVALID = 255 
    }

    /// <summary>
    /// Bytecode generator/interpreter
    /// </summary>
    /// <typeparam name="T">Type for parameter 1</typeparam>
    /// <typeparam name="T2">Type for parameter 2</typeparam>
    public static class Bytecode<T, T2>
    {       
        const char StringTerminator = (char)0x03; //ETX character (end of text), hex 0x03

        /// <summary>
        /// Bind a method with two parameters to be called on the given instruction. When the assigned BoggleInstruction is parsed by the bytecode interpreter, the specified method is called with the code's instructions as the parameters.
        /// </summary>
        /// <param name="binding">BoggleInstructions command to bind to.</param>
        /// <param name="bindmethod">A two parameter method to bind to the specified boggle instruction</param>
        public static void BindInstruction(BoggleInstructions binding, Action<T, T2> bindmethod)
        {
            try
            {
                BindingsList.Add(binding, bindmethod);
            }
            catch
            {
                //Throw an error or something.
            }
        }

        /// <summary>
        /// Unbinds the specified instruction from any currently bound methods.
        /// </summary>
        /// <param name="ins">Instruction to unbind.</param>
        public static void UnbindInstruction(BoggleInstructions ins)
        {
            BindingsList.Remove(ins);
        }

        static Dictionary<BoggleInstructions, Action<T, T2>> BindingsList = new Dictionary<BoggleInstructions,Action<T,T2>>();

        /// <summary>
        /// Paramater types must correspond to the types that the binding was declared with
        /// </summary>
        /// <param name="raw_instruction"></param>
        public static void Parse(string raw_instruction)
        {
            byte player_index = (byte)char.GetNumericValue(raw_instruction[0]); //WHY IS THAT NOT STRAIGHTFORWARD?
            BoggleInstructions instruction = BoggleInstructions.INVALID;
            try
            {
                instruction = (BoggleInstructions)Enum.ToObject(typeof(BoggleInstructions), (byte)raw_instruction[1]);
            }
            catch (Exception e)
            {
                Debug.Assert(false, e.ToString());
            }

            T instruction1;
            T2 instruction2;

            string s2 = raw_instruction.Remove(0, 2);

            try
            {
                instruction1 = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(s2.Split(new char[] { StringTerminator }, StringSplitOptions.RemoveEmptyEntries)[0]);
                instruction2 = (T2)TypeDescriptor.GetConverter(typeof(T2)).ConvertFromString(s2.Split(new char[] { StringTerminator }, StringSplitOptions.RemoveEmptyEntries)[1]);
            }

            catch (Exception e)
            {
                Debug.Assert(false, e.ToString());
                return;
            }

            Console.WriteLine("Player: " + player_index);
            Console.WriteLine("Instruction: " + Enum.GetName(typeof(BoggleInstructions), instruction));
            Console.WriteLine("Parameter 1: " + instruction1);
            Console.WriteLine("Paramater 2: " + instruction2);

            try
            {
                BindingsList[instruction].Invoke(instruction1, instruction2);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
            return output;
        }
    }
}
