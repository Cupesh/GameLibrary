using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Pages;

public partial class MainMenuPage : ContentPage
{
	public MainMenuPage(MainMenuViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}