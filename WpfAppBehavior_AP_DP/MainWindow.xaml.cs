using System.Diagnostics;
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

namespace WpfAppBehavior
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cornerbtn.ButtonCornerRadius = new CornerRadius(23);
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"{ex.Message}");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            cornerbtn.ButtonCornerRadius = new CornerRadius(3);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            cornerbtn.ButtonCornerRadius = new CornerRadius(8);

        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            cornerbtn.ButtonCornerRadius = new CornerRadius(10);

        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            try
            {
                cornerbtn.ButtonCornerRadius = new CornerRadius(15);
            }
            catch (ArgumentException ex)
            {

                MessageBox.Show($"{ex.Message}");
            }
        }
    }
}