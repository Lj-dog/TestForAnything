using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp_Navigation.VMs
{
   public partial class Page1VM:ObservableObject
    {
        [RelayCommand]
        private void KeyBing()
        {
            MessageBox.Show("Ctrl + S");
        }
    }
}
