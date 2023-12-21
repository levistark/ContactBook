using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Models;
using ContactBookLibrary.Services;
using System.Collections.ObjectModel;
using Contact = ContactBookLibrary.Models.Contact;

namespace ContactBookMaui.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ContactServices _contactServices;
    public MainViewModel(ContactServices contactServices)
    {
        _contactServices = contactServices;
        UpdateContactList();
    }

    [ObservableProperty]
    private Contact _addContactForm = new();

    [ObservableProperty]
    private ObservableCollection<IContact> _contactList = [];

    [RelayCommand]
    public void AddContactToList()
    {
        if (AddContactForm != null 
            && !string.IsNullOrWhiteSpace(AddContactForm.FirstName)
            && !string.IsNullOrWhiteSpace(AddContactForm.LastName)
            && !string.IsNullOrWhiteSpace(AddContactForm.Email)
            && !string.IsNullOrWhiteSpace(AddContactForm.Phone)
            && !string.IsNullOrWhiteSpace(AddContactForm.Address))
        {
            var result = _contactServices.AddContact(AddContactForm);

            if (result.Status == ContactBookLibrary.Enums.ServiceStatus.CREATED)
            {
                UpdateContactList();
                AddContactForm = new();
            }
        }
    }

    [RelayCommand]
    public void DeleteContactFromList(Contact contact)
    {
        var result = _contactServices.DeleteContact(contact);

        if (result.Status == ContactBookLibrary.Enums.ServiceStatus.UPDATED)
        {
            UpdateContactList();
        }
    }

    public void UpdateContactList()
    {
        ContactList = new ObservableCollection<IContact>((List<IContact>)_contactServices.GetContacts().Result);
    }
}
