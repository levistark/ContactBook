using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using MauiContactBook.Services;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Collections.ObjectModel;
using Contact = ContactBookLibrary.Models.Contact;


namespace MauiContactBook.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    MauiContactServices _mauiContactServices;

    // Lägg in event-triggern från MauiContactServices
    public ContactListViewModel(MauiContactServices mauiContactServices)
    {
        _mauiContactServices = mauiContactServices;
        _mauiContactServices.ContactsUpdated += (sender, e) =>
        {
            UpdateContactList();
        };

        // Metod som uppdaterar listan av kontakter
        UpdateContactList();
    }

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    /// <summary>
    /// Metod som navigerar till lägga till-menyn
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private static async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("ContactAddPage");
    }

    /// <summary>
    /// Metod som navigerar till edit-vyn
    /// </summary>
    /// <param name="contact">Contact model</param>
    /// <returns></returns>
    [RelayCommand]
    private static async Task NavigateToEdit(Contact contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"Contact", contact }
        };

        await Shell.Current.GoToAsync("ContactEditPage", parameters);
    }

    /// <summary>
    /// Metod som ber MauiContactServices att ta bort en kontakt ur listan
    /// </summary>
    /// <param name="contact">Contact model</param>
    [RelayCommand]
    private void Remove(Contact contact)
    {
        _mauiContactServices.RemoveContactFromList(contact);
        UpdateContactList();
    }

    /// <summary>
    /// Metod som uppdaterar listan av kontakter
    /// </summary>
    public void UpdateContactList()
    {
        var list = _mauiContactServices.GetContactsFromList();
        Contacts = new ObservableCollection<Contact>(list!.Cast<Contact>());
    }



}