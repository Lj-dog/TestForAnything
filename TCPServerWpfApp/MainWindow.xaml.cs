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
        public static Dictionary<string, string> ClientList { get; set; } = new() { { "sf", "asdf" }, { "123", "asdf" }, };
        public Dictionary<string, Task> ClientTask;
        private TcpListener listener;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void txb_scr_dir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("textboxEnter");
            }
        }

        private void OpenService_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse(this.txtIP.Text);
            int port = int.Parse(this.txtPort.Text);
            listener = new(ip, port);
            listener.Start();
        }

        private void CloseService_Click(object sender, RoutedEventArgs e)
        {
            listener.Stop();
        }
    }
}