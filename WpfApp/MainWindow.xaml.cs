using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
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


namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
       public ObservableCollection<Student> Students { get { return students; } set { students = value; RaisePropertyChangeed(nameof(Students)); } }

        public void RaisePropertyChangeed(string propertyName)
        {
            if(null != this.PropertyChanged)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        ObservableCollection<Student> students;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Get the current button.
            Button cmd = (Button)e.OriginalSource;

            // Create an instance of the window named
            // by the current button.
            Type type = this.GetType();
            Assembly assembly = type.Assembly;
            Window win = (Window)assembly.CreateInstance(
                type.Namespace + "." + cmd.Content);

            // Show the window.
            win.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Students = new ObservableCollection<Student>
            {
                new Student { Name = "张三", Age = 20 },
                new Student { Name = "李四", Age = 22 },
                new Student { Name = "王五", Age = 24 }
            };
            //studentListBox.ItemsSource = students;
        }

        private void buttonVictore_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DependencyObject level1 = VisualTreeHelper.GetParent(btn);
            DependencyObject level2 = VisualTreeHelper.GetParent(level1);
            DependencyObject level3 = VisualTreeHelper.GetParent(level2);
            MessageBox.Show(level1.GetType().ToString());
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

     public class NumericUpDown
    {
        public string Name { get; set; } = "afd";
        public int Age { get; set; } = 2;
    }
}