using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using NotifyIcon_托盘图标.Utils;

namespace NotifyIcon_托盘图标
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IServiceProvider provider;

        private readonly AppNotifyIcon icon;

        private readonly MainVM vm;

        private bool forceExit = false;

        public MainWindow(IServiceProvider provider, MainVM vM, AppNotifyIcon icon)
        {
            this.provider = provider;
            this.icon = icon;
            this.vm = vM;
            DataContext = this.vm;

            this.icon.DoubleClickEvent += (sender, e) =>
            {
                this.Show();
                this.WindowState = WindowState.Minimized;
                this.WindowState = WindowState.Normal;
                this.Activate();
            };

            this.icon.ExitClick += (sender, e) =>
            {
                forceExit = true;
                this.Close();
            };

            InitializeComponent();
        }

        private void WinClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (forceExit)
                return;
            if (vm.AskIsMinInIconWhenClose)
            {
                var res = MessageBox.Show(
                    "是否关闭程序，否则最小化到托盘",
                    "关闭行为",
                    MessageBoxButton.YesNoCancel
                );
                if (res == MessageBoxResult.Yes)
                    return;
                if (res == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    this.HideWinInIcon();
                }
                if (res == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
            else
            {
                if (vm.IsMinimize)
                {
                    e.Cancel = true;
                    this.HideWinInIcon();
                }
                else
                    return;
            }
        }

        private void WinLoaded(object sender, RoutedEventArgs e)
        {
            if (vm != null)
            {
                if (vm.MinInIconWhenStart)
                {
                    this.HideWinInIcon();
                }
            }
        }

        private void WinClosed(object sender, EventArgs e)
        {
            using (FileStream fs = new FileStream(MainVM.configJsonFile, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.AutoFlush = true;
                    sw.Write(JsonSerializer.Serialize(vm));
                }
            }
        }
    }
}
