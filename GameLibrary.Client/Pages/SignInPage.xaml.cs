using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Pages;

public partial class SignInPage : ContentPage
{
	public SignInPage(SignInViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
    }
}