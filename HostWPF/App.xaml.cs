using System.Configuration;
using System.Data;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using HostWPF.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HostWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();
            host.Start();

            var app = new App();
            app.InitializeComponent();
            app.MainWindow = host.Services.GetRequiredService<MainWindow>();
            app.MainWindow.Show();
            app.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(container =>
                {
                    container.AddHostedService<CheckUpdateService>();

                    container.AddSingleton<MainWindow>(sp =>
                    {
                        //lambda 语句块与表达式的区别
                        return new MainWindow() { DataContext = sp.GetRequiredService<MainVM>() };
                    });

                    container.AddSingleton<MainVM>();

                    container.AddSingleton<WeakReferenceMessenger>();
                    container.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());

                    container.AddSingleton(_ => Current.Dispatcher);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                    //logging.AddSerilog(Log.Logger);
                    logging.Services.AddSingleton(Log.Logger);
                });
        }
    }
}
