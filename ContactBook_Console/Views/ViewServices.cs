
using ContactBook_Console.Navigation;
using ContactBook_Console.Validation;
using ContactBookLibrary.Models;
using ContactBookLibrary.Services;
using System.Data;
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
    UserEntryValidation _validation = new();

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
        Header("Add new contact");

        Write("Enter first name: ");
        string firstName = _validation.ValidateUserEntry(Console.ReadLine()!);
        if (firstName == "q")
        {
            StartMenu();
            return;
        }

        Write("Enter last name: ");
        string lastName = _validation.ValidateUserEntry(Console.ReadLine()!);
        if (lastName == "q")
        {
            StartMenu();
            return;
        }

        Write("Enter email: ");
        string email = _validation.ValidateUserEntry(Console.ReadLine()!);
        if (email == "q")
        {
            StartMenu();
            return;
        }

        Write("Enter phone: ");
        string phone = _validation.ValidateUserEntry(Console.ReadLine()!);
        if (phone == "q")
        {
            StartMenu();
            return;
        }

        Write("Enter address: ");
        string address = _validation.ValidateUserEntry(Console.ReadLine()!);
        if (address == "q")
        {
            StartMenu();
            return;
        }

        var result = _contactServices.AddContact(new Contact()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Address = address
        });

        if (result.Status == ContactBookLibrary.Enums.ServiceStatus.ALREADY_EXISTS)
        {
            Blank();
            WriteLine("A contact with this email address already exists in database. Please add a user with a unique email address.");
            PressAnyKey();
        }
        else if (result.Status == ContactBookLibrary.Enums.ServiceStatus.CREATED)
        {
            Blank();
            WriteLine("New contact was added to the list.");
            PressAnyKey();
        }
        else
        {
            Blank();
            WriteLine("Contact could not be added to the list. Please see error log message.");
            PressAnyKey();
        }
    }

    public void PressAnyKey()
    {
        WriteLine("Press any key to continue.");
        Console.ReadKey();
        StartMenu();
    }
    public void ExitMenu()
    {
        Console.Clear();
        WriteLine("Are you sure you want to exit (y/n)?");

        nav.AddMenuOption("y", Exit);
        nav.AddMenuOption("n", StartMenu);
        nav.MenuValidation();
    }
    public void Exit()
    {

    }
}
