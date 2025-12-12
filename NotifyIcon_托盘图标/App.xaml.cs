using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text.Json;
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
            services.AddTransient<MainVM>((provider) => {

                try
                {  //加载配置
                    using (FileStream fs = new FileStream(MainVM.configJsonFile, FileMode.Open))
                    {
                        using(StreamReader sr = new(fs))
                        {
                            var str = sr.ReadToEnd();
                            return JsonSerializer.Deserialize<MainVM>(str)??new MainVM();
                        }
                    }
                }
                catch (FileNotFoundException e)
                {

                    return new MainVM();
                }
            });
            services.AddAppNotifyIcon(this);
            //services.AddSingleton<AppNotifyIcon>( privider => AppNotifyIcon.Instance );
            provider = services.BuildServiceProvider();

           
            App.Current.MainWindow = provider.GetService<MainWindow>();
            App.Current.MainWindow?.Show();
            base.OnStartup(e);
           
        }
    }

}
