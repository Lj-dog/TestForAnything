using dnGREP;
using FilesSelectorTest;
using Microsoft.Win32;
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
using WpfDialogInputFilesInFolder.Dialog;

namespace WpfDialogInputFilesInFolder
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

        private void inputFiles(object sender, RoutedEventArgs e)
        {
            var filediolog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Select Files",
                Filter =   "CS (*.cs)|*.cs|"+"All files (*.*)|*.*",
                Multiselect = true,
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (filediolog.ShowDialog() == false)
                return;

            var selectedFiles = filediolog.FileNames;

            showFiles.ItemsSource = selectedFiles; 


        }

        private void CustomizeDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new FilesSelector()
            {
                Title = "Select Files",
                Filter = new string[] { ".cs", ".txt", ".xml" },
            };
            dialog.ShowDialog();
            if (dialog.DialogResult == false)
                return;

            var selectedFiles = dialog.GetResult();
            showFiles.ItemsSource = selectedFiles;
        }

        private void FilesDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new SelectFilesAndFilesInFolder()
            {
                Title = "Select Files",
                Filter = new string[] { ".cs", ".txt", ".xml" },

            };
            //dialog.ShowDialog();
            //if (dialog.DialogResult == false)
            //    return;
    
            if (dialog.ShowDialog() == false)
                return;
            showFiles.ItemsSource = dialog.GetFiles();
        }

        private void FileFolderDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new FileFolderDialog();
         
            dialog.ShowDialog();
        }
    }
}