using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DevicesFactory.Protocols;

namespace DevicesFactory.IDevices;

public abstract class Device: ICreateComunicationChannels
{
    protected Device(string name)
    {
        DeviceName = name;
    }

    /// <summary>
    /// (子)设备名称
    /// </summary>
    public string DeviceName { get; set; }

    public abstract void CreateComunicationChannels();
}


public interface ICommunicate<TSetting> where TSetting : Protocol
{
    TSetting Protocol { get; set; }

    public void Connect();

    public Task ConnectAsync();

    public void Disconnect();

    public void Send(string? targetName,string message);

    public Task SendAsync(string? targetName, string message);

    public void Send(string? targetName, byte[] data);

    public Task SendAsync(string? targetName, byte[] data);

    public string ReceiveString(string? targetName);

    public Task<string> ReceiveStringAsync(string? targetName);

    public byte[] ReceiveBytes(string? targetName);

    public Task<byte[]> ReceiveBytesAsync(string? targetName);
}
