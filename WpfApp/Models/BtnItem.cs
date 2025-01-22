using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp.Models
{
    public partial class BtnItem:ObservableObject
    {
        [ObservableProperty]
        private int row;

        [ObservableProperty]
        private int col;

        //public int Row {  get; set; }

        //public int Col { get; set; }
    }
}
