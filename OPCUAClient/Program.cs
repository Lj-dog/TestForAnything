using Opc;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Server;
using OPCUAClient;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace OPCUAClient
{
    internal class Program
    {
        private bool m_useSecurity;

        public bool UseSecurity
        {
            get { return m_useSecurity; }
            set { m_useSecurity = value; }
        }

        //opc.tcp://127.0.0.1:62547/DataAccessServer
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            //OpcUAHelper

            #region OpcUAHelper
            /* OpcUaClient m_OpcUaClient = new OpcUaClient();

             m_OpcUaClient.UserIdentity = new UserIdentity(new AnonymousIdentityToken());

             try
             {
                 await m_OpcUaClient.ConnectServer("opc.tcp://127.0.0.1:62547/DataAccessServer");

                 while (true)
                 {
                     Console.WriteLine("1.Read\r\n" + "2.Write\r\n" + "3.Clear\r\n" + "4.Exit\r\n");
                     var input = Console.ReadLine();

                     try
                     {
                         if (input.Equals("1"))
                         {
                             Console.WriteLine("Input ReadNodeID:\r\n");
                             //var readNoedID = Console.ReadLine();
                             string nodeID = "ns=2;s=Machines/Enable";
                             try
                             {
                                 bool value = m_OpcUaClient.ReadNode<bool>(nodeID);
                                 Console.WriteLine($"nodeID({nodeID}) Value:{value}");
                             }
                             catch (Exception readEx)
                             {
                                 Console.WriteLine($"ReadNodeID:{nodeID},Error:{readEx.Message}");
                             }
                         }
                         else if (input.Equals("2"))
                         {
                             Console.WriteLine("Input WriteNodeID:\r\n");

                             Console.WriteLine("value:\r\n");
                             string nodeID = "ns=2;s=Machines/Enable";

                             try
                             {
                                 bool boolValue = bool.Parse(Console.ReadLine());
                                 bool suceess = m_OpcUaClient.WriteNode<bool>(nodeID, boolValue);
                                 if (suceess)
                                     Console.WriteLine($"nodeID({nodeID}) Value:{boolValue}");
                                 else
                                     Console.WriteLine($"nodeID({nodeID}) WriteFail");
                             }
                             catch (Exception writeEx)
                             {
                                 Console.WriteLine($"ReadNodeID:{nodeID},Error:{writeEx.Message}");
                             }
                         }
                         else if (input.Equals("3"))
                             Console.Clear();
                         else if (input.Equals("4"))
                             return;
                     }
                     catch (Exception e)
                     {
                         Console.WriteLine($"发生错误，错误信息：{e.Message}");
                     }
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine("Connected Failed", ex);
             }*/

            #endregion


        }
    }
}
