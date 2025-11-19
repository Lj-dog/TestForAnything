using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCPClientWpfApp.ViewModels;

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
            this.DataContext = new MainViewModel();
        }

        private async void Connect_Click(object sender, RoutedEventArgs e)
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
                CTSource = new CancellationTokenSource();
                token = CTSource.Token;
                byte[] buffer = new byte[1024];
                int len = 0;

                while (true)
                {

                    len = await networkStream.ReadAsync(buffer, 0, buffer.Length,token);
                    if (len == 0)
                    {
                        break;
                    }
                    var message = Encoding.ASCII.GetString(buffer, 0, len);
                    Application.Current.Dispatcher.Invoke(
                        new Action(() =>
                        {
                            this.RevText.Text += message + "\n";
                        })
                    );
                    //Task.Delay(int.MaxValue, token);
                }
            }
            catch (TaskCanceledException ee)
            {
                MessageBox.Show("停止接受");
            }
            catch(OperationCanceledException oe)
            {
                MessageBox.Show("停止接受");
            }
            catch (Exception ex)
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
