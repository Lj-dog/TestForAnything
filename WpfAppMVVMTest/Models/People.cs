using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfAppMVVMTest.Models
{
    public partial class People : ObservableObject
    {
        //(1) 1 尝试使用BindingList来使DataGrid里单元格变化后触发事件。
        // 需要将全部属性改为可观察属性
        [ObservableProperty]
        private int iD;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private string? address;

        [ObservableProperty]
        private byte age;

        [ObservableProperty]
        private DateTime birthDay = DateTime.Now;

        //public int ID { get; set; }

        //public string? Name { get; set; }

        //public string? Description { get; set; }

        //public string? Address { get; set; }

        //public byte Age { get; set; }

        //public DateTime BirthDay { get; set; } = DateTime.Now;
    }
}
