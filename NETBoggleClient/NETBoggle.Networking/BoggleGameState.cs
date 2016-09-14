using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> origin/master
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
        /// <summary>
        /// Nothing.
        /// </summary>
        public void Construct(Server cur_s)
        {

        }

        /// <summary>
        /// Waits for player ready.
        /// </summary>
        /// <param name="cur_s"></param>
        /// <returns></returns>
        public IBoggleState Handle(Server cur_s)
        {
            bool all_ready = true;
            int ready_count = 0;

            foreach (Player p in cur_s.Players)
            {
                if (!p.Ready) // Check ready state of all players, update their forms
                {
                    all_ready = false;
                    //p.SetElementEnabled("buttonReadyRound", true);
                    cur_s.NetMSG_SetFormState("buttonReadyRound", true, p);
                }
                else
                {
                    //p.SetElementEnabled("buttonReadyRound", false);
                    cur_s.NetMSG_SetFormState("buttonReadyRound", false, p);
                    ready_count++;
                }
            }

            //foreach (Player p in cur_s.Players)
            //{
            //    //p.SetElementText("labelReadyPlayers", string.Format("{0}/{1} Ready", ready_count, cur_s.Players.Count));

            //}

<<<<<<< HEAD
            cur_s.NetMSG_SetFormText("labelReadyPlayers", string.Format("{0}/{1} Ready", ready_count, cur_s.Players.Count)); // Tell players how many people are ready
=======
            cur_s.NetMSG_SetFormText("labelReadyPlayers", string.Format("{0}/{1} Ready", ready_count, cur_s.Players.Count));
>>>>>>> origin/master

            if (!all_ready)
            {
                return null;
            }

            foreach (Player p in cur_s.Players)
            {
<<<<<<< HEAD
                p.Ready = false; //Clear the ready flag
            }

            return new BogglePlay(); //Move onto gameplay
=======
                p.Ready = false;
            }

            return new BogglePlay();
