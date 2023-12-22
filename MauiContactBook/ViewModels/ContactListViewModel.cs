using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiContactBook.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToAdd()
    {
        await Shell.Current.GoToAsync("//ContactAddPage");
    }

}