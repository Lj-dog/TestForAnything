using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMVVMTest.ViewModels
{
    public partial class RadioVM: ObservableObject
    {
        [ObservableProperty]
        private bool checked1;

        [ObservableProperty]
        private bool checked2;

        [ObservableProperty]
        ObservableCollection<bool> checkList;

        [RelayCommand]
        private void Changedchecked1()
        {
            Checked1 = !Checked1;
        }

        [RelayCommand]
        private void Changedchecked2()
        {
            Checked2 = !Checked2;
        }
    }
}
