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
        private List<People> peopleList;
        private List<People> peopleChangeList;


        public MainWindow()
        {
            InitializeComponent();
            sQLiteHelper = new("./DBFile.db", true);
            peopleList = new List<People>()

            {
                new() { Name = "小明", Age = 55, PhoneNum = 7876 },

                new() { Name = "小兰", Age = 55, PhoneNum = 7876 },

                new() { Name = "小明", Age = 12, PhoneNum = 1234 },
                new() { Name = "小明", Age = 44, PhoneNum = 4567 },
                new() { Name = "小红", Age = 55, PhoneNum = 7876 },
                new() { Name = "小绿", Age = 55, PhoneNum = 7876 },
            };
            peopleChangeList = new List<People>()

            {
                new() { Name = "小明", Age = 18, PhoneNum = 1111 },

                new() { Name = "小兰", Age = 123, PhoneNum = 7876 },

                new() { Name = "小绿", Age = 555, PhoneNum = 23333 },
                new() { Name = "小明", Age = 44, PhoneNum = 4567 },
                new() { Name = "小红", Age = 123, PhoneNum = 7876 },
                new() { Name = "小明", Age = 555, PhoneNum = 66666 },
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sQLiteHelper.LoadToDB(peopleList);
        }

        /// <summary>
        /// 修改最新小明数据age为18
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sQLiteHelper.ChangeLastData(18);
        }

        /// <summary>
        /// 覆盖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            sQLiteHelper.CoverListData(peopleChangeList);
        }
    }

    public class Student : People
    {
        public int StudentID { get; set; }
    }
}