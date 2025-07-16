using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.IO.Ports;
using System.Net.Sockets;
using System.Security.AccessControl;
using DevicesFactory.IDevices;
using DevicesFactory.Protocols;

namespace DevicesFactory
{

   public interface ICreateComunicationChannels
    {
        public void CreateComunicationChannels();
    }

    public class DeviceFactory
    {
        public  static  DeviceFactory Instance { get; private set; } = new DeviceFactory();

        /// <summary>
        /// 保存设备连接
        /// </summary>
        private static ConcurrentDictionary<int , TcpClient> tcpClients = new();

        private static ConcurrentDictionary<SerialPortSetting, SerialPort> serialPorts = new();

        private static ConcurrentDictionary<OtherClientSetting, OtherClient> otherClients = new();

        private static ConcurrentDictionary<TCPServerSetting, TcpListener> tcpListeners = new();

        private DeviceFactory() { }

        public void CreateDevicesChannel(List<ICreateComunicationChannels> devices)
        {
            foreach (var device in devices)
            {
                device.CreateComunicationChannels();
            }
        }

        public void CreateChannel<T>(T protocol) where T : Protocol,new()
        { 
            
        }

        public void CreateChannel(Type type)
        {

        }

        #region 第一次尝试多设备多通讯实现
        public void CreateDevice(Devices.Device device)
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
                else if (deviceA.Protocol is OtherClientSetting setting)
                {
                    //...
                }
            }
            else if (device is DeviceB deviceB)
            {
                //....
            }
        }

        public void Connect(Devices.Device device)
        {
            if (device is ICommunicate communicateDevice)
            {
                communicateDevice.Connect();
            }
        }

        public void Disconnect(Devices.Device device)
        {
            if (device is ICommunicate communicateDevice)
            {
                communicateDevice.Disconnect();
            }
        }

        public void Send(Devices.Device device, string message)
        {
            if (device is ICommunicate communicateDevice)
            {
                communicateDevice.Send(message);
            }
        } 
        #endregion
    }
}
