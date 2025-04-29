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
using CommunityToolkit.Mvvm.Messaging;
using WpfApp.Models;

namespace WpfApp.VMS
{
    public partial class ItemsGridVM : ObservableRecipient
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

        public List<BtnItem> SelectedItems { get; set; } = new List<BtnItem>();

        [RelayCommand]
        private void Additem()
        {
            if (Gridcols <= ItemCol)
                Gridcols = (ItemCol + 1);
            if (Gridrows <= ItemRow)
                Gridrows = (ItemRow + 1);

            Items.Add(new() { Row = ItemRow, Col = ItemCol });
        }

        [RelayCommand]
        private void Delitem()
        {
            foreach (var item in SelectedItems)
            {
                Items.Remove(item);
            }

            var maxRow = Items.Select(o => o.Row).Max();
            var maxCol = Items.Select(o=> o.Col).Max();
            Gridrows = maxRow + 1;
            Gridcols = maxCol + 1;
            SelectedItems.Clear();
        }


        public ItemsGridVM()
        {
            IsActive = true;
            //WeakReferenceMessenger.Default.Register<BtnItem>(this, (s, m) =>
            //{
            //    if (!SelectedItems.Contains(m))
            //        SelectedItems.Add(m);
            //});
        }
        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<ItemsGridVM, BtnItem>(this, (s, m) =>
            {
                if (SelectedItems.Contains(m))
                    SelectedItems.Remove(m);
                else
                    SelectedItems.Add(m);
            });
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }
    }
}
