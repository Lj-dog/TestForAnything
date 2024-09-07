using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClientWpfApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string ip = "127.0.0.1";

        public string IP
        {
            get
            { return ip; }
            set
            {
                if (value != ip)
                {
                    ip = value;
                    RaisePropertyChanged(nameof(IP));
                }
            }
        }

        private int port = 100;

        public int Port
        {
            get { return port; }
            set
            {
                if (value != port)
                {
                    port = value;
                    RaisePropertyChanged(nameof(Port));
                }
            }
        }
    }
}