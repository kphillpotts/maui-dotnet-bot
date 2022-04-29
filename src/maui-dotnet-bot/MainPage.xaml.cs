using Svg.Skia;

namespace maui_dotnet_bot;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var background = await LoadRawSvg("backgrounds/dotnet-bot-backgrounds_clown.svg");
        var foreground = await LoadRawSvg("test.svg");

        // add to svg collection
        SvgCanvas.SvgLayers.Add(background);
        SvgCanvas.SvgLayers.Add(foreground);
    }

    private SKSvg ReadEmbeddedSvg(string resourceName)
    {
        SKSvg svg = new SKSvg();
        using (var stream = GetType().Assembly.GetManifestResourceStream(resourceName))
        {
            svg.Load(stream);
        }
        return svg;
    }

    async Task<SKSvg> LoadRawSvg(string fileName)
    {
        // read the file
        using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(stream);
        var contents = reader.ReadToEnd();

        //load into SVG
        SKSvg svg = new SKSvg();
        svg.FromSvg(contents);
        return svg;
    }
}

