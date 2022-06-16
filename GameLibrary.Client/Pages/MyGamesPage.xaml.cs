using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Pages;

public partial class MyGamesPage : ContentPage
{
	public MyGamesPage(MyGamesViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}