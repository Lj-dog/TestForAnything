using System.Collections.Concurrent;
using System.IO.Ports;
using System.Net.Sockets;
using System.Security.AccessControl;
using DevicesFactory.Devices;
using DevicesFactory.Protocols;

namespace DevicesFactory
{

    interface ICreateComunicationChannel
    {

    }

    public class DeviceFactory
    {
        public static DeviceFactory Instance { get; private set; } = new DeviceFactory();

        /// <summary>
        /// 保存设备连接
        /// </summary>
        private static ConcurrentDictionary<int , TcpClient> tcpClients = new();

        private static ConcurrentDictionary<SerialPortSetting, SerialPort> serialPorts = new();

        private static ConcurrentDictionary<OtherClientSetting, OtherClient> otherClients = new();

        private static ConcurrentDictionary<TCPServerSetting, TcpListener> tcpListeners = new();

        private DeviceFactory() { }

        public void CreateChannel<T>(T protocol) where T : Protocol,new()
        { 
            
        }


        public void CreateDevice(Device device)
        {
            if (device is DeviceA deviceA)
            {
                if (deviceA.Protocol is TCPClientSetting clientSetting)
                {
                    deviceA.Client = tcpClients.GetOrAdd(clientSetting.GetHashCode(), new TcpClient());
                }
                else if (deviceA.Protocol is TCPServerSetting serverSetting)
                {
                    deviceA.Server = tcpListeners.GetOrAdd(serverSetting, new TcpListener(System.Net.IPAddress.Any, serverSetting.Port));

                }
                else if (deviceA.Protocol is SerialPortSetting serialPortSetting)
                {

                    deviceA.Serial = serialPorts.GetOrAdd(serialPortSetting, new SerialPort(
                        serialPortSetting.PortName,
                        serialPortSetting.BaudRate,
                        serialPortSetting.Parity,
                        serialPortSetting.DataBit,
                        serialPortSetting.StopBits
                    )
                    {
                        DtrEnable = serialPortSetting.DtrEnable,
                        RtsEnable = serialPortSetting.RtsEnable,
                    });
                }
                else if( deviceA .Protocol is OtherClientSetting setting)
                {
                    //...
                }
            }
            else if (device is DeviceB deviceB)
            {
                //....
            }
        }

        public void Connect(Device device)
        {
            if (device is ICommunicate communicateDevice)
            {
                communicateDevice.Connect();
            }
        }

        public void Disconnect(Device device)
        {
            if (device is ICommunicate communicateDevice)
            {
                communicateDevice.Disconnect();
            }
        }

        public void Send(Device device, string message)
        {
            if (device is ICommunicate communicateDevice)
            {
                communicateDevice.Send(message);
            }
        }
    }
}
