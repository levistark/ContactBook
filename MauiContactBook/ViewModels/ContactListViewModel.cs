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
    public ContactListViewModel(MauiContactServices mauiContactServices)
    {
        _mauiContactServices = mauiContactServices;
        _mauiContactServices.ContactsUpdated += (sender, e) =>
        {
            UpdateContactList();

        };
    }

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    [RelayCommand]
    private static async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("ContactAddPage");
    }

    [RelayCommand]
    private static async Task NavigateToEdit(Contact contact)
    {
        var parameters = new ShellNavigationQueryParameters
        {
            {"Contact", contact }
        };

        await Shell.Current.GoToAsync("ContactEditPage", parameters);

    }

    [RelayCommand]
    private void Remove(Contact contact)
    {
        _mauiContactServices.RemoveContactFromList(contact);
        UpdateContactList();
    }

    public void UpdateContactList()
    {
        var list = _mauiContactServices.GetContactsFromList();
        Contacts = new ObservableCollection<Contact>(list!.Cast<Contact>());
    }



}