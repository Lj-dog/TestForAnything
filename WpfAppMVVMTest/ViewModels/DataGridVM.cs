using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfAppMVVMTest.Models;

namespace WpfAppMVVMTest.ViewModels
{
    public partial class DataGridVM : ObservableObject
    {
        [ObservableProperty]
        private People people = new();

        [ObservableProperty]
        private People selectedPeople;

        public ObservableCollection<People> Peoples { get; set; } = new();

        // (1)尝试使用BindingList来使DataGrid里单元格变化后触发事件。
        //[ObservableProperty]
        //private BindingList<People> _peoples;

        public DataGridVM()
        {
            //(1)
            #region 1 尝试使用BindingList来使DataGrid里单元格变化后触发事件。
            //Peoples = new();
            //Peoples.ListChanged += Peoples_ListChanged;
            #endregion
        }

        //(1)
        #region 1 尝试使用BindingList来使DataGrid里单元格变化后触发事件。
        //private void Peoples_ListChanged(object? sender, ListChangedEventArgs e)
        //{
        //    MessageBox.Show("ListChangeded");
        //}
        #endregion

        [RelayCommand]
        private void BtnAdd()
        {
            Peoples.Add(
                new()
                {
                    Address = People.Address,
                    Age = People.Age,
                    Name = People.Name,
                    ID = People.ID,
                    BirthDay = People.BirthDay,
                    Description = People.Description,
                }
            );
        }

        [RelayCommand]
        private void BtnRemove(IList selectedPeoples)
        {
            if (selectedPeoples != null && selectedPeoples.Count > 0)
            {
                List<People> deletePeoples = selectedPeoples.OfType<People>().ToList();
                foreach (var item in deletePeoples)
                {
                    if (item is People people)
                    {
                        Peoples.Remove(people);
                    }
                }
            }
        }
    }
}
