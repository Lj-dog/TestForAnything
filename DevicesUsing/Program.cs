using System.Threading.Tasks;
using DevicesFactory;
using DevicesFactory.Protocols;
using DevicesUsing.Devices;
using Microsoft.VisualBasic;

namespace DevicesUsing
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            #region Old
            ////Create a A类设备a中的子设备 a1
            //var deviveA_a1 = new DeviceA("deviveA_a1")
            //{
            //    Protocol = new TCPClientSetting()
            //    {
            //        IP = "127.0.0.1",
            //        Port = 502
            //    }
            //};
            ////Create a A类设备a中的子设备 a2
            //var deviveA_a2 = new DeviceA("deviveA_a2")
            //{
            //    Protocol = new TCPClientSetting()
            //    {
            //        IP = "127.0.0.1",
            //        Port = 502
            //    }
            //};
            ////Create a A类设备b
            //var deviceA_b = new DeviceA("deviceA_b")
            //{
            //    Protocol = new TCPServerSetting()
            //    {
            //        Port = 123
            //    }
            //};

            ////Create a A类设备c
            //var deviceA_c = new DeviceA("deviceA_c")
            //{
            //    Protocol = new TCPClientSetting
            //    {
            //        IP = "127.0.0.1",
            //        Port = 505,
            //    }
            //};

            //List<Device> devices = new();

            //devices.Add(deviveA_a1);
            //devices.Add(deviveA_a2);
            //devices.Add(deviceA_b);
            //devices.Add(deviceA_c);

            //foreach (var device in devices)
            //{
            //    DeviceFactory.Instance.CreateDevice(device);
            //}

            //foreach (var device in devices)
            //{
            //    DeviceFactory.Instance.Connect(device);
            //}

            //Console.WriteLine("设备连接成功！");

            //Console.ReadLine();

            //foreach (var device in devices)
            //{
            //    DeviceFactory.Instance.Send(device, "Hello");
            //}

            //deviveA_a1.Send("a");
            //deviveA_a2.Send("a");
            //deviceA_c.Send("c");
            //deviceA_b.Send("b");

            //foreach (var device in devices)
            //{
            //    DeviceFactory.Instance.Disconnect(device);
            //}

            #endregion
            #region 尝试ReadLine后是否能WriteLine
            //Task.Run(() =>
            //{
            //    for (int i = 0; i < 3000; i++)
            //    {
            //        Console.WriteLine(i);
            //    }
            //    ;
            //});
            //Console.ReadLine();
            //Console.WriteLine("任务已启动");
            //Console.ReadLine();
            #endregion

            DeviceA device1 = new("A1");
            device1.TcpClientChannel = new DevicesFactory.IDevices.TcpClientChannel();
            device1.TcpClientChannel.Protocol = new TCPClientSetting()
            {
                IP = "127.0.0.1",
                Port = 502,
            };

            DeviceA device2 = new DeviceA("A2");
            device2.TcpServerChannel = new DevicesFactory.IDevices.TcpServerChannel();
            device2.TcpServerChannel.Protocol = new TCPServerSetting() { Port = 802 };

            //DeviceA device3 = new DeviceA("A3");
            //device3.TcpServerChannel = new DevicesFactory.IDevices.TcpServerChannel();
            //device3.TcpServerChannel.Protocol = new TCPServerSetting() { Port = 802 };

            device1.CreateComunicationChannels();
            device2.CreateComunicationChannels();
            //device3.CreateComunicationChannels();

            while (Console.ReadLine() != string.Empty)
            {
                device1.GetState();
                device2.GetState();
            }
        }
    }
}
