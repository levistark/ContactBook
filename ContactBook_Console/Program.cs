
using ContactBook_Console.Validation;
using ContactBook_Console.Views;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactBook_Console;

internal class Program
{
    static void Main(string[] args)
    {
        // Instansiera klasser och services
        var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<ContactServices>();
            services.AddSingleton<ViewServices>();
            services.AddSingleton<UserEntryValidation>();
            
        }).Build();

        builder.Start();
        Console.Clear();

        var viewServices = builder.Services.GetRequiredService<ViewServices>();

        // Ladda upp start-menyn
        viewServices.StartMenu();
    }
}
