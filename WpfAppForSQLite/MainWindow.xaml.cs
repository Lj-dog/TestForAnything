using ForSQLite;
using ForSQLite.Models;
using System.Windows;
using System.Windows.Xps.Serialization;

namespace WpfAppForSQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private SQLiteHelper sQLiteHelper;
        private List<Student> peopleList;

        public MainWindow()
        {
            InitializeComponent();
            sQLiteHelper = new("./DBFile.db", true);
            peopleList = new List<Student>() {
                new() { Name="as", Age = 12,PhoneNum=1234,StudentID=1},
                new() { Name="fggf", Age = 44,PhoneNum=4567,StudentID=2},
                 new() { Name="zxc", Age = 55,PhoneNum=7876, StudentID = 3},
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sQLiteHelper.LoadToDB(peopleList as List<People>);
        }
    }

    public class Student : People
    {
        public int StudentID { get; set; }
    }
}