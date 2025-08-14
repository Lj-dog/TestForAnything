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
    class DeviceA : Device
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



        public override void GetState()
        {
            throw new NotImplementedException();
        }

        public override void SendCommand(string command)
        {
            throw new NotImplementedException();
        }

        public override Task SendCommandAsync(string command)
        {
            throw new NotImplementedException();
        }

        public override string ReceiveCommand()
        {
            throw new NotImplementedException();
        }
    }
}
