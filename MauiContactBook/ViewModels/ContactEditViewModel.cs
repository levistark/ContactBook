using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;
using MauiContactBook.Services;
using System.Linq.Expressions;
using Contact = ContactBookLibrary.Models.Contact;

namespace MauiContactBook.ViewModels;

public partial class ContactEditViewModel : ObservableObject, IQueryAttributable
{
    private readonly MauiContactServices _mauiContactServices;

    public ContactEditViewModel(MauiContactServices mauiContactServices)
    {
        _mauiContactServices = mauiContactServices;
    }

    [ObservableProperty]
    private Contact contact = new();

    [RelayCommand]
    private async Task EditContact()
    {
        _mauiContactServices.UpdateContact(Contact);
        Contact = new();
        await NavigateToList();
    }

    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("..");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Contact = (query["Contact"] as Contact)!;
    }
}
