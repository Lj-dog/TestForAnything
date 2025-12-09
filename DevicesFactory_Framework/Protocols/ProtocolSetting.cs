using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DevicesFactory.Protocols
{
    public abstract class ProtocolSetting
    {
        public abstract override int GetHashCode();

        public abstract override bool Equals(object obj);
    }

    public class TCPClientSetting : ProtocolSetting
    {
        public override int GetHashCode()
        {
            //return HashCode.Combine(IP, Port);
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + (IP?.GetHashCode() ?? 0);
                hash = hash * 31 + Port.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is TCPClientSetting setting && IP == setting.IP && Port == setting.Port;
        }

        public string IP { get; set; }

        public int Port { get; set; }
    }

    public class TCPServerSetting : ProtocolSetting
    {
        public override int GetHashCode()
        {
            return Port.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is TCPServerSetting setting && Port == setting.Port;
        }

        public int Port { get; set; }
    }

    public class SerialPortSetting : ProtocolSetting
    {
        public SerialPortSetting()
        {
            PortsName = SerialPort.GetPortNames().ToList();
            if (PortsName.Count > 0)
            {
                PortName = PortsName.First();
            }
        }

        public override int GetHashCode()
        {
            return PortName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is SerialPortSetting setting && PortName == setting.PortName);
        }

        public string PortName { get; set; }

        public List<string> PortsName { get; }

        public int BaudRate { get; set; }

        public Parity Parity { get; set; }

        public int DataBit { get; set; }

        public List<int> DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public Handshake Handshake { get; set; }

        public bool DtrEnable { get; set; }

        public bool RtsEnable { get; set; }
    }

    /// <summary>
    /// 第三方库通讯类配置
    /// </summary>
    public class OtherClientSetting : ProtocolSetting
    {
        public string IP { get; set; }

        public override bool Equals(object obj)    
        {
            return obj is OtherClientSetting setting && IP == setting.IP;
        }

        public override int GetHashCode()
        {
            return IP.GetHashCode();
        }
    }

    /// <summary>
    /// 第三方库通讯类
    /// </summary>
    public class OtherClient
    {
        private int port = 502;

        private string ip = "127.0.0.1";

        private TcpClient tCPClient;

        private NetworkStream stream;

        public OtherClient(string ip)
        {
            tCPClient = new TcpClient();
            this.ip = ip;
        }

        public void Connect()
        {
            tCPClient.Connect(ip, port);
            stream = tCPClient.GetStream();
        }

        public void Send(string message)
        {
            var buffer = UTF8Encoding.UTF8.GetBytes(message);
            stream.Write(buffer,0,buffer.Length);
        }
    }
}
