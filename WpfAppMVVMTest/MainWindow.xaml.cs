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
using WpfAppMVVMTest.Models;

namespace WpfAppMVVMTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Employee> employees;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.DataGridVM();

            employees = new List<Employee>(Employee.FakeMany(13));
            var cvs = this.FindResource("view") as CollectionViewSource;
            cvs.Source = employees;
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            var em = e.Item as Employee;
            var text = filterTextBox.Text;
            e.Accepted = em.FirstName.Contains(text) || em.LastName.Contains(text);

        }

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(employeeDataGrid.ItemsSource).Refresh();
        }

        private void NewEmployee_Click(object sender, RoutedEventArgs e)
        {
            employees.Add(Employee.FakeOne());
            CollectionViewSource.GetDefaultView(employeeDataGrid.ItemsSource).Refresh();

        }
    }
}