using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using MauiContactBook.Pages;
using MauiContactBook.Services;
using MauiContactBook.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiContactBook
{
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<ContactListPage>();
            builder.Services.AddSingleton<ContactListViewModel>();

            builder.Services.AddSingleton<ContactAddPage>();
            builder.Services.AddSingleton<ContactAddViewModel>();

            builder.Services.AddSingleton<ContactEditPage>();
            builder.Services.AddSingleton<ContactEditViewModel>();

            builder.Services.AddSingleton<ContactServices>();
            builder.Services.AddSingleton<IFileService, FileService>();
            builder.Services.AddSingleton<MauiContactServices>();


            return builder.Build();
        }
    }
}
