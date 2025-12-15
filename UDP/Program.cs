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
            IPEndPoint iPEndPoint_B = new IPEndPoint(ip_127_0_0_5, 234);
            UdpClient udpClientB = new UdpClient(iPEndPoint_B);

            //不绑定端口
            UdpClient udpClientC = new UdpClient();

            try
            {
                Byte[] sendBytes_A_P2P = Encoding.ASCII.GetBytes("A say Hi?");
                Byte[] sendBytes_B_P2P = Encoding.ASCII.GetBytes("B say Hi?");
                Byte[] sendBytes_C_P2P = Encoding.ASCII.GetBytes("C say Hi?");

                IPEndPoint remoteIPEndP_IP3 = new IPEndPoint(
                    IPAddress.Parse("127.0.0.1"),
                    555
                );
                IPEndPoint remoteIPEndIP_IP1 = new IPEndPoint(
                    IPAddress.Parse("192.168.153.1"),
                    666
                );
                #region P2P发送

                try
                {
                    udpClientA.Send(sendBytes_A_P2P);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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

                Byte[] sendBytes_A_all_IP1 = Encoding.ASCII.GetBytes("A say Hi to everyone in 192.168.153 ?");
                Byte[] sendBytes_C_all_IP1 = Encoding.ASCII.GetBytes("C say Hi to everyone in 192.168.153 ?");

                Byte[] sendBytes_A_all = Encoding.ASCII.GetBytes("A say Hi to everyone?");
                Byte[] sendBytes_C_all = Encoding.ASCII.GetBytes("C say Hi to everyone?");
                #region 广播


                udpClientA.Send(sendBytes_A_all_IP1, allIPEndPoint_IP1);
                udpClientC.Send(sendBytes_C_all_IP1, allIPEndPoint_IP1);
                //udpClientA.EnableBroadcast = true;
                //udpClientC.EnableBroadcast = true;
                var a = udpClientA.Send(sendBytes_A_all, allIPEndPoint);
                var b = udpClientC.Send(sendBytes_C_all, allIPEndPoint);


                #endregion

                #region 组播

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
