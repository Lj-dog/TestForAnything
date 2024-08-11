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

namespace TCPClientWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpClient TcpClient { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TcpClient = new TcpClient();
                int port;
                if (int.TryParse(this.PortTextBox.Text, out port))
                {
                    TcpClient.Connect(this.IPTextBox.Text, port);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Connect Error!!!");
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            //TcpClient.Close();
            TcpClient?.Dispose();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            NetworkStream networkStream = TcpClient.GetStream();
            byte[] bytes = Encoding.ASCII.GetBytes(this.SendText.Text);
            networkStream.Write(bytes, 0, bytes.Length);
        }
    }
}