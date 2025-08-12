using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DevicesFactory.IDevices;

namespace DevicesUsing.Devices
{
    class DeviceA : Device, IDevices
    {
        TcpClientChannel TcpClientChannel;

        TcpServerChannel TcpServerChannel;

        public bool IsRuning { get; set; }

        public bool IsErrored { get; set; }

        public DeviceA(string name)
            : base(name) { }

        public override void CreateComunicationChannels()
        {
            if (TcpClientChannel.Protocol != null)
                TcpClientChannel.Connect();
            if (TcpServerChannel.Protocol != null)
                TcpServerChannel.Connect();
        }

        public void GetState()
        {
            if (TcpClientChannel.Protocol != null)
            {
                IsErrored = TcpClientChannel.ReceiveBytes() == new byte[] { 0x00 };
                IsRuning = TcpClientChannel.ReceiveBytes() == new byte[] { 0x01 };
            }

            if (TcpServerChannel.Protocol != null)
            {
                IsErrored = TcpClientChannel.ReceiveBytes() == new byte[] { 0x00 };
                IsRuning = TcpClientChannel.ReceiveBytes() == new byte[] { 0x01 };
            }
        }

        public string ReceiveCommand()
        {
            throw new NotImplementedException();
        }

        public void SendCommand(string command)
        {
            if (TcpClientChannel.Protocol != null)
            {
                TcpClientChannel.Send(null, command);
            }
            if (TcpServerChannel.Protocol != null)
            {
                TcpServerChannel.Send(null, command);
            }
        }

        public Task SendCommandAsync(string command)
        {
            throw new NotImplementedException();
        }
    }
}
