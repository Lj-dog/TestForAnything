using Microsoft.Extensions.DependencyInjection;
using NotifyIcon_托盘图标.Utils;
using System.Configuration;
using System.Data;
using System.Windows;

namespace NotifyIcon_托盘图标
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceCollection? services;

        private IServiceProvider? provider;
        protected override void OnStartup(StartupEventArgs e)
        {
            services = new ServiceCollection();
            services.AddSingleton<MainWindow>();
            services.AddTransient<MainVM>();
            services.AddAppNotifyIcon(this);
            //services.AddSingleton<AppNotifyIcon>( privider => AppNotifyIcon.Instance );
            provider = services.BuildServiceProvider();

           
            App.Current.MainWindow = provider.GetService<MainWindow>();
            App.Current.MainWindow?.Show();
            base.OnStartup(e);
           
        }
    }

}
