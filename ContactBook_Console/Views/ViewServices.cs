
using ContactBook_Console.Navigation;
using ContactBook_Console.Validation;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models;
using ContactBookLibrary.Services;
using System.Data;
using System.Numerics;
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
        

        var res = _contactServices.GetContacts();
        
        if (res.Result is List<IContact> list)
        {

            WriteLine("Select a contact by entering their list nr, or enter 'q' to go back");
            Blank();

            if (list.Count > 0)
            {
                foreach (IContact contact in list)
                {
                    WriteLine("List nr: " + list.IndexOf(contact));
                    WriteLine($"Full name: {contact.FirstName} {contact.LastName}");
                    Blank();
                }
            }

            Blank();
            Write("Select contact list nr: ");

            var validatedEntry = _validation.ValidateUserEntry(Console.ReadLine()!);
            if (validatedEntry == "q")
                StartMenu();
            else
                ContactDetailsMenu(validatedEntry);
        }
        else
        {
            WriteLine("No emplyoyees in list.");
            PressAnyKey();
        }


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
    public void ContactDetailsMenu(string contactListIndex)
    {
        Header("Contact details");

        var result = _contactServices.GetContact(contactListIndex);

        if (result.Result is IContact contact)
        {
            WriteLine("Contact Id: " + contact.Id);
            WriteLine($"Full name: {contact.FirstName} {contact.LastName}");
            WriteLine("Email address: " + contact.Email);
            WriteLine("Phone number: " + contact.Phone);
            WriteLine("Address: " + contact.Address);
            Blank();

            WriteLine("1. Edit Contact");
            WriteLine("2. Delete Contact");
            WriteLine("3. Go back to Show All Contacts");

            var option = Console.ReadLine();
            bool valid = false;

            while (valid == false)
            {
                switch (option) 
                {
                    case "1":
                        EditContactMenu(contact, contactListIndex);
                        valid = true;
                        break;
                    case "2":
                        DeleteContactMenu(contact, contactListIndex);
                        valid = true;
                        break;
                    case "3":
                        ShowContactsMenu();
                        valid = true;
                        break;
                    default:
                        WriteLine("Invalid option, try again");
                        option = Console.ReadLine();
                        break;
                }
            }
      
        }
        else 
        {
            WriteLine("Couldn't display contact details.");
            Blank();
            PressAnyKey();
        }
    }
    public void EditContactMenu(IContact contact, string contactListIndex)
    {
        Header("Edit contact");

        WriteLine("Choose the number of the property you want to update, or press 'q' to go back");
        Blank();

        WriteLine("1. First name: " + contact.FirstName);
        WriteLine("2. Last name: " + contact.LastName);
        WriteLine("3. Email address: " + contact.Email);
        WriteLine("4. Phone number: " + contact.Phone);
        WriteLine("5. Address: " + contact.Address);
        Blank();

        var option = Console.ReadLine();
        bool valid = false;

        while (valid == false)
        {
            switch (option)
            {
                case "1":
                    Blank();
                    Write("Enter new first name: ");
                    var input = Console.ReadLine();
                    contact.FirstName = _validation.ValidateUserEntry(input!);

                    Blank();
                    WriteLine("First name was successfully updated ");
                    Thread.Sleep(1000);

                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;

                case "2":
                    Blank();
                    Write("Enter new last name: ");
                    input = Console.ReadLine();
                    contact.LastName = _validation.ValidateUserEntry(input!);

                    Blank();
                    WriteLine("Last name was successfully updated ");
                    Thread.Sleep(1000);

                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;

                case "3":
                    Blank();
                    Write("Enter new email: ");
                    input = Console.ReadLine();
                    contact.Email = _validation.ValidateUserEntry(input!);

                    Blank();
                    WriteLine("Email was successfully updated ");
                    Thread.Sleep(1000);

                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;

                case "4":
                    Blank();
                    Write("Enter new phone number: ");
                    input = Console.ReadLine();
                    contact.Phone = _validation.ValidateUserEntry(input!);

                    Blank();
                    WriteLine("Phone number was successfully updated ");
                    Thread.Sleep(1000);

                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;

                case "5":
                    Blank();
                    Write("Enter new address: ");
                    input = Console.ReadLine();
                    contact.Address = _validation.ValidateUserEntry(input!);

                    Blank();
                    WriteLine("Address was successfully updated ");
                    Thread.Sleep(1000);

                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;

                case "q":
                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;

                default:
                    break;
            }
        }

    }
    public void DeleteContactMenu(IContact contact, string contactListIndex)
    {
        Console.Clear();
        WriteLine($"Are you sure you want to delete {contact.FirstName} {contact.LastName} from the list (y/n)?");

        var option = Console.ReadLine();
        bool valid = false;

        while (valid == false)
        {
            switch (option)
            {
                case "y":
                    var result = _contactServices.DeleteContact(contact);

                    if (result.Status == ContactBookLibrary.Enums.ServiceStatus.DELETED)
                    {
                        Console.Clear();
                        WriteLine("Contact was successfully deleted from list.");
                        Thread.Sleep(1000);
                        ShowContactsMenu();
                        valid = true;
                    }
                    else
                    {
                        Console.Clear();
                        WriteLine("User couldn't be deleted. Please see debug log for more info.");
                        Thread.Sleep(1000);
                        ShowContactsMenu();
                        valid = true;
                    }
                    break;

                case "n":
                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;
                default:
                    Console.Clear();
                    WriteLine($"Are you sure you want to delete {contact.FirstName} {contact.LastName} from the list (y/n)?");
                    option = Console.ReadLine();
                    break;
            }
        }
    }
    public void PressAnyKey()
    {
        Blank();
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
