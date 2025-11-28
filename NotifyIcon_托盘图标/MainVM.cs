using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyIcon_托盘图标
{
   public partial class MainVM:ObservableObject
    {
        [ObservableProperty]
        private bool minInIconWhenStart = true;

        [ObservableProperty]
        private bool askIsMinInIconWhenClose = true;
    }
}
