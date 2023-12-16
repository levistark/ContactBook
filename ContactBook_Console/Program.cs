
using ContactBook_Console.Validation;
using ContactBook_Console.Views;
using ContactBookLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactBook_Console;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {
            services.AddSingleton<ContactServices>();
            services.AddSingleton<ViewServices>();
            services.AddSingleton<UserEntryValidation>();
            
        }).Build();

        builder.Start();
        Console.Clear();

        var viewServices = builder.Services.GetRequiredService<ViewServices>();

        viewServices.StartMenu();
    }
}
