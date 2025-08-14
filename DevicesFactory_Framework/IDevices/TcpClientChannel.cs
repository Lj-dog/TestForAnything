using System;
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
    public class TcpClientChannel : IChannel<TCPClientSetting>
    {
        public TCPClientSetting Protocol { get; set; }
        protected TcpClient ChannelClient { get; set; }

        protected NetworkStream Stream { get; set; }

        private CancellationTokenSource readCTS;

        public event Action<ResultMessage> MessageReceived;

        public void Connect()
        {
            if (ChannelClient.Connected)
                return;
            readCTS = new CancellationTokenSource();
            ChannelClient.Connect(Protocol.IP, Protocol.Port);
            Stream = ChannelClient.GetStream();
            byte[] bufferbytes = new byte[1024];
            while (!readCTS.IsCancellationRequested)
            {
                int datalength = Stream.Read(bufferbytes, 0, bufferbytes.Length);
                if (datalength == 0) break;
                MessageReceived.Invoke(new ResultMessage(ChannelClient.Client.RemoteEndPoint?.ToString(), bufferbytes.Take(datalength).ToArray()));
            }
        }

        public async Task ConnectAsync()
        {
            if (ChannelClient.Connected)
                return;
            await ChannelClient.ConnectAsync(Protocol.IP, Protocol.Port);
            Stream = ChannelClient.GetStream();
            byte[] bufferbytes = new byte[1024];
            try
            {
                while (true)
                {
                    int datalength = await Stream.ReadAsync(bufferbytes, 0, bufferbytes.Length, readCTS.Token);
                    if (datalength == 0) break;
                    MessageReceived.Invoke(new ResultMessage(ChannelClient.Client.RemoteEndPoint?.ToString(), bufferbytes.Take(datalength).ToArray()));
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e);             
            }
        }

        public void Disconnect()
        {
            readCTS.Cancel();
            ChannelClient.Close();
        }

        public void Send(string targetName, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            if (
                ChannelClient.Client.Connected
                && ChannelClient.Client.RemoteEndPoint?.ToString() == targetName
            )
            {
                Stream.Write(data, 0, data.Length);
            }
        }

        public void Send(string targetName, byte[] data)
        {
            if (
                ChannelClient.Client.Connected
                && ChannelClient.Client.RemoteEndPoint?.ToString() == targetName
            )
            {
                Stream.Write(data, 0, data.Length);
            }
        }

        public void Send(string message)
        {
            if (ChannelClient.Client.Connected)
            {
                var data = Encoding.UTF8.GetBytes(message);
                Stream.Write(data,0,data.Length);
            }
                
        }

        public void Send(byte[] data)
        {
            if (ChannelClient.Client.Connected)
                Stream.Write(data, 0, data.Length);
        }

        public async Task SendAsync(string targetName, string message)
        {
            if (
                ChannelClient.Client.Connected
                && ChannelClient.Client.RemoteEndPoint?.ToString() == targetName
            )
            {
                var data = Encoding.UTF8.GetBytes(message);
                await Stream.WriteAsync(data,0,data.Length);
            }
               
        }

        public async Task SendAsync(string targetName, byte[] data)
        {
            if (
                ChannelClient.Client.Connected
                 
                && ChannelClient.Client.RemoteEndPoint?.ToString() == targetName
            )
                await Stream.WriteAsync(data, 0, data.Length);
        }

        public async Task SendAsync(string message)
        {
            if (ChannelClient.Client.Connected)
            {
                var data = Encoding.UTF8.GetBytes(message);
                await Stream.WriteAsync(data, 0, data.Length);
            }
        }

        public async Task SendAsync(byte[] data)
        {
            if (ChannelClient.Client.Connected)
                await Stream.WriteAsync(data, 0, data.Length);
        }
    }
}

