using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NETBoggle.Networking.Bytecode;
using System.Net;
using System.Net.Sockets;
using RedCorona.Net;

namespace NETBoggle.Networking
{
    public static class NetworkTools
    {
        public const int PORT = 7654;

        public class NetClient
        {
            ClientInfo Client;
            Socket sock;

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

            public void Connect()
            {
                if (Client == null) return;
                Client.BeginReceive();
            }

            public void SendMessageToServer(BoggleInstructions instruction, string param1, string param2, int player_index)
            {
                string ins = Bytecode.Bytecode.Generate(instruction, param1, param2, player_index);
                if (Client != null)
                {
                    Client.Send(ins);
                }
            }

        }

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
            public Dictionary<int, Player> PlayerLookup;
            public Dictionary<int, ClientInfo> ClientTable = new Dictionary<int, ClientInfo>();

            public NetServer(Server owner)
            {
                OurServer = owner;
                PlayerLookup = new Dictionary<int, Player>(Server.PLAYER_CAP);
            }

            public void StartServer()
            {
                Server_ = new RedCorona.Net.Server(PORT, new ClientEvent(ClientConnect));
            }

            public bool ClientConnect(RedCorona.Net.Server s, ClientInfo new_client)
            {
                new_client.Delimiter = "\n";
                new_client.OnRead += ReadClient; //Read client messages
                new_client.OnClose += New_client_OnClose;
                PlayerLookup.Add(new_client.ID, new Player(new_client.ID)); // Add a client to our listing.
                ClientTable.Add(new_client.ID, new_client);
                return true;
            }

            private void New_client_OnClose(ClientInfo ci)
            {
                PlayerLookup.Remove(ci.ID); //Remove them.
                ClientTable.Remove(ci.ID);
                ci.OnRead -= ReadClient; //Unbind the events or else MEMORY LEAK (won't be GC'd)
                ci.OnClose -= New_client_OnClose;
            }

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