using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WpfAppMVVMTest.Models;

namespace WpfAppMVVMTest.ViewModels
{
    class DataGridVM : ObservableObject
    {
        public ObservableCollection<People> Peoples { get; set; }
        public DataGridVM()
        {
            
        }
    }
}
