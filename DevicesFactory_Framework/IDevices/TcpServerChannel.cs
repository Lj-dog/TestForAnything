using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevicesFactory_Framework.IDevices.Models;
using DevicesFactory.Protocols;

namespace DevicesFactory.IDevices
{
    public class TcpServerChannel : IChannel<TCPServerSetting>
    {
        public TCPServerSetting Protocol { get; set; }

        protected TcpListener Server { get; set; }

        protected Dictionary<string, TcpClient> ChannelClinets { get; } =
            new Dictionary<string, TcpClient>();

        private CancellationTokenSource listenCTS;

        public event Action<ResultMessage> MessageReceived;

        private async Task Handle(TcpClient client)
        {
            var stream = client.GetStream();
            byte[] bufferbytes = new byte[1024];

                while (!listenCTS.IsCancellationRequested)
                {
                    int datalength = await stream.ReadAsync(bufferbytes, 0, bufferbytes.Length);
                    if (datalength == 0)
                        break;
                    MessageReceived?.Invoke(
                        new ResultMessage(
                            client.Client.RemoteEndPoint?.ToString(),
                            bufferbytes.Take(datalength).ToArray()
                        )
                    );
                }
            
        }

        public  void Connect()
        {
            listenCTS = new CancellationTokenSource();
            if (Server == null)
                Server = new TcpListener(IPAddress.Any, Protocol.Port);

            Server.Start();

            while (!listenCTS.Token.IsCancellationRequested)
            {
                var client =  Server.AcceptTcpClient();
                ChannelClinets.Add(client.Client.RemoteEndPoint.ToString(), client);
                _ = Handle(client);
                ChannelClinets.Remove(client.Client.RemoteEndPoint.ToString());
            }
        }

        public async Task ConnectAsync()
        {
            listenCTS = new CancellationTokenSource();
            if (Server == null)
                Server = new TcpListener(IPAddress.Any, Protocol.Port);

            Server.Start();

            while (!listenCTS.Token.IsCancellationRequested)
            {
                var client = await Server.AcceptTcpClientAsync();
                ChannelClinets.Add(client.Client.RemoteEndPoint.ToString(), client);
                _ = Handle(client);
                ChannelClinets.Remove(client.Client.RemoteEndPoint.ToString());

            }
        }

        public void Disconnect()
        {
            listenCTS?.Cancel();
            Server.Stop();
        }


        public void Send(string targetName, string message)
        {
            throw new NotImplementedException();
        }

        public void Send(string targetName, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] data)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string targetName, string message)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string targetName, byte[] data)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(string message)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(byte[] data)
        {
            throw new NotImplementedException();
        }

    
    }
}
