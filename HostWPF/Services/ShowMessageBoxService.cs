using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostWPF.Services
{
    public class ShowMessageBoxService: IMessageBoxService
    {
        public void ShowMessage(string message)
        {
            System.Windows.MessageBox.Show(message);
        }
    }
  
}
