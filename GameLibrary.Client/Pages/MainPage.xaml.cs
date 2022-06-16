using GameLibrary.Client.Services;
using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}


