using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NotifyIcon_托盘图标;
using NotifyIcon_托盘图标.Utils;
using Forms = System.Windows.Forms;

namespace NotifyIcon_托盘图标.Utils
{
    public class AppNotifyIcon
    {
        private static readonly Lazy<AppNotifyIcon> instance = new Lazy<AppNotifyIcon>(() =>
            new AppNotifyIcon()
        );

        private readonly Forms.NotifyIcon icon;

        private static readonly Uri iconUri = new(
            AppDomain.CurrentDomain.BaseDirectory + @"Resources\devil_ico.ico",
            UriKind.RelativeOrAbsolute
        );

        public event Forms.MouseEventHandler? DoubleClickEvent;

        public event EventHandler? ExitClick;

        private AppNotifyIcon()
        {
            icon = new Forms.NotifyIcon();
            InitialIcon(icon);
        }

        private void InitialIcon(Forms.NotifyIcon icon)
        {
            icon.Icon = new Icon(iconUri.LocalPath);
            icon.Visible = true;
            icon.Text = "NotifyIcon_悬浮提示ToolTip";
            icon.BalloonTipTitle = "已启动";
            icon.BalloonTipText = "运行中。。。";

            //0
            Forms.ContextMenuStrip cms = new Forms.ContextMenuStrip();
            icon.ContextMenuStrip = cms;
            Forms.ToolStripMenuItem exitMenuItem = new Forms.ToolStripMenuItem("退出");
            exitMenuItem.Click += (sender, e) =>
            {
                ExitClick?.Invoke(sender, e);
            };
            cms.Items.Add(exitMenuItem);

            icon.MouseDoubleClick += (sender, e) =>
            {
                DoubleClickEvent?.Invoke(sender, e);
            };
            icon.ShowBalloonTip(1000);
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
        public static void AddAppNotifyIcon(this ServiceCollection services, App app)
        {
            services.AddTransient<AppNotifyIcon>((provider) => AppNotifyIcon.Instance);
            app.Exit += (object sender, ExitEventArgs e) =>
            {
                AppNotifyIcon.Instance.Close(e);
            };
        }
    }
}
