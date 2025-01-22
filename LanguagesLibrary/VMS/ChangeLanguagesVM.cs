using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using LanguagesLibrary.MS;

namespace LanguagesLibrary.VMS
{
    internal partial class ChangeLanguagesVM : ObservableObject 
    {
        public ObservableCollection<Languages> Languages { get; set; }

        public ChangeLanguagesVM()
        {
            
        }
    }
}
