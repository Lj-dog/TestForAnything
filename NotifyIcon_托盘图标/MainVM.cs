using CommunityToolkit.Mvvm.ComponentModel;
namespace NotifyIcon_托盘图标
{
   public partial class MainVM:ObservableObject
    {

        public const string configJsonFile = "CloseConfig.json";

        [ObservableProperty]
        private bool minInIconWhenStart = false;

        [ObservableProperty]
        private bool askIsMinInIconWhenClose = true;

        [ObservableProperty]
        private bool isMinimize = true;
    }
}
