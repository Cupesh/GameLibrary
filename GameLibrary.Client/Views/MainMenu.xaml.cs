using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Views;

public partial class MainMenu : ContentPage
{
	public MainMenu(MainMenuViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}