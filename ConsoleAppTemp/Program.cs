using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FreeSql;
using FreeSql.DataAnnotations;

namespace ConsoleAppTemp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            #region 正则表达式
            //string pattern = @"(QX|IX)(\d{1,4})\.([0-7])$";
            //  Regex regex = new Regex(pattern);
            //  var match = regex.Match("IX14.5");
            //  if (match.Success)
            //      foreach (Group m in match.Groups)
            //      {
            //          Console.WriteLine(m.Value);
            //      }
            //  else
            //      Console.WriteLine("false");
            #endregion
            #region 异常Data属性的使用

            //Console.WriteLine("\nException with some extra information...");
            //RunTest(false);
            //Console.WriteLine("\nException with all extra information...");
            //RunTest(true);
            #endregion
            Machine machine1 = new(1, "machine1");
            Machine machine2 = new(2, "machine2");

            machine1.Run();
            machine2.Run();
        }

        #region 异常Data属性的使用
        public static void RunTest(bool displayDetails)
        {
            try
            {
                NestedRoutine1(displayDetails);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown.");
                Console.WriteLine(e.Message);
                if (e.Data.Count > 0)
                {
                    Console.WriteLine("  Extra details:");
                    foreach (DictionaryEntry de in e.Data)
                        Console.WriteLine(
                            "    Key: {0,-20}      Value: {1}",
                            "'" + de.Key.ToString() + "'",
                            de.Value
                        );
                }
            }
        }

        public static void NestedRoutine1(bool displayDetails)
        {
            try
            {
                NestedRoutine2(displayDetails);
            }
            catch (Exception e)
            {
                e.Data["ExtraInfo"] = "Information from NestedRoutine1.";
                e.Data.Add("MoreExtraInfo", "More information from NestedRoutine1.");
                throw;
            }
        }

        public static void NestedRoutine2(bool displayDetails)
        {
            Exception e = new Exception("This statement is the original exception message.");
            if (displayDetails)
            {
                string s = "Information from NestedRoutine2.";
                int i = -903;
                DateTime dt = DateTime.Now;
                e.Data.Add("stringInfo", s);
                e.Data["IntInfo"] = i;
                e.Data["DateTimeInfo"] = dt;
            }
            throw e;
        }
        #endregion

        #region 两异步方法同时调用static异步方法

        //模拟IOC拿到实例
        public static ServerManager Server =
            new("502", new List<string> { "127.0.0.1", "192.168.3.4" });

        public static WorkFlow workFlow = new();

        public class ServerManager
        {
            private TcpListener TcpListener;

            public List<string> IPList { get; set; }

            public Dictionary<string, bool> IsConnected { get; private set; }

            public ConcurrentDictionary<string, TcpClient> Clients { get; private set; } = new ConcurrentDictionary<string, TcpClient>();

            public ConcurrentDictionary<string, bool> IsReply { get; set; } = new ConcurrentDictionary<string, bool>();

            public ServerManager(string port, List<string> iplist)
            {
                IPList = iplist;
                TcpListener = new(IPAddress.Any, int.Parse(port));
            }

            public async Task StartAsync()
            {
                TcpListener.Stop();
                TcpListener.Start();
                while (true)
                {
                    TcpClient client = await TcpListener.AcceptTcpClientAsync();

                    var ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    if (IPList.Contains(ip))
                    {
                        Clients.AddOrUpdate(ip, client, (key, c) => { return client; });
                        IsConnected[ip] = true;
                        _ = HandleAsync(client,ip);
                    }
                }
            }

            private async Task HandleAsync(TcpClient client,string ip)
            {
                NetworkStream stream = client.GetStream();
                var data = new byte[1024];
                while (true)
                {
                    try
                    {
                        int datanum = await stream.ReadAsync(data, 0, data.Length);
                        if (datanum == 0)
                            break;
                        IsReply[ip] = true;
                    }
                    catch (Exception e) when (e is IOException || e is SocketException)
                    {

                        break;
                    }
                }
                Clients.TryRemove(ip, out var c);
                IsConnected[ip] = false;
                stream.Close();
                client.Close();
            }

            public async Task SendAsync(string ip,string message)
            {
                TcpClient client;
                if (!Clients.TryGetValue(ip, out client))
                    return;
                NetworkStream network = client.GetStream();
                try
                {
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await network.WriteAsync(messageBytes, 0, messageBytes.Length);

                }
                catch (Exception e) when (e is IOException || e is SocketException)
                {

                    Clients.TryRemove(ip, out var _);
                    IsConnected[ip] = false;
                    network.Dispose();
                    client.Close();
                }
            }
        }

        public static async Task<bool> IsSendSuccess(byte machineNum, string message)
        {
            var ip = Server.IPList[machineNum];
            if (!Server.IsConnected[ip])
                return false;
            await Server.SendAsync(ip, message);
            await Task.Delay(500);//waiting reply
            for (int i = 0; i < 3; i++)
            {
                var res = Server.IsReply[ip];
                if (res)
                {
                    Server.IsReply[ip] = false;
                    return res;
                }
                await Task.Delay(100); // waiting again
            }
            return false;
        }

        public class Machine
        {
            private byte num;
            //携带信息
            public string Name { get; set; }
            public Machine(byte num ,string name)
            {
                this.num = num;
                Name = name;
            }
            public async void Run()
            {
                try
                {
                    var token = new CancellationTokenSource();
                    while (true)
                    {
                      await workFlow.Working(num, IsSendSuccess, token.Token);
                    }
                }
                catch (Exception)
                {

                    ;
                }
                //...
            }
            //...
        }

        //在另一个外部库
        public class WorkFlow
        {
            public async Task Working(byte machineNum, Func<byte, string, Task<bool>> isSendSuccess,CancellationToken token)
            {
                //其他操作 异步
                //
                var message = "其他设备上拿到的Message";
                if (!await IsSendSuccess(machineNum, message))
                    return;
                //其他操作 异步
            }
        }
        #endregion
    }
}
