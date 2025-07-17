using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace WpfDialogInputFilesInFolder.Dialog
{
    /// <summary>
    /// SelectFilesAndFilesInFolder.xaml 的交互逻辑
    /// </summary>
    public partial class SelectFilesAndFilesInFolder : Window
    {
        public SelectFilesAndFilesInFolder()
        {
            InitializeComponent();
        }

        public string[] Filter { get; set; }

        public string SelectedFilter { get; set; }

        public new bool? ShowDialog()
        {
            if (Directory.Exists(selectpath.Text))
            {
                selectpath.Text = selectpath.Text;
            }
            else
            {
                selectpath.Text = null;
            }
            Filters.ItemsSource = Filter;

           return base.ShowDialog();
        }

        private void selectPath(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog? dialog = new OpenFolderDialog()
            {
                Multiselect = false
            };

            if (dialog.ShowDialog() == false)
                return;
            selectpath.Text = dialog.FolderName;
        }

        public string[] GetFiles()
        {
            HashSet<string> files = new HashSet<string>();

           

            if (string.IsNullOrEmpty(selectpath.Text)||!Directory.Exists(selectpath.Text))
                return files.ToArray();

            GetFilesInDir(files,selectpath.Text);
            if (files.Count == 0)
            {
                MessageBox.Show("No files found in the selected folder.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return files.ToArray();
            }
            return files.ToArray();
        }

        private void GetFilesInDir(HashSet<string> files,string dirOrfile)
        {
            var selectedFilter = (string)Filters.SelectedItem;
            foreach (var file in Directory.GetFiles(dirOrfile).Where(file => file.EndsWith(selectedFilter)))
            {
                files.Add(file);
            }
            foreach (var path in Directory.GetDirectories(dirOrfile))
            {
                GetFilesInDir(files, path);
            }

        }


        private void Okbtn(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void filesSelectWinLoaded(object sender, RoutedEventArgs e)
        {
            if (Filters.Items.Count > 0)
                Filters.SelectedIndex = 0;
        }
    }
}
