using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiContactBook.ViewModels;

public partial class ContactAddViewModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToList()
    {
        await Shell.Current.GoToAsync("//ContactListPage");
    }

}
