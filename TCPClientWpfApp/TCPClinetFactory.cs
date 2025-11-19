using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClientWpfApp
{
    public class TCPClinetFactory
    {
        private TcpClient TcpClient { get; set; }
        private NetworkStream networkStream;
        public CancellationTokenSource CTSource { get; set; }

        private CancellationToken token;

        public TCPClinetFactory(string ip, string port)
        {
            TcpClient = new TcpClient();

            if (!int.TryParse(port, out int _port))
                throw new ArgumentException("端口错误");

            TcpClient.Connect(ip, _port);
            networkStream = TcpClient.GetStream();

            CTSource = new CancellationTokenSource();
            token = CTSource.Token;
        }
    }
}