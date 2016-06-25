using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETBoggle.Networking
{
    /// <summary>
    /// Handler class for a single boggle die.
    /// </summary>
    public class BoggleDie
    {
        const int NUM_LETTERS = 6;

        public string[] Letters = new string[NUM_LETTERS];

        public string CurrentLetter = string.Empty;

        public Tuple<int, int> Position = new Tuple<int, int>(0, 0);

        public BoggleDie(string[] letters)
        {
            Letters = letters;
        }

        public void SetPosition(int x, int y)
        {
            Position = new Tuple<int, int>(x, y);
        }

        /// <summary>
        /// Get a random letter from this die.
        /// </summary>
        /// <returns>A random letter from this die.</returns>
        public string GetRandLetter()
        {
            Random r = new Random();
            CurrentLetter = Letters[r.Next(0, NUM_LETTERS - 1)];
            return CurrentLetter;
        }
    }

    /// <summary>
    /// A massive horrific hack for the JSON Parser.
    /// </summary>
    public struct DiceWrapper
    {
        public string[] Die0, Die1, Die2, Die3, Die4, Die5, Die6, Die7, Die8, Die9, Die10, Die11, Die12, Die13, Die14, Die15; 
    }
}
