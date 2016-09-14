using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Text;
=======
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> origin/master
using NETBoggle.Networking.Bytecode;
using System.Net;
using System.Net.Sockets;
using RedCorona.Net;

namespace NETBoggle.Networking
{
<<<<<<< HEAD
    /// <summary>
    /// Parent class for both Network types
    /// </summary>
    public static class NetworkTools
    {
        /// <summary>
        /// Constant port what the game runs on.
        /// </summary>
        public const int PORT = 7654;

        /// <summary>
        /// A client for the client/server system.
        /// </summary>
=======
    public static class NetworkTools
    {
        public const int PORT = 7654;

>>>>>>> origin/master
        public class NetClient
        {
            ClientInfo Client;
            Socket sock;

<<<<<<< HEAD
            /// <summary>
            /// Initialises the client
            /// </summary>
            /// <param name="connectionip">Ip to connect to</param>
=======
>>>>>>> origin/master
            public void Init(string connectionip)
            {
                Socket sock = Sockets.CreateTCPSocket(connectionip, PORT);
                try
                {
                    Client = new ClientInfo(sock, false);
                    Client.OnRead += Client_OnRead;
                }
                catch
                {
                    return;
                }
            }

            private void Client_OnRead(ClientInfo ci, string text)
            {
                string inte = text.Replace("\n", string.Empty).Replace("\0", string.Empty);
                Bytecode.Bytecode.Parse(inte); //Do it here since the server doesn't exist on our end. This is garuanteed to execute correctly.
            }

<<<<<<< HEAD
            /// <summary>
            /// Join the server
            /// </summary>
=======
>>>>>>> origin/master
            public void Connect()
            {
                if (Client == null) return;
                Client.BeginReceive();
            }

<<<<<<< HEAD
            /// <summary>
            /// Send a message to the server
            /// </summary>
            /// <param name="instruction"></param>
            /// <param name="param1"></param>
            /// <param name="param2"></param>
            /// <param name="player_index"></param>
=======
>>>>>>> origin/master
            public void SendMessageToServer(BoggleInstructions instruction, string param1, string param2, int player_index)
            {
                string ins = Bytecode.Bytecode.Generate(instruction, param1, param2, player_index);
                if (Client != null)
                {
                    Client.Send(ins);
                }
            }

        }

<<<<<<< HEAD
        /// <summary>
        /// The server for the client/server system.
        /// </summary>
=======
>>>>>>> origin/master
        public class NetServer
        {
            //TcpListener Server;

            //public void Init()
            //{
            //    Server = new TcpListener(IPAddress.Any, PORT);
            //    Server.Start();
            //}

            Server OurServer;
            RedCorona.Net.Server Server_;
            ClientInfo Client_;
<<<<<<< HEAD
            /// <summary>
            /// Player referenced by their IDs
            /// </summary>
            public Dictionary<int, Player> PlayerLookup;
            /// <summary>
            /// Clients referenced by their IDs
            /// </summary>
            public Dictionary<int, ClientInfo> ClientTable = new Dictionary<int, ClientInfo>();

            /// <summary>
            /// CTOR
            /// </summary>
            /// <param name="owner"></param>
=======
            public Dictionary<int, Player> PlayerLookup;
            public Dictionary<int, ClientInfo> ClientTable = new Dictionary<int, ClientInfo>();

>>>>>>> origin/master
            public NetServer(Server owner)
            {
                OurServer = owner;
                PlayerLookup = new Dictionary<int, Player>(Server.PLAYER_CAP);
            }

<<<<<<< HEAD
            /// <summary>
            /// Start a new server
            /// </summary>
=======
>>>>>>> origin/master
            public void StartServer()
            {
                Server_ = new RedCorona.Net.Server(PORT, new ClientEvent(ClientConnect));
            }

<<<<<<< HEAD
            /// <summary>
            /// Shut down the server
            /// </summary>
            public void CloseServer()
            {
                Server_Broadcast(Bytecode.Bytecode.Generate(BoggleInstructions.SERVER_SHUTDOWN, string.Empty));
                Server_.Close();
            }

            /// <summary>
            /// When a client connects to the server
            /// </summary>
            /// <param name="s"></param>
            /// <param name="new_client"></param>
            /// <returns></returns>
=======
>>>>>>> origin/master
            public bool ClientConnect(RedCorona.Net.Server s, ClientInfo new_client)
            {
                new_client.Delimiter = "\n";
                new_client.OnRead += ReadClient; //Read client messages
                new_client.OnClose += New_client_OnClose;
<<<<<<< HEAD
                Player np = new Player(new_client.ID);
                PlayerLookup.Add(new_client.ID, np); // Add a client to our listing.
                ClientTable.Add(new_client.ID, new_client);
                OurServer.PlayerConnected(np);
                return true;
            }

            /// <summary>
            /// When a client leaves the server
            /// </summary>
            /// <param name="ci"></param>
            private void New_client_OnClose(ClientInfo ci)
            {
                Debug.Log("Player disconnected");
                OurServer.PlayerDisconnect(PlayerLookup[ci.ID]);
=======
                PlayerLookup.Add(new_client.ID, new Player(new_client.ID)); // Add a client to our listing.
                ClientTable.Add(new_client.ID, new_client);
                return true;
            }

            private void New_client_OnClose(ClientInfo ci)
            {
>>>>>>> origin/master
                PlayerLookup.Remove(ci.ID); //Remove them.
                ClientTable.Remove(ci.ID);
                ci.OnRead -= ReadClient; //Unbind the events or else MEMORY LEAK (won't be GC'd)
                ci.OnClose -= New_client_OnClose;
            }

<<<<<<< HEAD
            /// <summary>
            /// Send a message to all players.
            /// </summary>
            /// <param name="msg"></param>
=======
>>>>>>> origin/master
            public void Server_Broadcast(string msg)
            {
                try
                {
                    Server_.Broadcast(Encoding.Unicode.GetBytes(msg)); //Send message
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }

<<<<<<< HEAD
            /// <summary>
            /// Send a message to a specific player
            /// </summary>
            /// <param name="msg"></param>
            /// <param name="p"></param>
=======
>>>>>>> origin/master
            public void Server_MessageClient(string msg, Player p)
            {
                try
                {
                    ClientInfo c = ClientTable[p.PlayerIndex];
                    c.Send(msg); //Message them.
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            private void ReadClient(ClientInfo ci, string text)
            {
                OurServer.ProcessInstruction(PlayerLookup[ci.ID], text.Replace("\n", string.Empty)); //Look up and send
            }
        }

        /// <summary>
        /// Get the IP address of the computer.
        /// </summary>
        /// <returns>IP address as a string</returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

    }
}