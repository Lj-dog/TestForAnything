using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        [RelayCommand]
        private void Selected()
        {
            WeakReferenceMessenger.Default.Send<BtnItem>(this);
        }

        [RelayCommand]
        private void DClick(BtnItem btnItem)
        {
            MessageBox.Show($"DClickItem is row:{btnItem.Row} , col:{btnItem.Col}");
        }
    }
}
