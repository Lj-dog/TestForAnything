using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DevicesFactory.Protocols;

namespace DevicesFactory.IDevices;

public class TcpClientChannel : ICommunicate<TCPClientSetting>
{
    public TCPClientSetting Protocol { get; set; }
    protected TcpClient Client { get; set; }

    protected NetworkStream Stream { get; set; }

    public void Connect()
    {
        Client.Connect(Protocol.IP, Protocol.Port);
        Stream = Client.GetStream();
    }

    public async Task ConnectAsync()
    {
        await Client.ConnectAsync(Protocol.IP, Protocol.Port);
        Stream = Client.GetStream();
    }

    public void Disconnect()
    {
        Client.Close();
    }

    public byte[] ReceiveBytes()
    {
        byte[] buffer = new byte[1024];
        if (Stream.DataAvailable)
        {
            Stream.Read(buffer);
        }

        return buffer;
    }

    public async Task<byte[]> ReceiveBytesAsync()
    {
        byte[] buffer = new byte[1024];
        if (Stream.DataAvailable)
        {
            await Stream.ReadAsync(buffer);
        }

        return buffer;
    }

    public string ReceiveString()
    {
        byte[] buffer = ReceiveBytes();
        return Encoding.UTF8.GetString(buffer).TrimEnd('\0');
    }

    public async Task<string> ReceiveStringAsync()
    {
        byte[] buffer = await ReceiveBytesAsync();
        return Encoding.UTF8.GetString(buffer).TrimEnd('\0');
    }

    public void Send(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        if (Stream.DataAvailable)
        {
            Stream.Write(data);
        }
    }

    public void Send(byte[] data)
    {
        if (Stream.DataAvailable)
        {
            Stream.Write(data);
        }
    }

    public async Task SendAsync(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        if (Stream.DataAvailable)
        {
            await Stream.WriteAsync(data, 0, data.Length);
        }
    }

    public async Task SendAsync(byte[] data)
    {
        if(Stream.DataAvailable)
        {
            await Stream.WriteAsync(data, 0, data.Length);
        }
    }
}
