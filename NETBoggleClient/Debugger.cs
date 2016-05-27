using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NETBoggleClient
{
    public partial class Debugger : Form
    {
        public Debugger()
        {
            InitializeComponent();
        }
    }

    public static class Debug
    {
        static Debugger CurLog;

        public static void Log(string text)
        {
            if (CurLog != null)
            {

            }
        }

        public static void SetupLog(Debugger log)
        {
            CurLog = log;
            log.Show();
        }
    }
}
