using NotifyIcon_托盘图标.Utils;
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

namespace NotifyIcon_托盘图标
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IServiceProvider provider;

        private readonly AppNotifyIcon icon;
        public MainWindow(IServiceProvider provider,MainVM vM, AppNotifyIcon icon)
        {
            this.provider = provider;
            this.icon = icon;
            DataContext = vM;
            InitializeComponent();
        }
    }
}