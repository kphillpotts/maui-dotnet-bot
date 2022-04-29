using CommunityToolkit.Mvvm.ComponentModel;
using maui_dotnet_bot.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maui_dotnet_bot.ViewModels
{
    [INotifyPropertyChanged]
    public partial class BotPartGroupViewModel
    {
        [ObservableProperty]
        private BotPartViewModel selectedPart;

        public ObservableCollection<BotPartViewModel> Parts { get; set; }

        public BotPartGroupViewModel(string group, List<BotPartViewModel> parts)
        {
            Parts = new ObservableCollection<BotPartViewModel>(parts);
            selectedPart = Parts.FirstOrDefault(p => p.Default == true);
        }
    }
}
