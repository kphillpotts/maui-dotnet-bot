using CommunityToolkit.Mvvm.ComponentModel;
using maui_dotnet_bot.Models;

namespace maui_dotnet_bot.ViewModels
{
    [INotifyPropertyChanged]
    public partial class BotPartViewModel
    {
        private BotPart part;
        private string group;

        public BotPartViewModel(string group, BotPart part)
        {
            this.part = part;
            this.group = group;
        }

        public string Category => group;
        public string Title => part.Title;
        public string Icon => $"objects/{group}/icons/{part.Icon}";
        public string Image => $"objects/{group}/{part.File}";
        public bool Default => part.Default;
        public bool HideAntenna => part.HideAntenna;
    }
}
