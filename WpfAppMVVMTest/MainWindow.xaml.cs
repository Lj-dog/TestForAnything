using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
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

        private ICollectionView collectionView;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.DataGridVM();

            this.employeePanel.DataContext = new EmployeeViewModel(Employee.FakeMany(3));

            //employees = new List<Employee>(Employee.FakeMany(13));

            ////做法一
            ////var cvs = this.FindResource("view") as CollectionViewSource;
            ////cvs.Source = employees;

            ////做法二
            //collectionView = CollectionViewSource.GetDefaultView(employees);
            //employeeDataGrid.ItemsSource = collectionView;
            //collectionView.Filter = (item) =>
            //{
            //    var em = item as Employee;
            //    return em.FirstName.Contains(filterTextBox.Text) ||
            //           em.LastName.Contains(filterTextBox.Text);
            //};
        }

        //做法一
        //private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        //{
        //    var em = e.Item as Employee;
        //    var text = filterTextBox.Text;
        //    e.Accepted = em.FirstName.Contains(text) || em.LastName.Contains(text);

        //}

        private void filterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //做法一
            //CollectionViewSource.GetDefaultView(employeeDataGrid.ItemsSource).Refresh();

            //做法二
            collectionView.Refresh();
        }

        private void NewEmployee_Click(object sender, RoutedEventArgs e)
        {
            employees.Add(Employee.FakeOne());
            //做法一
            //CollectionViewSource.GetDefaultView(employeeDataGrid.ItemsSource).Refresh();

            //做法二
            collectionView.Refresh();
        }
    }

    public partial class EmployeeViewModel : ObservableObject
    {

       List<Employee> employees;

        [ObservableProperty]
        ICollectionView collectionView;

        [ObservableProperty]
        string filterText;

        partial void OnFilterTextChanged(string value)=> CollectionView?.Refresh();

        public EmployeeViewModel(IEnumerable<Employee> employees)
        {
            this.employees = new List<Employee>(employees);
            CollectionView = CollectionViewSource.GetDefaultView(this.employees);
            CollectionView.Filter = (item) =>
            {
                if (string.IsNullOrEmpty(FilterText)) return true;
                var em = item as Employee;
                return em.FirstName.Contains(FilterText) ||
                       em.LastName.Contains(FilterText);
            };
        }

        [RelayCommand]
        void AddNewEmployee()
        {
            employees.Add(Employee.FakeOne());
            //刷新视图
            CollectionView?.Refresh();
        }
    }
}