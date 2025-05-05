using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp_Navigation.Models;

namespace WpfApp_Navigation.VMs
{
    public partial class ContentVM : ObservableObject
    {
        [ObservableProperty]
        private ContentBase? showContent = new ContentA();

        [RelayCommand]
        private void Next()
        {
            if (ShowContent is ContentA)
                ShowContent = new ContentB();
            else
                ShowContent = new ContentA();
        }

        [RelayCommand]
        private void Previous()
        {
            if (ShowContent is ContentA)
                ShowContent = new ContentB();
            else
                ShowContent = new ContentA();
        }
    }
}
