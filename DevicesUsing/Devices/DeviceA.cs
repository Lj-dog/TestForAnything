using DevicesFactory.Devices;
using DevicesFactory.IDevices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DevicesUsing.Devices
{
    internal class DeviceA : TcpClientChannel, TcpServerChannel
    {
        TcpClient TcpClientChannel.Client { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        NetworkStream TcpClientChannel.Stream { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        TcpListener TcpServerChannel.Server { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        ConcurrentDictionary<TcpClient, NetworkStream> TcpServerChannel.Clinets => throw new NotImplementedException();

        void TcpClientChannel.ICommunicate.Connect()
        {
            throw new NotImplementedException();
        }

        void ICommunicate.Disconnect()
        {
            throw new NotImplementedException();
        }

        byte[] ICommunicate.ReceiveBytes()
        {
            throw new NotImplementedException();
        }

        string ICommunicate.ReceiveString()
        {
            throw new NotImplementedException();
        }

        void ICommunicate.Send(string message)
        {
            throw new NotImplementedException();
        }

        void ICommunicate.Send(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
