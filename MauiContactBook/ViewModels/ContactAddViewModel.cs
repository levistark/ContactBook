using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;
using ContactBookLibrary.Services;
using MauiContactBook.Services;
using Microsoft.Maui.Controls.Platform;
using Contact = ContactBookLibrary.Models.Contact;

namespace MauiContactBook.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    private readonly MauiContactServices _mauiContactServices;
    public ContactAddViewModel(MauiContactServices mauiContactServices)
    {
        _mauiContactServices = mauiContactServices;
    }

    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("..");
    }

    [ObservableProperty]
    private Contact contact = new();

    [RelayCommand]
    private async Task AddContact()
    {
        var result = _mauiContactServices.AddContactToList(Contact);
        Contact = new();
        await NavigateToList();

    }
}
