using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FilesSelectorTest
{
    /// <summary>
    /// Interaction logic for FilesSelector.xaml
    /// </summary>
    public partial class FilesSelector : Window, INotifyPropertyChanged
    {
        private readonly string[] devices;

        public string CurrentDirectory { get; set; }
        public string CurrentDisplayDirectory { get; set; }

        public string[] CurrentPaths { get; set; } = new string[0];

        public HashSet<string> SelectedPaths { get; private set; } = new HashSet<string>();

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _disableRemoveSelectedPaths = false;

        // [".jpg", ".zip"]
        public string[] Filter { get; set; } = new string[0];

        public FilesSelector(string initializePath = null)
        {
            CurrentDirectory = initializePath;
            DataContext = this;
            InitializeComponent();
            devices = DriveInfo.GetDrives().Select(v => v.Name).ToArray();
        }

        public new void ShowDialog()
        {
            if (Directory.Exists(CurrentDirectory))
            {
                SelectDirectory(CurrentDirectory);
            }
            else
            {
                SelectDirectory(null);
            }
            base.ShowDialog();
        }

        private void SelectDirectory(string dir)
        {
            bool isRoot = false;
            if (dir == "/" || dir == "\\" || dir == null || dir == "")
            {
                isRoot = true;
                _disableRemoveSelectedPaths = true;
                CurrentPaths = devices;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPaths)));
                _disableRemoveSelectedPaths = false;
            }
            else if (!System.IO.Path.IsPathRooted(dir))
            {
                dir = System.IO.Path.Combine(CurrentDirectory, dir);
            }
            if (!Directory.Exists(dir) && !isRoot)
            {
                return;
            }
            CurrentDirectory = dir;
            CurrentDisplayDirectory = CurrentDirectory;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentDirectory)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentDisplayDirectory)));

            string[] findPaths;
            try
            {
                if (isRoot)
                {
                    findPaths = devices;
                }
                else
                {
                    if (Filter.Length == 0)
                    {
                        findPaths = Directory.GetDirectories(CurrentDirectory)
                            .Concat(Directory.GetFiles(CurrentDirectory)).ToArray();
                    }
                    else
                    {
                        findPaths = Directory.GetDirectories(CurrentDirectory)
                            .Concat(Directory.GetFiles(CurrentDirectory).Where(file => Filter.Any(v => file.EndsWith(v)))).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "访问失败");
                return;
            }

            if (!isRoot)
            {
                _disableRemoveSelectedPaths = true;
                CurrentPaths = findPaths.Select(v => System.IO.Path.GetFileName(v)).ToArray();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPaths)));
                _disableRemoveSelectedPaths = false;
            }

            var test = findPaths.Intersect(SelectedPaths).ToArray();
            foreach (var item in test)
            {
                var path = item;
                path = System.IO.Path.GetFileName(path);
                if (path == "")
                    path = item;
                listView.SelectedItems.Add(path);
            }
        }

        // 进入根目录(驱动器列表)
        private void Button_GoHome(object sender, RoutedEventArgs e)
        {
            SelectDirectory(null);
        }

        // 返回上一级
        private void Button_GoToParent(object sender, RoutedEventArgs e)
        {
            var parent = Directory.GetParent(CurrentDirectory ?? "/");
            SelectDirectory(parent?.FullName);
        }

        // 双击进入目录
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = (ListViewItem)e.Source;
            var content = (string)listViewItem.Content;
            if (!System.IO.Path.IsPathRooted(content))
            {
                content = System.IO.Path.Combine(CurrentDirectory, content);
            }
            SelectedPaths.Remove(content);
            SelectDirectory(content);
        }

        // 选择列表更改事件
        private void FilesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                var path = (string)item;
                if (!System.IO.Path.IsPathRooted(path))
                {
                    path = System.IO.Path.Combine(CurrentDirectory, (string)item);
                }
                SelectedPaths.Add(path);
            }
            if (!_disableRemoveSelectedPaths)
            {
                foreach (var item in e.RemovedItems)
                {
                    var path = (string)item;
                    if (!System.IO.Path.IsPathRooted(path))
                    {
                        path = System.IO.Path.Combine(CurrentDirectory, (string)item);
                    }
                    SelectedPaths.Remove(path);
                }
            }
        }

        // 跳转
        private void Button_GoTo(object sender, RoutedEventArgs e)
        {
            if (CurrentDisplayDirectory?.EndsWith(":") == true)
            {
                CurrentDisplayDirectory = CurrentDisplayDirectory + "\\";
            }
            SelectDirectory(CurrentDisplayDirectory);
        }

        // 回车跳转
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_GoTo(this, e);
            }
        }

        public string[] GetResult()
        {
            HashSet<string> files = new HashSet<string>();
            foreach (var item in SelectedPaths)
            {
                Search(item);
            }
            return files.ToArray();
            
            void Search(string path)
            {
                if (Directory.Exists(path))
                {
                    foreach (var item in Directory.GetFiles(path).Where(file => Filter.Length == 0 || Filter.Any(v => file.EndsWith(v))))
                    {
                        files.Add(item);
                    }

                    foreach (var item in Directory.GetDirectories(path))
                    {
                        Search(item);
                    }
                }
                else if (File.Exists(path))
                {
                    files.Add(path);
                }
            }
        }

        private void Button_OK(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
