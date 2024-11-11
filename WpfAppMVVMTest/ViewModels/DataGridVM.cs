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
  partial  class DataGridVM : ObservableObject
    {

        [ObservableProperty]
        private People people;

        public ObservableCollection<People> Peoples {  get; set; }
        public DataGridVM()
        {
            
        }
    }
}
