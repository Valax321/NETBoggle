using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NETBoggle.Networking
{
    /// <summary>
    /// Server running the game backend.
    /// </summary>
    public class Server
    {
        public const int PLAYER_CAP = 50; // Max players per server.
        const int GameLength = 60; //Length in seconds

        /// <summary>
        /// Initialise a new server
        /// </summary>
        /// <param name="name">the hosted server name</param>
        /// <param name="password">optional server password</param>
        public Server(string name, string password)
        {
            ServerName = name;
            ServerPassword = password;
        }

        /// <summary>
        /// Get the number of players on the server.
        /// </summary>
        public int PlayerCount
        {
            get
            {
                return Players.Count;
            }
        }

        public string ServerName = "Boggle Server";
        public string ServerPassword = string.Empty;

        public List<Player> Players = new List<Player>(PLAYER_CAP);

        public void ConnectPlayer(Player new_player)
        {
            try
            {
                Players.Add(new_player);
            }

            catch (Exception e)
            {
                throw new ServerFullException(string.Format("Current server {0} is full", ServerName), e);
            }
        }
        
        public void Tick(float tick)
        {
            
        }     

    }

    public class ServerFullException : Exception
    {
        public ServerFullException() { }

        public ServerFullException(string message) : base(message) { }

        public ServerFullException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Server client
    /// </summary>
    public class Player
    {
        public string PlayerName { get; set; }

        public uint Score { get; set; }
    }
}
