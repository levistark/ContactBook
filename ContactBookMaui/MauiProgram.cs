using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using ContactBookMaui.ViewModels;
using Microsoft.Extensions.Logging;

namespace ContactBookMaui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ContactServices>();
        builder.Services.AddSingleton<IFileService, FileService>();

        builder.Logging.AddDebug();
        return builder.Build();
    }
}
