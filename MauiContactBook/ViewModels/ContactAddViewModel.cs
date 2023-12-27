using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using Contact = ContactBookLibrary.Models.Contact;

namespace MauiContactBook.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    private readonly ContactServices _contactServices;
    public ContactAddViewModel(ContactServices contactServices)
    {
        _contactServices = contactServices;
    }

    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("..");
    }

    [ObservableProperty]
    private Contact contact = new();

    [RelayCommand]
    private void AddContact()
    {
        var result = _contactServices.GetContacts();

        // Om resultatet är en listan med kontakter
        if (result is List<IContact> list)
        {
            // Om resultatet inte innehåller en kontakt med samma mejladress
            if (list.Any(c => c.Email != Contact.Email))
            {
                // Lägg till kontakt i listan
                var addResult = _contactServices.AddContact(Contact);
                Contact = new();
            }
            else
            {
                // Annars, prompta användaren med pop-up meddelande
            }
        }
        // Om resultatet inte är en listan med kontakter
        else
        {
            // Skicka pop-up fel-meddelande
        }
    }
}
