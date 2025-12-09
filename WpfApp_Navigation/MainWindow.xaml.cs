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

namespace WpfApp_Navigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigationWinOpen(object sender, RoutedEventArgs e)
        {
            var NavigationWin = new NavigationViewWin();
            NavigationWin.Show();
        }

        private void FrameWinOpen(object sender, RoutedEventArgs e)
        {
            var frameWin = new FrameWin();
            frameWin.Show();
        }

        private void ContentWinOpen(object sender, RoutedEventArgs e)
        {
            var contentWin = new ContentWin();
            contentWin.Show();
        }
    }
}