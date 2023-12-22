using MauiContactBook.ViewModels;

namespace MauiContactBook.Pages;

public partial class ContactListPage : ContentPage
{
	public ContactListPage(ContactListViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }


}