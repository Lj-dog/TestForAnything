using Bogus;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfAppMVVMTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Randomizer.Seed = new Random(1334);
        }
    }

}
