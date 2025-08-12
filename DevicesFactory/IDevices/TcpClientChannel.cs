using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DevicesFactory.Protocols;

namespace DevicesFactory.IDevices;

public class TcpClientChannel : IChannel<TCPClientSetting>
{
    public TCPClientSetting Protocol { get; set; }
    protected TcpClient ChannelClient { get; set; }

    protected NetworkStream Stream { get; set; }

    public void Connect()
    {
        ChannelClient.Connect(Protocol.IP, Protocol.Port);
        Stream = ChannelClient.GetStream();
    }

    public async Task ConnectAsync()
    {
        await ChannelClient.ConnectAsync(Protocol.IP, Protocol.Port);
        Stream = ChannelClient.GetStream();
    }

    public void Disconnect()
    {
        ChannelClient.Close();
    }

    public byte[] ReceiveBytes(string? targetName)
    {
        byte[] buffer = new byte[1024];
        if (
            ChannelClient.Client.Connected
            && ChannelClient.Client.RemoteEndPoint?.ToString() == targetName
        )
        {
            int bytesRead = Stream.Read(buffer, 0, buffer.Length);
            return buffer.Take(bytesRead).ToArray();
        }
        return Array.Empty<byte>();
    }

    public byte[] ReceiveBytes()
    {
        byte[] buffer = new byte[1024];
        if (ChannelClient.Client.Connected)
        {
            int bytesRead = Stream.Read(buffer, 0, buffer.Length);
            return buffer.Take(bytesRead).ToArray();
        }
        return Array.Empty<byte>();
    }

    public async Task<byte[]> ReceiveBytesAsync(string? targetName)
    {
        byte[] buffer = new byte[1024];
        if (
            ChannelClient.Client.Connected
            && ChannelClient.Client.RemoteEndPoint?.ToString() == targetName
        )
        {
            int bytesRead = await Stream.ReadAsync(buffer, 0, buffer.Length);
            return buffer.Take(bytesRead).ToArray();
        }
        return Array.Empty<byte>();
    }

    public async Task<byte[]> ReceiveBytesAsync()
    {
        byte[] buffer = new byte[1024];
        if (ChannelClient.Client.Connected)
        {
            int bytesRead = await Stream.ReadAsync(buffer, 0, buffer.Length);
            return buffer.Take(bytesRead).ToArray();
        }
        return Array.Empty<byte>();
    }

    public string ReceiveString(string? targetName)
    {
        byte[] data = ReceiveBytes(targetName);
        return Encoding.UTF8.GetString(data);
    }

    public string ReceiveString()
    {
        byte[] data = ReceiveBytes();
        return Encoding.UTF8.GetString(data);
    }

    public async Task<string> ReceiveStringAsync(string? targetName)
    {
        byte[] data = await ReceiveBytesAsync(targetName);
        return Encoding.UTF8.GetString(data);
    }

    public async Task<string> ReceiveStringAsync()
    {
        byte[] data = await ReceiveBytesAsync();
        return Encoding.UTF8.GetString(data);
    }

    public void Send(string? targetName, string message)
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

    public void Send(string? targetName, byte[] data)
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
            Stream.Write(Encoding.UTF8.GetBytes(message));
    }

    public void Send(byte[] data)
    {
        if (ChannelClient.Client.Connected)
            Stream.Write(data, 0, data.Length);
    }

    public async Task SendAsync(string? targetName, string message)
    {
        if (
            ChannelClient.Client.Connected
            && ChannelClient.Client.RemoteEndPoint?.ToString() == targetName
        )
            await Stream.WriteAsync(Encoding.UTF8.GetBytes(message));
    }

    public async Task SendAsync(string? targetName, byte[] data)
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
            await Stream.WriteAsync(Encoding.UTF8.GetBytes(message));
    }

    public async Task SendAsync(byte[] data)
    {
        if (ChannelClient.Client.Connected)
            await Stream.WriteAsync(data, 0, data.Length);
    }
}
