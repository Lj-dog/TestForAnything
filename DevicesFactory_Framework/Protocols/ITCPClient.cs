using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DevicesFactory.Protocols
{
    internal interface ITCPClient
    {
        TcpClient TcpClient { get; set; }


        void Connect();

        void Disconnect();

        void Send(string targetName, string message);

        Task SendAsync(string targetName, string message);

         byte[] ReceiveBytes(string? targetName);
          

    }
}
