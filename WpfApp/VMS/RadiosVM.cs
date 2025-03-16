using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.VMS
{
    public partial class RadiosVM: ObservableObject
    {
        static bool switchChecked = false;

        [ObservableProperty]
        private bool isChecked_1;

        [ObservableProperty]
        private bool isChecked_2;

        [RelayCommand]
        private void Checked()
        {
            if(switchChecked)
            {
                switchChecked = false;
                IsChecked_1 = true;
                IsChecked_2 = true;
            }
            else
            {
                switchChecked = true;
                IsChecked_2 = true;
                IsChecked_1 = true;
            }
        }
    }
}
