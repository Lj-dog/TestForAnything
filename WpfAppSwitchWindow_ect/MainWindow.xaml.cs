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

namespace WpfAppSwitchWindow_ect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ComboBoxItem comboBoxItem = new ComboBoxItem();
            this.ComBox.Items.Add("One");
            this.ComBox.Items.Add("Two");
            this.ComBox.SelectedIndex = 0;
            this.StackPanel_One.Visibility = Visibility.Collapsed;
            this.StackPanel_Two.Visibility = Visibility.Visible;
        }

        private void BtnOneClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("One Clicked");
        }

        private void BtnTwoClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{this.TBoxTwo_1.Text},{this.TBoxTwo_2.Text}");
        }

        private void ComBoxSelectChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex == 0)
            {
                this.StackPanel_One.Visibility = Visibility.Visible;
                this.StackPanel_Two.Visibility = Visibility.Collapsed;
            }

            if (comboBox.SelectedIndex == 1)
            {
                this.StackPanel_One.Visibility = Visibility.Collapsed;
                this.StackPanel_Two.Visibility = Visibility.Visible;
            }
        }
    }
}