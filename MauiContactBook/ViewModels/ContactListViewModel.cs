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
    private readonly ContactServices _contactServices;
    MauiContactServices _mauiContactServices;
    public ContactListViewModel(ContactServices contactServices, MauiContactServices mauiContactServices)
    {
        _contactServices = contactServices;
        _mauiContactServices = mauiContactServices;
        UpdateContactList();
    }

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    [RelayCommand]
    private static async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("ContactAddPage");
    }

    [RelayCommand]
    private static async Task NavigateToEdit()
    {
        await Shell.Current.GoToAsync("ContactEditPage");

    }

    [RelayCommand]
    private void Remove()
    {

    }

    public void UpdateContactList()
    {
        var contactList = _contactServices.GetContacts().Result as IEnumerable<IContact>;
        Contacts = new ObservableCollection<Contact>(contactList!.Cast<Contact>());
    }



}