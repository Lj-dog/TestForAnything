using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace HostWPF
{
    public  class MainVM:ObservableObject
    {
        private readonly Dispatcher _dispatcher;

        public string Message { get; set; } = "Host";

        public string? LogLevel {  get; set; }
        public MainVM(IConfiguration configuration,Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            LogLevel = configuration["Logging:LogLevel:Microsoft"];
        }

        async Task FooAsync()
        {
            _dispatcher.Invoke(() => Message = "Hello");
        }
    }
}
