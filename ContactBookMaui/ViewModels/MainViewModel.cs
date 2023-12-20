using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using System.Collections.ObjectModel;

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

    }

    public void UpdateContactList()
    {
        ContactList = new ObservableCollection<IContact>((List<IContact>)_contactServices.GetContacts().Result);
    }
}
