using ContactBookLibrary.Interfaces;
using ContactBookMaui.ViewModels;
using System.Collections.ObjectModel;

namespace ContactBookMaui;
public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
