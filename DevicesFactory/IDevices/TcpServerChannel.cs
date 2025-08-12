using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DevicesFactory.Protocols;

namespace DevicesFactory.IDevices;

public class TcpServerChannel : IChannel<TCPServerSetting>
{
    public TCPServerSetting Protocol { get; set; }

    protected TcpListener Server { get; set; }

    protected ConcurrentDictionary<string, TcpClient> ChannelClinets { get; } =
        new ConcurrentDictionary<string, TcpClient>();

    private CancellationTokenSource _listenCTS;

    public void Connect()
    {
        _listenCTS = new CancellationTokenSource();
        Server?.Start(Protocol.Port);

        while (!_listenCTS.Token.IsCancellationRequested)
        {
            var client = Server.AcceptTcpClient();
            ChannelClinets.TryAdd(client.Client.RemoteEndPoint.ToString(), client);
        }
    }

    public async Task ConnectAsync()
    {
        _listenCTS = new CancellationTokenSource();
        Server?.Start(Protocol.Port);

        while (!_listenCTS.Token.IsCancellationRequested)
        {
            var client = await Server.AcceptTcpClientAsync(_listenCTS.Token);
            ChannelClinets.TryAdd(client.Client.RemoteEndPoint.ToString(), client);
        }
    }

    public void Disconnect()
    {
        _listenCTS?.Cancel();
        Server.Stop();
    }

    public byte[] ReceiveBytes(string? targetName)
    {
        var client = ChannelClinets.GetValueOrDefault(targetName);
        if (client != null && client.Connected)
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

    public Task<byte[]> ReceiveBytesAsync(string? targetName)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> ReceiveBytesAsync()
    {
        throw new NotImplementedException();
    }

    public string ReceiveString(string? targetName)
    {
        throw new NotImplementedException();
    }

    public string ReceiveString()
    {
        throw new NotImplementedException();
    }

    public Task<string> ReceiveStringAsync(string? targetName)
    {
        throw new NotImplementedException();
    }

    public Task<string> ReceiveStringAsync()
    {
        throw new NotImplementedException();
    }

    public void Send(string? targetName, string message)
    {
        throw new NotImplementedException();
    }

    public void Send(string? targetName, byte[] data)
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

    public Task SendAsync(string? targetName, string message)
    {
        throw new NotImplementedException();
    }

    public Task SendAsync(string? targetName, byte[] data)
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
