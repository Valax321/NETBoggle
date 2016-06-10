using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETBoggle.Networking
{
    class BoggleDie
    {
        const int NUM_LETTERS = 6;

        public string[] Letters = new string[NUM_LETTERS];

        public BoggleDie(string[] letters)
        {
            Letters = letters;
        }

        /// <summary>
        /// Get a random letter from this die.
        /// </summary>
        /// <returns>A random letter from this die.</returns>
        public string GetRandLetter()
        {
            Random r = new Random();
            return Letters[r.Next(0, NUM_LETTERS - 1)];
        }
    }

    public struct DiceWrapper
    {
        public string[] Die0, Die1, Die2, Die3, Die4, Die5, Die6, Die7, Die8, Die9, Die10, Die11, Die12, Die13, Die14, Die15; 
    }
}
