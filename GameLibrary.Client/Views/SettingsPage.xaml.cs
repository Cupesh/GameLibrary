using GameLibrary.Client.ViewModels;

namespace GameLibrary.Client.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}