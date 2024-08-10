using ForSQLite;
using ForSQLite.Models;
using System.Windows;

namespace WpfAppForSQLite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteHelper sQLiteHelper;
        private List<People> peopleList;

        public MainWindow()
        {
            InitializeComponent();
            sQLiteHelper = new("./DBFile.db", true);
            peopleList = new List<People>() {
                new() { Name="as", Age = 12,PhoneNum=1234},
                new() { Name="fggf", Age = 44,PhoneNum=4567},
                 new() { Name="zxc", Age = 55,PhoneNum=7876},
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sQLiteHelper.LoadToDB<People>(peopleList);
        }
    }
}