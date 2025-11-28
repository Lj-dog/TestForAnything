using NotifyIcon_托盘图标;
using NotifyIcon_托盘图标.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace NotifyIcon_托盘图标.Utils
{
    public class AppNotifyIcon
    {
        private static readonly Lazy<AppNotifyIcon> instance = new Lazy<AppNotifyIcon>(() => new AppNotifyIcon());
   
        private readonly NotifyIcon icon;

        private readonly static Uri iconUri = new(AppDomain.CurrentDomain.BaseDirectory + @"Resources\devil_ico.ico",UriKind.RelativeOrAbsolute);

        private AppNotifyIcon() {

            icon = new NotifyIcon();
            InitialIcon(icon);

        }

        private void InitialIcon(NotifyIcon icon)
        {
            icon.BalloonTipTitle = " test";
            icon.Icon = new Icon(iconUri.LocalPath);
            icon.Visible = true;
        }

        public void Close(ExitEventArgs e)
        {
            icon.Dispose();
        }

    
        public static AppNotifyIcon Instance => instance.Value;
    }

   
}

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppNotifyIconExension
    {
        public static void AddAppNotifyIcon(this ServiceCollection services,App app)
        {
            services.AddTransient<AppNotifyIcon>((provider)=> AppNotifyIcon.Instance);
            app.Exit += (object sender, ExitEventArgs e) => { AppNotifyIcon.Instance.Close(e); };
        }
    }
}
