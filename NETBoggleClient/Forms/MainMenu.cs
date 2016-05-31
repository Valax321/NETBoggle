using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NETBoggle.Client
{
    public partial class MainMenu : Form
    {
        public MainMenu(string[] progargs)
        {
            InitializeComponent();

            foreach (string s in progargs)
            {
                if (s == "-debug")
                {
                    Debug.SetupLog(new Debugger());
                }
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            PlayerSettings.LoadPlayerSettings();
            name.Text = PlayerSettings.Settings.PlayerName;
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            PlayerSettings.SavePlayerSettings();
        }
    }
}
