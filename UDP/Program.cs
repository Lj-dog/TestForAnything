using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP
{
    #region UDP

    public class Porgram
    {
        //IP1 : 192.168.153.1
        //IP2 : 192.168.3.244
        //IP3 : 127.0.0.1
        static async Task Main()
        {
            // 绑定本地端口
            UdpClient udpClientA = new UdpClient(321);

            //绑定本地IP端点
            IPAddress ip_127_0_0_5 = IPAddress.Parse("127.0.0.5");
            IPEndPoint iPEndPoint_B = new IPEndPoint(ip_127_0_0_5, 321);
            UdpClient udpClientB = new UdpClient(iPEndPoint_B);

            //不绑定端口
            UdpClient udpClientC = new UdpClient();

            try
            {
                Byte[] sendBytes_A_P2P = Encoding.ASCII.GetBytes("A say Hi?");
                Byte[] sendBytes_B_P2P = Encoding.ASCII.GetBytes("B say Hi?");
                Byte[] sendBytes_C_P2P = Encoding.ASCII.GetBytes("C say Hi?");

                IPEndPoint remoteIPEndP_IP3 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 555);
                IPEndPoint remoteIPEndIP_IP1 = new IPEndPoint(
                    IPAddress.Parse("192.168.153.1"),
                    666
                );
                #region P2P发送
                Console.WriteLine("点对点收发");
                try
                {
                    udpClientA.Send(sendBytes_A_P2P);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }

                udpClientA.Send(sendBytes_A_P2P, remoteIPEndP_IP3);
                Console.WriteLine();
                Console.WriteLine("A say Hi to 127.0.0.1:555");
                udpClientA.Send(sendBytes_A_P2P, remoteIPEndIP_IP1);
                Console.WriteLine();
                Console.WriteLine("A say Hi to 192.168.153.1:666");

                udpClientB.Send(sendBytes_B_P2P, remoteIPEndP_IP3);
                Console.WriteLine();
                Console.WriteLine("B say Hi to 127.0.0.1:555");
                try
                {
                    udpClientB.Send(sendBytes_B_P2P, remoteIPEndIP_IP1);
                    Console.WriteLine();
                    Console.WriteLine("B say Hi to 192.168.153.1:666");
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{e.Message},IPEndPoint:{remoteIPEndIP_IP1}");
                }

                udpClientC.Send(sendBytes_C_P2P, remoteIPEndP_IP3);
                Console.WriteLine();
                Console.WriteLine("C say Hi to 127.0.0.1:555");
                udpClientC.Send(sendBytes_C_P2P, remoteIPEndIP_IP1);
                Console.WriteLine();
                Console.WriteLine("C say Hi to 192.168.153.1:666");


                #endregion

                //此网段所有IP
                IPEndPoint allIPEndPoint_IP1 = new IPEndPoint(
                    IPAddress.Parse("192.168.153.255"),
                    666
                );

                //默认网卡IP
                IPEndPoint allIPEndPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 666);

                Byte[] sendBytes_A_all_IP1 = Encoding.ASCII.GetBytes(
                    "A say Hi to everyone in 192.168.153 ?"
                );
                Byte[] sendBytes_C_all_IP1 = Encoding.ASCII.GetBytes(
                    "C say Hi to everyone in 192.168.153 ?"
                );

                Byte[] sendBytes_A_all = Encoding.ASCII.GetBytes("A say Hi to everyone?");
                Byte[] sendBytes_C_all = Encoding.ASCII.GetBytes("C say Hi to everyone?");
                #region 广播

                Console.WriteLine("广播");

                udpClientA.Send(sendBytes_A_all_IP1, allIPEndPoint_IP1);
                Console.WriteLine();
                Console.WriteLine("A say Hi to 192.168.153:666");
                udpClientC.Send(sendBytes_C_all_IP1, allIPEndPoint_IP1);
                Console.WriteLine();
                Console.WriteLine("C say Hi to 192.168.153:666");
                //udpClientA.EnableBroadcast = true;
                //udpClientC.EnableBroadcast = true;
                var a = udpClientA.Send(sendBytes_A_all, allIPEndPoint);
                Console.WriteLine();
                Console.WriteLine("A say Hi to everyone:666");
                var b = udpClientC.Send(sendBytes_C_all, allIPEndPoint);
                Console.WriteLine();
                Console.WriteLine("C say Hi to everyone:666");

                #endregion

                //组播地址
                //IP 组播通信必须依赖于 IP 多播地址，在 IPv4 中它是一个 D 类 IP 地址，范围从 224.0.0.0 到 239.255.255.255，并被划分为局部链接多播地址、预留多播地址和管理权限多播地址3类：

                //局部链接多播地址范围在 224.0.0.0~224.0.0.255，这是为路由协议和其它用途保留的地址，路由器并不转发属于此范围的IP包；

                //预留多播地址为 224.0.1.0~238.255.255.255，可用于全球范围（如Internet）或网络协议；

                //管理权限多播地址为 239.0.0.0~239.255.255.255，可供组织内部使用，类似于私有 IP 地址，不能用于 Internet，可限制多播范围。

                Byte[] sendBytes_multi = Encoding.ASCII.GetBytes("Gruop 224.0.1.0 say Hi");

                IPAddress multiCastIP = IPAddress.Parse("224.0.1.0");

                #region 组播

                Console.WriteLine("组播");

                UdpClient multiRev_D = new UdpClient();

                multiRev_D.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                multiRev_D.Client.Bind(new IPEndPoint(IPAddress.Any,777));

                multiRev_D.JoinMulticastGroup(multiCastIP);


                UdpClient multiRev_E = new UdpClient();


                multiRev_E.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                multiRev_E.Client.Bind(new IPEndPoint(IPAddress.Any, 777));

                multiRev_E.JoinMulticastGroup(multiCastIP);

                //接受组播信息

                Task.Run(() =>
                {
                    IPEndPoint multiRev_D_IPEndPoint = null;
                    var bytes = multiRev_D.Receive(ref multiRev_D_IPEndPoint);
                    string msg = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine($"multiRev_D receive: {msg} receive IPEndPoint:{multiRev_D_IPEndPoint}");
                    Console.WriteLine();
                    multiRev_D.DropMulticastGroup(multiCastIP);
                }); 

                Task.Run(() =>
                {
                    IPEndPoint multiRev_E_IPEndPoint = null;
                    var bytes = multiRev_E.Receive(ref multiRev_E_IPEndPoint);
                    string msg = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine($"multiRev_E receive: {msg} receive IPEndPoint:{multiRev_E_IPEndPoint}");
                    Console.WriteLine();
                    multiRev_E.DropMulticastGroup(multiCastIP);
                });

                //发送组播信息

                IPEndPoint multiCastIPEndPoint = new IPEndPoint(multiCastIP, 777);

                UdpClient sendMultiUdp = new UdpClient();

                sendMultiUdp.Connect(multiCastIPEndPoint);

                sendMultiUdp.Send(sendBytes_multi);

                Console.ReadKey();
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                udpClientA.Close();
                udpClientB.Close();
                udpClientC.Close();
            }
        }
    }
    #endregion
}
