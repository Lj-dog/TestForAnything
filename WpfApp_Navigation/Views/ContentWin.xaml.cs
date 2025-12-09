using System;
using System.Collections.Generic;
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
using WpfApp_Navigation.VMs;

namespace WpfApp_Navigation
{
    /// <summary>
    /// ContentWin.xaml 的交互逻辑
    /// </summary>
    public partial class ContentWin : Window
    {
        public ContentWin()
        {
            InitializeComponent();
            this.DataContext = new ContentVM();
        }
    }
}
