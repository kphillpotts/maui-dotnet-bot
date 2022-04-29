using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using maui_dotnet_bot.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace maui_dotnet_bot.ViewModels
{
    [INotifyPropertyChanged]
    public partial class MainViewModel 
    {
        [ObservableProperty()]
        BotPartGroupViewModel antenna;

        [ObservableProperty]
        BotPartGroupViewModel arms;

        [ObservableProperty]
        BotPartGroupViewModel backgrounds;

        [ObservableProperty]
        BotPartGroupViewModel eyes;

        [ObservableProperty]
        BotPartGroupViewModel eyewear;

        [ObservableProperty]
        BotPartGroupViewModel headgear;

        [ObservableProperty]
        BotPartGroupViewModel legs;

        [ObservableProperty]
        BotPartGroupViewModel parts;
        
        public async Task LoadBotPartsAsync()
        {
            Antenna = await GetBotParts("antenna");
            Arms = await GetBotParts("arms");
            Backgrounds = await GetBotParts("backgrounds");
            Eyes = await GetBotParts("eyes");
            Eyewear = await GetBotParts("eyewear");
            Headgear = await GetBotParts("headgear");
            Legs = await GetBotParts("legs");
        }

        private async Task<BotPartGroupViewModel> GetBotParts(string partGroupName)
        {
            // setup my yaml deserialzer
            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

            // read yml file
            string fileName = $"_data/{partGroupName}.yml";
            using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            using var reader = new StreamReader(stream);
            var ymlContent = reader.ReadToEnd();

            // deserialize into a collection of parts
            var parts = deserializer.Deserialize<List<BotPart>>(ymlContent);

            // create a group from all the parts
            List<BotPartViewModel> botParts = new();
            botParts.AddRange(parts.Select(part => new BotPartViewModel(partGroupName, part)));
            BotPartGroupViewModel group = new BotPartGroupViewModel(partGroupName, botParts);

            return group;
        }
    }
}
