using GameLibrary.Client.Services;
using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}


