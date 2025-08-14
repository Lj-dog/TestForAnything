using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevicesFactory.Protocols;
using DevicesFactory_Framework.IDevices.Models;

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
            byte[] buffer = new byte[1024];
            while(true)
            {

            }
        }

        public void Connect()
        {
            listenCTS = new CancellationTokenSource();
            Server?.Start(Protocol.Port);

            while (!listenCTS.Token.IsCancellationRequested)
            {
                var client = Server.AcceptTcpClient();
                ChannelClinets.Add(client.Client.RemoteEndPoint.ToString(), client);
                _= Handle(client);
            }
        }

        public async Task ConnectAsync()
        {
            listenCTS = new CancellationTokenSource();
            Server?.Start(Protocol.Port);

            while (!listenCTS.Token.IsCancellationRequested)
            {
                var client = await Server.AcceptTcpClientAsync();
                ChannelClinets.Add(client.Client.RemoteEndPoint.ToString(), client);
            }
        }

        public void Disconnect()
        {
            listenCTS?.Cancel();
            Server.Stop();
        }

        public byte[] ReceiveBytes(string targetName)
        {
            TcpClient client;
            var hasclient = ChannelClinets.TryGetValue(targetName,out client);
            if (hasclient && client != null && client.Connected)
            {
                var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                return buffer.Take(bytesRead).ToArray();
            }
            return Array.Empty<byte>();
        }

        public byte[] ReceiveBytes()
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ReceiveBytesAsync(string targetName)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ReceiveBytesAsync()
        {
            throw new NotImplementedException();
        }

        public string ReceiveString(string targetName)
        {
            throw new NotImplementedException();
        }

        public string ReceiveString()
        {
            throw new NotImplementedException();
        }

        public Task<string> ReceiveStringAsync(string targetName)
        {
            throw new NotImplementedException();
        }

        public Task<string> ReceiveStringAsync()
        {
            throw new NotImplementedException();
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

