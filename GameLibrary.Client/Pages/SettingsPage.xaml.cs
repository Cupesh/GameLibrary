using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}