>>>>>>> origin/master
        }
    }

    /// <summary>
    /// This state handles the actual gameplay for Boggle.
    /// First, construct randomises the dice.
    /// Then, every frame we tick down the timer until we reach 0, then we move to the tally state.
    /// </summary>
    public class BogglePlay : IBoggleState
    {
        /// <summary>
        /// The current delta time for the server.
        /// </summary>
        public float CurTime = Server.GameLength;

        /// <summary>
        /// Called when this dice is initialised.
        /// </summary>
        /// <param name="cur_s"></param>
        public void Construct(Server cur_s)
        {
<<<<<<< HEAD
            cur_s.NetMSG_SetFormText("wordsBox.textBoxWordHistory", string.Empty); //Clear word history for the last round
=======
            //foreach (Player p in cur_s.Players)
            //{
            //    //p.SetElementEnabled("wordsBox", true); //HACKHACKHACK
            //}

>>>>>>> origin/master
            cur_s.NetMSG_SetFormState("wordsBox", true);

            int row = 1;
            int col = 1;

            cur_s.RandomiseDicePositions();

            foreach (BoggleDie bd in cur_s.DiceLetters)
            {
                string letter = bd.GetRandLetter();
                //foreach (Player p in cur_s.Players)
                //{
                //    p.SetElementText(string.Format("boxBoard.letter_r{0}c{1}", row, col), letter);
                //}

<<<<<<< HEAD
                cur_s.NetMSG_SetFormText(string.Format("boxBoard.letter_r{0}c{1}", row, col), letter); // Look up each letter on the game form.
=======
                cur_s.NetMSG_SetFormText(string.Format("boxBoard.letter_r{0}c{1}", row, col), letter);
>>>>>>> origin/master

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

        /// <summary>
        /// Called when this die ticks.
        /// </summary>
        /// <param name="cur_s"></param>
        /// <returns></returns>
        public IBoggleState Handle(Server cur_s) //Fires every tick.
        {
            if (CurTime <= 0)
            {
                //foreach (Player p in cur_s.Players)
                //{
                //    p.SetElementEnabled("wordsBox", false);
                //    p.SetElementText("wordsBox.textBoxWordInput", string.Empty);
                //    p.SetElementText("wordsBox.textBoxWordHistory", string.Empty);
                //}

                cur_s.NetMSG_SetFormState("wordsBox", false);
                cur_s.NetMSG_SetFormText("wordsBox.textBoxWordInput", string.Empty);
<<<<<<< HEAD

                return new BoggleRoundEnd(); //Move on to scoring
=======
                cur_s.NetMSG_SetFormText("wordsBox.textBoxWordHistory", string.Empty);

                return new BoggleRoundEnd();
>>>>>>> origin/master
            }
            else
            {
                CurTime -= cur_s.DeltaTime;

<<<<<<< HEAD
                string DisplayTime = string.Format("{0}", (CurTime > 0 ? ((int)CurTime).ToString() : "0")); //Update the player's timer
=======
                string DisplayTime = string.Format("{0}", (CurTime > 0 ? ((int)CurTime).ToString() : "0"));
>>>>>>> origin/master

                //foreach (Player p in cur_s.Players)
                //{
                //    p.SetElementText("lblTimeRemain", DisplayTime);
                //}

                cur_s.NetMSG_SetFormText("lblTimeRemain", DisplayTime);

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
        /// <summary>
        /// Words shared by multiple players, disregarded.
        /// </summary>
        public List<string> CommonWords = new List<string>();
<<<<<<< HEAD
        Dictionary<string, uint> CWords = new Dictionary<string, uint>();
=======
>>>>>>> origin/master

        /// <summary>
        /// Called on init
        /// </summary>
        /// <param name="cur_s"></param>
        public void Construct(Server cur_s)
        {
<<<<<<< HEAD
            //!!!!!!!!TODO: Fix common word detection !!!!!
            foreach (Player p in cur_s.Players)
            {
                try
                {
                    foreach (string s in p.TypedWords)
                    {
                        if (!CWords.ContainsKey(s))
                        {
                            CWords.Add(s, 0); //Add it to the register of common words
                        }
                        else
                        {
                            CWords[s]++;
                        }
                    }

                    //for (int i = p.TypedWords.Count - 1; i >= 0; i--) // Hooray backwards iteration
                    //{
                    //    string s = p.TypedWords[i];

                    //    if (!CommonWords.Contains(s))
                    //    {
                    //        CommonWords.Add(s);
                    //    }
                    //    else
                    //    {
                    //        p.TypedWords.Remove(s);
                    //    }
                    //}

                }

                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Word error");
=======
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
>>>>>>> origin/master
                }
            }

            foreach (Player p in cur_s.Players)
            {
                int temp_score = 0;

<<<<<<< HEAD
                foreach (string common in CWords.Keys)
                {
                    if (CWords[common] == 0) continue;
                    try
                    {
                        p.TypedWords.Remove(common);
                    }
                    catch
                    {
                        Debug.Log("Common word removal error. Ignoring.");
                    }
                }

=======
>>>>>>> origin/master
                // Problems here:
                // Score can get unreasonably large after quite a few rounds (e.g. 19 rather than 5).

                foreach (string word in p.TypedWords)
                {
                    if (word.Length <= 4)
                    {
                        p.Score += 1;
                        temp_score += 1;
                    }
                    else if (word.Length == 5)
                    {
                        p.Score += 2;
                        temp_score += 2;
                    }
                    else if (word.Length == 6)
                    {
                        p.Score += 3;
                        temp_score += 3;
                    }
                    else if (word.Length == 7)
                    {
                        p.Score += 5;
                        temp_score += 5;
                    }
                    else
                    {
                        p.Score += 11;
                        temp_score += 11;
                    }
<<<<<<< HEAD

                    cur_s.BroadcastInstruction(Bytecode.BoggleInstructions.PLAYER_SCORE, p.PlayerName ,p.Score.ToString()); //Tell the players the new scores.
=======
>>>>>>> origin/master
                }

                //MessageBox.Show(temp_score.ToString(), "Your Score This Round"); //ADD SOMETHING HERE!!!!!!!
            }
        }

        /// <summary>
        /// Nothing, returns new state.
        /// </summary>
        /// <param name="cur_s"></param>
        /// <returns></returns>
        public IBoggleState Handle(Server cur_s)
        {
            return new BoggleWaitReady();
        }
    }
}
