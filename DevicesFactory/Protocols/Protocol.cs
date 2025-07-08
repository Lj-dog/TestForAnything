using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DevicesFactory.Protocols
{
    public abstract class Protocol
    {

    }

    public class TCPClientSetting: Protocol
    {
        public override int GetHashCode()
        {
            return HashCode.Combine(IP,Port);
        }
        public string IP { get; set; }

        public int Port { get; set; }
    }

    public class TCPServerSetting : Protocol
    {
        public override int GetHashCode()
        {
            return Port.GetHashCode();
        }
        public int Port { get; set; }
    }

    public class SerialPortSetting : Protocol
    {
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
    public class OtherClientSetting: Protocol
    {
        public string IP { get; set; }
    }

    /// <summary>
    /// 第三方库通讯类
    /// </summary>
    public class OtherClient {

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
            stream.Write(buffer);
        }


    }
}
