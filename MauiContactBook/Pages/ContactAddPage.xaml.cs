using MauiContactBook.ViewModels;

namespace MauiContactBook.Pages;

public partial class ContactAddPage : ContentPage
{
	public ContactAddPage(ContactAddViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}