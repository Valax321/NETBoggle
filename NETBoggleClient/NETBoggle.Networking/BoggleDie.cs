using System;

namespace NETBoggle.Networking
{
    /// <summary>
    /// Handler class for a single boggle die.
    /// </summary>
    public class BoggleDie
    {
        const int NUM_LETTERS = 6;

        /// <summary>
        /// Potential letters for the die.
        /// </summary>
        public string[] Letters = new string[NUM_LETTERS];

        /// <summary>
        /// Current letter from the random list.
        /// </summary>
        public string CurrentLetter = string.Empty;

        /// <summary>
        /// The current 2d location for this die.
        /// </summary>
        public Tuple<int, int> Position = new Tuple<int, int>(0, 0);

        /// <summary>
        /// Has this die been used for a word yet?
        /// </summary>
        public bool Consumed;

        /// <summary>
        /// Constructor for die.
        /// </summary>
        /// <param name="letters">Letters this die could potentially be.</param>
        public BoggleDie(string[] letters)
        {
            Letters = letters;
        }

        /// <summary>
        /// Set the position of this die.
        /// </summary>
        /// <param name="x">the x coord to set.</param>
        /// <param name="y">the y coord to set.</param>
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
        /// <summary>
        /// Die wrapper
        /// </summary>
        public string[] Die0, Die1, Die2, Die3, Die4, Die5, Die6, Die7, Die8, Die9, Die10, Die11, Die12, Die13, Die14, Die15; 
    }
}
