using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp.Models;

namespace WpfApp.VMS
{
    public partial class ItemsGridVM : ObservableObject
    {
        [ObservableProperty]
        private int itemRow;

        [ObservableProperty]
        private int itemCol;

        [ObservableProperty]
        private int gridrows;

        [ObservableProperty]
        private int gridcols;

        public ObservableCollection<BtnItem> Items { get; set; } =
            new ObservableCollection<BtnItem>();

        [RelayCommand]
        private void Additem()
        {
            if (Gridcols <= ItemCol)
                Gridcols = (itemCol + 1);
            if (Gridrows <= ItemRow)
                Gridrows = (itemRow + 1);

            Items.Add(new() { Row = ItemRow, Col = ItemCol });
        }
    }
}
