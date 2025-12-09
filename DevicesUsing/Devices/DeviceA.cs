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
        public TcpClientChannel TcpClientChannel;

        public TcpServerChannel TcpServerChannel;


        public bool IsErrored { get; set; }

        public DeviceA(string name)
            : base(name) { }

        public override void CreateComunicationChannels()
        {
            if (TcpClientChannel != null && TcpClientChannel.Protocol != null)
            {
                TcpClientChannel.MessageReceived += (m) =>
                {
                    var Command = Encoding.UTF8.GetString(m.ReceiveData);
                    Console.WriteLine($"{m.Target} : {DeviceName}:"+ Command);
                    if (Command.Contains("Error"))
                    {
                        IsErrored = true;
                    }
                };
                TcpClientChannel.ConnectAsync();
            }
            if (TcpServerChannel != null && TcpServerChannel.Protocol != null)
            {
                TcpServerChannel.MessageReceived += (m) =>
                {
                    var Command = Encoding.UTF8.GetString(m.ReceiveData);
                    Console.WriteLine($"{m.Target} : {DeviceName}:" + Command);
                    if (Command.Contains("Error"))
                    {
                        IsErrored = true;
                    }
                };
                TcpServerChannel.ConnectAsync();
            }
        }

        public override void GetState()
        {
            Console.WriteLine($"{DeviceName} is {(IsErrored ? "Errored" : "Normal")}");
        }

        public override void SendCommand(string command)
        {
            throw new NotImplementedException();
        }

        public override Task SendCommandAsync(string command)
        {
            throw new NotImplementedException();
        }

        public override Task LongJobTask()
        {
            throw new NotImplementedException();
        }

        //public override string ReceiveCommand()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
