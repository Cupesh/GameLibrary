using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}