using System;
using System.Net;
using System.Net.Sockets;

namespace NETBoggle.Networking
{
    class UdpRedirect
    {
        public string _address;
        public int _Port;
        public UdpRedirect()
        {
        }

        //TODO: Rewrite UdpRedirect.Connect() using async
        public void Connect()
        {
            UdpClient _UdpClient = new UdpClient(_Port);
            int? LocalPort = null;
            while (true)
            {
                IPEndPoint _IPEndPoint = null;
                byte[] _bytes = _UdpClient.Receive(ref _IPEndPoint);
                if (LocalPort == null) LocalPort = _IPEndPoint.Port;
                bool Local = IPAddress.IsLoopback(_IPEndPoint.Address);
                string AddressToSend = null;
                int PortToSend = 0;
                if (Local)
                {
                    AddressToSend = _address;
                    PortToSend = _Port;
                }
                else
                {
                    AddressToSend = "127.0.0.1";
                    PortToSend = LocalPort.Value;
                }
                _UdpClient.Send(_bytes, _bytes.Length, AddressToSend, PortToSend);
            }
        }
    }
}

