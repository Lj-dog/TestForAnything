using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using DevicesFactory.Protocols;

namespace DevicesFactory.Devices
{
    public abstract class Device
    {
        protected Device(string name)
        {
            DeviceName = name;
        }

        /// <summary>
        /// (子)设备名称
        /// </summary>
        public string DeviceName { get; set; }

        public Protocol Protocol { get; set; }
    }

    public class DeviceA : Device, ICommunicate
    {
        public DeviceA(string name)
            : base(name) { }

        public TcpClient Client { get; set; }

        public TcpListener Server { get; set; }

        public SerialPort Serial { get; set; }

        public void Connect()
        {
            if (Client != null && !Client.Connected && Protocol is TCPClientSetting clientSetting)
            {
                Client.Connect(clientSetting.IP, clientSetting.Port);
            }
            else if (Server != null)
            {
                Server.Start();
            }
            else if (Serial != null && !Serial.IsOpen)
            {
                Serial.Open();
            }
        }

        public void Disconnect()
        {
            if (Client != null)
            {
                Client.Close();
            }
            else if (Server != null)
            {
                Server.Stop();
            }
            else if (Serial != null)
            {
                Serial.Close();
            }
        }

        public string Receive()
        {
            var buffer = new byte[1024];
            int bytesRead = 0;
            if (Client.Connected)
            {
                var netstream = Client.GetStream();
                bytesRead = netstream.Read(buffer, 0, buffer.Length);
            }
            else if (Server != null)
            {
                using TcpClient client = Server.AcceptTcpClient();
                var netstream = client.GetStream();
                bytesRead = netstream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
            else if (Serial.IsOpen)
            {
                bytesRead = Serial.BytesToRead;
                Serial.Read(buffer, 0, Serial.BytesToRead);
            }
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public void Send(string message)
        {
            message = DeviceName + ": " + message + "\r\n";
            if (Client!=null&&Client.Connected)
            {
                var netstream = Client.GetStream();
                var buffer = Encoding.UTF8.GetBytes(message);
                netstream.Write(buffer, 0, buffer.Length);
            }
            else if (Server != null)
            {
                using TcpClient client = Server.AcceptTcpClient();
                var netstream = client.GetStream();
                var buffer = Encoding.UTF8.GetBytes(message);
                netstream.Write(buffer, 0, buffer.Length);
            }
            else if (Serial!=null&&Serial.IsOpen)
            {
                Serial.Write(message);
            }
        }
    }

    public class DeviceB : Device, ICommunicate
    {
        public DeviceB(string name)
            : base(name) { }

        public TcpClient Client { get; set; }

        public OtherClient OtherClient { get; set; }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public string Receive()
        {
            throw new NotImplementedException();
        }

        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICommunicate
    {
        public void Connect();

        public void Disconnect();

        public void Send(string message);

        public string Receive();
    }
}
