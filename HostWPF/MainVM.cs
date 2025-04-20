using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HostWPF.Services;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
using Serilog;

namespace HostWPF
{
    public partial class MainVM : ObservableObject
    {
        [ObservableProperty]
        string title = "Host";

        private readonly Dispatcher _dispatcher;
        private readonly ILogger logger;
        private readonly ICatFactsService catFactsService;
        private readonly IMessageBoxService messageBoxService;

        public string? LogLevel { get; set; }

        public string? Version { get; set; }

        [ObservableProperty]
        private ObservableCollection<string> catFacts  = new ObservableCollection<string>();


        //整个源更换时无法实现通知界面
        //public ObservableCollection<string> CatFacts = new ObservableCollection<string>();


        public MainVM()
        {
            
        }

        public MainVM(IConfiguration configuration, Dispatcher dispatcher,ILogger logger,ICatFactsService catFactsService,IMessageBoxService messageBoxService)
        {
            _dispatcher = dispatcher;
            this.logger = logger;
            this.catFactsService = catFactsService;
            this.messageBoxService = messageBoxService;
            LogLevel = configuration["Logging:LogLevel:Microsoft"];
            Version = configuration["Logging:LogLevel:Default"];
            this.logger.Information("MainVM Loaded,LogLevel: {LogLevel},Version: {Version}", LogLevel,Version);
        }

        async Task FooAsync()
        {
            _dispatcher.Invoke(() => Title = "CatFacts");
        }

        [RelayCommand]
        async Task GetFacts(string text)
        {
            if (int.TryParse(text,out int limit))
            {
                if (limit>0)
                {
                    var facts = await catFactsService.GetCatFactsAsync(limit);
                    foreach (var item in facts)
                    {
                        CatFacts.Add(item);
                    }  
                }
                else
                {
                    messageBoxService.ShowMessage("Limit should over zero");
                }
            }
            else
            {
                messageBoxService.ShowMessage("Worng limit input");
            }
        }

        [RelayCommand]
        async Task ChangeObsCollection()
        {
            CatFacts= new ObservableCollection<string>() { "1","2"};
        }
    }
}
