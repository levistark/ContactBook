
using ContactBook_Console.Navigation;
using ContactBook_Console.Validation;
using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models;
using ContactBookLibrary.Services;
using System.Data;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace ContactBook_Console.Views;

public class ViewServices
{
    /// <summary>
    /// ViewServices används för att hantera applikationens olika vyer och UI. 
    /// </summary>

    private ContactServices _contactServices;

    // DI
    public ViewServices(ContactServices contactServices)
    {
        _contactServices = contactServices;
    }

    // Instansiering utav MenuNavigation och UserEntryValidation
    MenuNavigation nav = new MenuNavigation();
    UserEntryValidation _validation = new();

    /// <summary>
    /// Nedan följer 3 metoder som är till för att "förkorta" vissa Console.-metoder för att göra koden lite mer städad och lättläst. 
    /// </summary>
    public void WriteLine(string text) { Console.WriteLine(text); }
    public void Write(string text) { Console.Write(text); }
    public void Blank() { Console.WriteLine(); }
    public void Header(string text)
    {
        Console.Clear();
        WriteLine($"## {text.ToUpper()} ##");
        Blank();
    }

    /// <summary>
    /// Nedan följer applikationens olika vyer och menyer.
    /// </summary>
    
    // Startmenyn
    public void StartMenu()
    {
        Header("Contact Book");

        WriteLine("1. Show Contacts");
        WriteLine("2. Add New Contact");
        WriteLine("3. Exit Program");

        // Användning av MenuNavigation-klassen möjliggör att skapa navigering och validering på ett mer "cleant sätt".
        // Mer kommentarer om dessa finns i class-filen
        nav.AddMenuOption("1", ShowContactsMenu);
        nav.AddMenuOption("2", AddContactMenu);
        nav.AddMenuOption("3", ExitMenu);
        nav.MenuValidation();
    }

    // Alla kontakter-menyn
    public void ShowContactsMenu()
    {
        Header("all contacts");

        // Hämta alla kontakter med ContactServices
        var res = _contactServices.GetContacts();
        
        if (res.Result is List<IContact> list)
        {
            // Om det finns kontakter inlagda
            if (list.Count > 0)
            {
                // Instruktioner till användaren
                WriteLine("Select a contact by entering their list nr, or enter 'q' to go back");
                Blank();

                if (list.Count > 0)
                {
                    // Lista ut alla kontakter på ett snyggt och prydligt sätt
                    foreach (IContact contact in list)
                    {
                        WriteLine("List nr: " + list.IndexOf(contact));
                        WriteLine($"Full name: {contact.FirstName} {contact.LastName}");
                        Blank();
                    }
                }

                // Möjlighet att gå in och se detaljer om varje kontakt ges. 
                Write("Select contact list nr: ");

                // Här används UserEntryValidation för att validera användarens input, och sedan lagra det i variabeln validatedEntry
                var validatedEntry = _validation.ValidateUserEntry(Console.ReadLine()!);

                // Om användaren skrivit in 'q', gå tillbaka till Starten. 
                if (validatedEntry == "q")
                {
                    StartMenu();
                }
                // Annars gå vidare till ContactDetailsMenu med användarens input (som ska representera kontaktens index i listan)
                else
                {

                }
                    ContactDetailsMenu(validatedEntry);
            }

            // Om inga kontakter finns i listan
            else
            {
                WriteLine("No emplyoyees in list.");
                PressAnyKey();
            }
        }
        


    }

