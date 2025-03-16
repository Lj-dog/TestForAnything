using CommunityToolkit.Mvvm.ComponentModel;
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

        ObservableCollection<bool> checkList;
    }
}
