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
        private NetworkStream networkStream;
        private CancellationTokenSource CTSource { get; set; }

        private CancellationToken token;

        public MainWindow()
        {
            InitializeComponent();
            CTSource = new CancellationTokenSource();
            token = CTSource.Token;
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
                    networkStream = TcpClient.GetStream();
                }
                Task.Run(() =>
                {
                    while (true)
                    {
                        token.ThrowIfCancellationRequested();
                        byte[] buffer = new byte[1024];
                        int len = networkStream.Read(buffer, 0, buffer.Length);
                        var message = Encoding.ASCII.GetString(buffer, 0, len);
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            this.RevText.Text += message + "\n";
                        }));
                    }
                }, token);
            }
            catch (TaskCanceledException ee)
            {
                MessageBox.Show("停止接受");
            }
            catch (Exception)
            {
                MessageBox.Show("Connect Error!!!");
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            //TcpClient.Close();
            CTSource.Cancel();
            TcpClient?.Dispose();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(this.SendText.Text);
            networkStream.Write(bytes, 0, bytes.Length);
        }
    }
}