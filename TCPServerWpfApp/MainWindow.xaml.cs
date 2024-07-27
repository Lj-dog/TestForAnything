using System.DirectoryServices.ActiveDirectory;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TCPServerWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Socket> clientScokeList = new List<Socket>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ////创建Socket
            //Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

            //IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(this.txtIP.Text), int.Parse(this.txtPort.Text));

            //socket.Bind(iPEndPoint);
            //socket.Listen(10);

            //ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), socket);
        }

      /// <summary>
        /// 线程池线程执行的接受客户端连接方法
        /// </summary>
        /// <param name="obj">传入的Socket</param>
         private void AcceptClientConnect(object obj)
         {
             //转换Socket
             var serverSocket = obj as Socket;

            //AppendTxtLogText("服务端开始接收客户端连接！");

            //不断接受客户端的连接
            // while (true)
            //{
            //    5、创建一个负责通信的Socket
            //    Socket proxSocket = serverSocket.Accept();
            //    AppendTxtLogText(string.Format("客户端：{0}连接上了！", proxSocket.RemoteEndPoint.ToString()));
            //    将连接的Socket存入集合
            //     clientScoketLis.Add(proxSocket);
            //    6、不断接收客户端发送来的消息
            //     ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveClientMsg), proxSocket);
            //}

        }
    }
}