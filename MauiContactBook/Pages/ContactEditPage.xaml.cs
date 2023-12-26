using MauiContactBook.ViewModels;

namespace MauiContactBook.Pages;

public partial class ContactEditPage : ContentPage
{
	public ContactEditPage(ContactEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}