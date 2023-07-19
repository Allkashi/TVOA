using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace TVOA
{
    class Connect
    {
        public string OpenConnect (string request)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.18"), 8888);
            socket.Connect(serverEndPoint);

            var encoding = Encoding.UTF8;

            var req = encoding.GetBytes(request);
            socket.Send(req);


            byte[] buffer = new byte[2048];
            int bytesReceived = socket.Receive(buffer);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

            return response;

        }
    }
}
