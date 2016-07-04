using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NETBoggle.Networking
{
    public static class NetworkTools
    {
        public const int PORT = 7654;

        public class NetClient
        {
            TcpClient Client;

            public void Init(string host)
            {
                Client = new TcpClient(AddressFamily.InterNetwork);
            }

            public async Task SendCommand(string command)
            {
                NetworkStream stm = new NetworkStream(Client.Client);
                await stm.WriteAsync(Encoding.Unicode.GetBytes(command), 0, command.Length);
            }

        }

        public class NetServer
        {
            TcpListener Server;

            public void Init()
            {
                Server = new TcpListener(IPAddress.Any, PORT);
                Server.Start();
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