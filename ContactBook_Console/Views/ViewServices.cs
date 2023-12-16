
using ContactBook_Console.Navigation;
using ContactBookLibrary.Services;
using System.Security.Cryptography.X509Certificates;

namespace ContactBook_Console.Views;

public class ViewServices
{

    private ContactServices _contactServices;
    public ViewServices(ContactServices contactServices)
    {
        _contactServices = contactServices;
    }

    MenuNavigation nav = new MenuNavigation();

    public void WriteLine(string text) { Console.WriteLine(text); }
    public void Write(string text) { Console.Write(text); }
    public void Blank() { Console.WriteLine(); }
    public void DashedLine() { Console.WriteLine("--------------------------"); }

    public void StartMenu()
    {
        Header("Contact Book");

        WriteLine("1. Show Contacts");
        WriteLine("2. Add New Contact");
        WriteLine("3. Exit Program");

        nav.AddMenuOption("1", ShowContactsMenu);
        nav.AddMenuOption("2", AddContactMenu);
        nav.AddMenuOption("3", ExitMenu);
        nav.MenuValidation();
    }

    public void Header(string text)
    {
        Console.Clear();
        WriteLine($"## {text.ToUpper()} ##");
        Blank();
    }

    public void ShowContactsMenu()
    {
        Header("all contacts");
        WriteLine("Select a contact by entering their list nr, or enter 'q' to go back");

        // CONTACT LIST......

        var userEntry = Console.ReadLine();
    }

    public void AddContactMenu()
    {

    }

    public void ExitMenu()
    {

    }
}
