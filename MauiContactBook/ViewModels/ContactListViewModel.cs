using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiContactBook.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
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

}