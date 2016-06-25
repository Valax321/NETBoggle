using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NETBoggle.Networking.Bytecode
{
    public enum BoggleInstructions
    {
        CONNECT = 0,
        READY = 1,
        SHUFFLE = 2,
        DISCONNECT = 3,
        FORMTEXT = 4,
        FORMSTATE = 5,
        SENDWORD = 6,
        SHUTDOWN = 7,
        TICK = 8
    }

    public static class Bytecode
    {       
        public const char StringTerminator = (char)0x03; //ETX character (end of text), hex 0x03

        public static void Parse<T, T2>(string raw_instruction)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(raw_instruction);

            byte player_index = bytes[0];
            BoggleInstructions instruction = (BoggleInstructions)Enum.ToObject(typeof(BoggleInstructions), bytes[1]);

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
            Console.WriteLine("Instruction: " + instruction);
            Console.WriteLine("Parameter 1: " + instruction1);
            Console.WriteLine("Paramater 2: " + instruction2);

        }        

        public static string Generate(BoggleInstructions ins, string param1, string param2 = "", int player_index = 0)
        {
            string output = string.Empty;
            output += player_index;
            output += (int)ins;
            output += param1 + StringTerminator;
            output += param2 + StringTerminator;
            return output;
        }

    }

}
