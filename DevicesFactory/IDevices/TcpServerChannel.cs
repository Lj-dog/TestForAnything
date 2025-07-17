
using DevicesFactory.Protocols;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DevicesFactory.IDevices;

public class TcpServerChannel:ICommunicate<TCPServerSetting>
{
    public TCPServerSetting Protocol { get ; set; }

    protected TcpListener? Server { get; set; }

    protected ConcurrentDictionary<TcpClient, NetworkStream> Clinets { get; } = new ConcurrentDictionary<TcpClient, NetworkStream>();

    public void Connect()
    {
        Server?.Start(Protocol.Port);
        
        
    }

    public Task ConnectAsync()
    {
        throw new NotImplementedException();
    }

    public void Disconnect()
    {
        throw new NotImplementedException();
    }

    public byte[] ReceiveBytes(string? targetName)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> ReceiveBytesAsync(string? targetName)
    {
        throw new NotImplementedException();
    }

    public string ReceiveString(string? targetName)
    {
        throw new NotImplementedException();
    }

    public Task<string> ReceiveStringAsync(string? targetName)
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

    public Task SendAsync(string? targetName, string message)
    {
        throw new NotImplementedException();
    }

    public Task SendAsync(string? targetName, byte[] data)
    {
        throw new NotImplementedException();
    }
}
