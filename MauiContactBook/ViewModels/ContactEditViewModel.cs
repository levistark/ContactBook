using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContactBookLibrary.Enums;
using ContactBookLibrary.Interfaces;
using MauiContactBook.Services;
using Contact = ContactBookLibrary.Models.Contact;

namespace MauiContactBook.ViewModels;

public partial class ContactEditViewModel : ObservableObject
{
    private readonly IContactServices _contactServices;
    private readonly MauiViewServices _mauiViewServices;

    public ContactEditViewModel(IContactServices contactServices, MauiViewServices mauiViewServices)
    {
        _contactServices = contactServices;
        _mauiViewServices = mauiViewServices;
    }

    [ObservableProperty]
    private Contact contact = new();

    [RelayCommand]
    private void EditContact()
    {
        var result = _contactServices.UpdateContactFully(Contact);

        string statusMessage = _mauiViewServices.HandleServiceResult(result);
        
      
    }

    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("..");

    }

}
