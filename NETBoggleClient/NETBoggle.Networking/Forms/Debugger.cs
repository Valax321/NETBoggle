using System;
using System.Windows.Forms;

namespace NETBoggle.Networking
{
    public partial class Debugger : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Debugger()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Log text to our console
        /// </summary>
        /// <param name="text"></param>
        public void Log(string text)
        {
            textboxDebugLog.Text += text + Environment.NewLine;
        }

        /// <summary>
        /// Update the log with our current state.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="e"></param>
        public void UpdateStateLog(IBoggleState state, EventArgs e)
        {
            labelServerState.Text = string.Format("Server State: {0}", state);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    /// <summary>
    /// Handles logging text to the log window/output in VS
    /// </summary>
    public static class Debug
    {
        static Debugger CurLog;

        /// <summary>
        ///Current Debugger, public access.
        /// </summary>
        public static Debugger DebugLog
        {
            get
            {
                if (CurLog != null)
                {
                    return CurLog;
                }
                else
                {
                    MessageBox.Show("Attempted to access null log screen");
                    return null;
                }
            }
        }

        /// <summary>
        /// Log text to the console
        /// </summary>
        /// <param name="text">text to log</param>
        public static void Log(string text)
        {
            if (CurLog != null)
            {
                CurLog.Log(text);              
            }

            Console.WriteLine(text);
        }

        /// <summary>
        /// Initialise the log. Use this before any Log() calls are made!
        /// </summary>
        /// <param name="log">A log to create</param>
        public static void SetupLog(Debugger log)
        {
            CurLog = log;
            log.Show();
        }
    }
}
