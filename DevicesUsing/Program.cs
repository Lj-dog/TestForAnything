using DevicesFactory;
using DevicesFactory.Devices;
using DevicesFactory.Protocols;

namespace DevicesUsing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //Create a A类设备a中的子设备 a1
            var deviveA_a1 = new DeviceA("deviveA_a1")
            {
                Protocol = new TCPClientSetting()
                {
                    IP = "127.0.0.1",
                    Port = 502
                }
            };
            //Create a A类设备a中的子设备 a2
            var deviveA_a2 = new DeviceA("deviveA_a2")
            {
                Protocol = new TCPClientSetting()
                {
                    IP = "127.0.0.1",
                    Port = 502
                }
            };
            //Create a A类设备b
            var deviceA_b = new DeviceA("deviceA_b")
            {
                Protocol = new TCPServerSetting()
                {
                    Port = 123
                }
            };


            //Create a A类设备c
            var deviceA_c = new DeviceA("deviceA_c")
            {
                Protocol = new TCPClientSetting
                {
                    IP = "127.0.0.1",
                    Port = 505,
                }
            };

            List<Device> devices = new();

            devices.Add(deviveA_a1);
            devices.Add(deviveA_a2);
            devices.Add(deviceA_b);
            devices.Add(deviceA_c);

            foreach (var device in devices)
            {
                DeviceFactory.Instance.CreateDevice(device);
            }

            foreach (var device in devices)
            {
                DeviceFactory.Instance.Connect(device);
            }

            Console.WriteLine("设备连接成功！");

            Console.ReadLine();

            foreach (var device in devices)
            {
                DeviceFactory.Instance.Send(device, "Hello");
            }

            deviveA_a1.Send("a");
            deviveA_a2.Send("a");
            deviceA_c.Send("c");
            deviceA_b.Send("b");

            foreach (var device in devices)
            {
                DeviceFactory.Instance.Disconnect(device);
            }

            Console.ReadLine();
        }
    }
}
