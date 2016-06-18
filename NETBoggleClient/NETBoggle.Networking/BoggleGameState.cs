using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NETBoggle.Networking
{
    /// <summary>
    /// Base interface for the Boggle Game's FSM (Finite State Machine)
    /// For more on FSMs, see https://en.wikipedia.org/wiki/Finite-state_machine
    /// </summary>
    public interface IBoggleState
    {
        /// <summary>
        /// Handles a single game tick.
        /// </summary>
        /// <param name="cur_s">The server we belong to.</param>
        /// <returns>A new state if we changed, else null.</returns>
        IBoggleState Handle(Server cur_s);

        /// <summary>
        /// Handles the construction of this state. Called on the tick we are created.
        /// </summary>
        /// <param name="cur_s">Our server</param>
        void Construct(Server cur_s);
    }

    /// <summary>
    /// This state waits for all players to click their 'ready' buttons on their game forms.
    /// When everyone is ready, it returns a gameplay state.
    /// </summary>
    public class BoggleWaitReady : IBoggleState
    {
        public void Construct(Server cur_s)
        {

        }

        public IBoggleState Handle(Server cur_s)
        {
            bool all_ready = true;
            int ready_count = 0;

            foreach (Player p in cur_s.Players)
            {
                if (!p.Ready)
                {
                    all_ready = false;
                    p.SetElementEnabled("buttonReadyRound", true);
                }
                else
                {
                    p.SetElementEnabled("buttonReadyRound", false);
                    ready_count++;
                }
            }

            foreach (Player p in cur_s.Players)
            {
                p.SetElementText("labelReadyPlayers", string.Format("{0}/{1} Ready", ready_count, cur_s.Players.Count));
            }

            if (!all_ready)
            {
                return null;
            }

            foreach (Player p in cur_s.Players)
            {
                p.Ready = false;
            }

            return new BogglePlay();
        }
    }

    /// <summary>
    /// This state handles the actual gameplay for Boggle.
    /// First, construct randomises the dice.
    /// Then, every frame we tick down the timer until we reach 0, then we move to the tally state.
    /// </summary>
    public class BogglePlay : IBoggleState
    {
        public float CurTime = Server.GameLength;

        public void Construct(Server cur_s)
        {
            foreach (Player p in cur_s.Players)
            {
                p.SetElementEnabled("wordsBox", true); //HACKHACKHACK
            }

            int row = 1;
            int col = 1;

            cur_s.RandomiseDicePositions();

            foreach (BoggleDie bd in cur_s.DiceLetters)
            {
                string letter = bd.GetRandLetter();
                foreach (Player p in cur_s.Players)
                {
                    p.SetElementText(string.Format("boxBoard.letter_r{0}c{1}", row, col), letter);
                }

                if (row < 4)
                {
                    row++;
                }
                else
                {
                    row = 1;
                    col++;
                }
            }
        }

        public IBoggleState Handle(Server cur_s) //Fires every tick.
        {
            if (CurTime <= 0)
            {
                foreach (Player p in cur_s.Players)
                {
                    p.SetElementEnabled("wordsBox", false);
                    p.SetElementText("wordsBox.textBoxWordInput", string.Empty);
                    p.SetElementText("wordsBox.textBoxWordHistory", string.Empty);
                }
                return new BoggleRoundEnd();
            }
            else
            {
                CurTime -= cur_s.DeltaTime;

                string DisplayTime = string.Format("{0}", (CurTime > 0 ? ((int)CurTime).ToString() : "0"));

                foreach (Player p in cur_s.Players)
                {
                    p.SetElementText("lblTimeRemain", DisplayTime);
                }

                return null;
            }
        }
    }

    // Boggle scoring:
    // 4 or < letters: 1 point
    // 5 letters: 2
    // 6 letters: 3
    // 7 letters: 5
    // > 7 letters: 11

        /// <summary>
        /// Handles the score calculations for Boggle.
        /// </summary>
    public class BoggleRoundEnd : IBoggleState
    {
        public List<string> CommonWords = new List<string>();

        public void Construct(Server cur_s)
        {
            foreach (Player p in cur_s.Players)
            {
                foreach (string s in p.TypedWords)
                {
                    if (!CommonWords.Contains(s))
                    {
                        CommonWords.Add(s); //Add it to the register of common words
                    }
                    else
                    {
                        p.TypedWords.Remove(s); //Duplicated word, get rid of it.
                    }
                }
            }

            foreach (Player p in cur_s.Players)
            {
                foreach (string word in p.TypedWords)
                {
                    if (word.Length <= 4)
                    {
                        p.Score += 1;
                    }
                    else if (word.Length == 5)
                    {
                        p.Score += 2;
                    }
                    else if (word.Length == 6)
                    {
                        p.Score += 3;
                    }
                    else if (word.Length == 7)
                    {
                        p.Score += 5;
                    }
                    else
                    {
                        p.Score += 11;
                    }
                }

                MessageBox.Show(p.Score.ToString(), "Your Score This Round");
            }
        }

        public IBoggleState Handle(Server cur_s)
        {
            return new BoggleWaitReady();
        }
    }
}