    // Lägg till kontakt-menyn
    public void AddContactMenu()
    {
        Header("Add new contact");

        // Användaren promptas till att skriva in värden för respektive property. 
        // För varje property så används _validation används för att kontrollera om fältet är tomt eller är lika med 'q'

        Write("Enter first name: ");
        string firstName = _validation.ValidateUserEntry(Console.ReadLine()!);
        if (firstName == "q")
        {
            StartMenu();

            // Förhindrar nästkommande logik genom att avsluta metoden här.
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


        // Lägger till ny kontakt och lagrar metodens ServiceResult i var result
        var result = _contactServices.AddContact(new Contact()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Phone = phone,
            Address = address
        });

        // Kontroller av ServiceResult.Status...

        if (result.Status == ServiceStatus.ALREADY_EXISTS)
        {
            Blank();
            WriteLine("A contact with this email address already exists in database. Please add a user with a unique email address.");
            PressAnyKey();
        }
        else if (result.Status == ServiceStatus.CREATED)
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

    // Kontaktdetaljer-menyn
    public void ContactDetailsMenu(string contactListIndex)
    {
        Header("Contact details");

        // Hämtar befintlig kontakt baserat på contactListIndex och lagrar ServiceResult i result
        var result = _contactServices.GetContact(contactListIndex);

        // Om Result representerar en Contact-instans, skriv ut dess värden 
        if (result.Result is Contact contact)
        {
            WriteLine("Contact Id: " + contact.Id);
            WriteLine($"First name: {contact.FirstName}");
            WriteLine($"Last name: {contact.LastName}");
            WriteLine("Email address: " + contact.Email);
            WriteLine("Phone number: " + contact.Phone);
            WriteLine("Address: " + contact.Address);
            Blank();

            WriteLine("1. Edit Contact");
            WriteLine("2. Delete Contact");
            WriteLine("3. Go back to Show All Contacts");

            // Användaren får 3 alternativ
            var option = Console.ReadLine();
            bool valid = false;

            // Validera användarens input
            while (valid == false)
            {
                switch (option) 
                {
                    case "1":
                        // Trigga metod och skicka med nuvarande kontakt, och dess list-index...
                        EditContactMenu(contact, contactListIndex);
                        valid = true;
                        break;
                    case "2":
                        // Trigga metod och skicka med nuvarande kontakt, och dess list-index...
                        DeleteContactMenu(contact, contactListIndex);
                        valid = true;
                        break;
                    case "3":
                        // Går tillbaks...
                        ShowContactsMenu();
                        valid = true;
                        break;
                    default:
                        // Felmeddelande
                        WriteLine("Invalid option, try again");
                        option = Console.ReadLine();
                        break;
                }
            }
      
        }
        // Om ServiceResult inte har en kontakt i sig
        else 
        {
            WriteLine("Couldn't display contact details.");
            Blank();
            PressAnyKey();
        }
    }

    // Redigera kontakt-menyn
    public void EditContactMenu(Contact contact, string contactListIndex)
    {
        Header("Edit contact");

        // Instruktioner
        WriteLine("Choose the number of the property you want to update, or press 'q' to go back");
        Blank();

        // Listar ut kontaktens properties 
        WriteLine("1. First name: " + contact.FirstName);
        WriteLine("2. Last name: " + contact.LastName);
        WriteLine("3. Email address: " + contact.Email);
        WriteLine("4. Phone number: " + contact.Phone);
        WriteLine("5. Address: " + contact.Address);
        Blank();

        var option = Console.ReadLine();
        bool valid = false;

        // Kontrollerar och validerar användarens input (option)
        while (valid == false)
        {
            switch (option)
            {
                // Skriver ut prompt
                case "1":
                    Blank();
                    Write("Enter new first name: ");
                    var input = Console.ReadLine();

                    // Kontrollera så att input inte är ett tomt fält
                    string validatedInput = _validation.ValidateUserEntry(input!);

                    // Uppdatera kontaktuppgifter genom skicka vidare Kontakt-objektet, det nya värdet, samt vald properties nummer.
                    var updateResult = _contactServices.UpdateContact(contact, validatedInput, option);

                    // En metod som Hanterar ServiceResult
                    HandleServiceResult(updateResult);

                    // Avslutar loop
                    valid = true;
                    break;

                case "2":
                    Blank();
                    Write("Enter new last name: ");
                    input = Console.ReadLine();

                    validatedInput = _validation.ValidateUserEntry(input!);
                    updateResult = _contactServices.UpdateContact(contact, validatedInput, option);
                    HandleServiceResult(updateResult);

                    valid = true;
                    break;

                case "3":
                    Blank();
                    Write("Enter new email: ");
                    input = Console.ReadLine();

                    validatedInput = _validation.ValidateUserEntry(input!);
                    updateResult = _contactServices.UpdateContact(contact, validatedInput, option);
                    HandleServiceResult(updateResult);

                    valid = true;
                    break;

                case "4":
                    Blank();
                    Write("Enter new phone number: ");
                    input = Console.ReadLine();

                    validatedInput = _validation.ValidateUserEntry(input!);
                    updateResult = _contactServices.UpdateContact(contact, validatedInput, option);
                    HandleServiceResult(updateResult);

                    valid = true;
                    break;

                case "5":
                    Blank();
                    Write("Enter new address: ");
                    input = Console.ReadLine();

                    validatedInput = _validation.ValidateUserEntry(input!);
                    updateResult = _contactServices.UpdateContact(contact, validatedInput, option);
                    HandleServiceResult(updateResult);

                    valid = true;
                    break;

                case "q":
                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;

                default:
                    break;
            }

            Thread.Sleep(1000);

            // Efter 1s, gå tillbaka till kontaktdetalj-menyn
            ContactDetailsMenu(contactListIndex);
        }
    }

    // Ta bort kontakt-menyn
    public void DeleteContactMenu(Contact contact, string contactListIndex)

    {
        // Kontrollera borttagning av kontakt
        Console.Clear();
        WriteLine($"Are you sure you want to delete {contact.FirstName} {contact.LastName} from the list (y/n)?");

        var option = Console.ReadLine();
        bool valid = false;

        // Hantera y/n
        while (valid == false)
        {
            switch (option)
            {
                case "y":
                    // Tar bort vald kontakt
                    var result = _contactServices.DeleteContact(contact);

                    // Hantera ServiceResult
                    if (result.Status == ServiceStatus.UPDATED)
                    {
                        Blank();
                        WriteLine("Contact was successfully deleted from list.");
                        Thread.Sleep(1000);
                        ShowContactsMenu();
                        valid = true;
                    }
                    else
                    {
                        Blank();
                        WriteLine("User couldn't be deleted. Please see debug log for more info.");
                        Thread.Sleep(1000);
                        ShowContactsMenu();
                        valid = true;
                    }
                    break;

                case "n":
                    // Går tillbaka till kontaktdetalj-menyn
                    ContactDetailsMenu(contactListIndex);
                    valid = true;
                    break;
                default:
                    // Repetera prompt
                    Console.Clear();
                    WriteLine($"Are you sure you want to delete {contact.FirstName} {contact.LastName} from the list (y/n)?");
                    option = Console.ReadLine();
                    break;
            }
        }
    }

    // En template för en "tryck på valfri knapp"-meny
    public void PressAnyKey()
    {
        Blank();
        WriteLine("Press any key to continue.");
        Console.ReadKey();
        StartMenu();
    }

    // Exit-menyn
    public void ExitMenu()
    {
        Console.Clear();
        WriteLine("Are you sure you want to exit (y/n)?");

        // Användning av MenuNavigation-klassen
        nav.AddMenuOption("y", Exit);
        nav.AddMenuOption("n", StartMenu);
        nav.MenuValidation();
    }

    // Metod som "avslutar" applikationen
    public void Exit()
    {

    }

    // Hanterar ServiceResults
    public void HandleServiceResult(IServiceResult serviceResult)
    {
        switch (serviceResult.Status)
        {
            case ServiceStatus.SUCCESS:
                Blank();
                WriteLine("Contact was successfully updated");
                break;

            case ServiceStatus.CREATED:
                Blank();
                WriteLine("Contact was successfully updated");
                break;

            case ServiceStatus.UPDATED:
                Blank();
                WriteLine("Contact was successfully updated");
                break;

            case ServiceStatus.FAILED:
                Blank();
                WriteLine("Something went wrong. Check debug message");
                break;

            case ServiceStatus.NOT_FOUND:
                Blank();
                WriteLine("Not found. Check debug message");
                break;
        }
 
    }

}
