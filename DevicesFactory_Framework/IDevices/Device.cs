using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DevicesFactory.Protocols;

namespace DevicesFactory.IDevices
{
    public abstract class Device : ICreateComunicationChannels
    {
        protected Device(string name)
        {
            DeviceName = name;
        }

        /// <summary>2
        /// (子)设备名称
        /// </summary>
        public string DeviceName { get; set; }


        public abstract void GetState();

        public abstract void SendCommand(string command);

        public abstract Task SendCommandAsync(string command);

        public abstract string ReceiveCommand();

        public abstract void CreateComunicationChannels();
    }

    public interface IChannel<TSetting> : ICommunicate
        where TSetting : ProtocolSetting
    {
        TSetting Protocol { get; set; }
    }


    public interface ICommunicate
    {
        void Connect();

        Task ConnectAsync();

        void Disconnect();

        void Send(string targetName, string message);

        Task SendAsync(string targetName, string message);

        void Send(string targetName, byte[] data);

        Task SendAsync(string targetName, byte[] data);

        //
        void Send(string message);

        Task SendAsync(string message);

        void Send(byte[] data);

        Task SendAsync(byte[] data);
    }
}


