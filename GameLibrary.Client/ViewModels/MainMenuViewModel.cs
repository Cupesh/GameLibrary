using GameLibrary.Client.Services;
using GameLibrary.Shared.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameLibrary.Client.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public string SearchText { get; set; }

        public ICommand OnFindGameClicked { get { return new Command(() => FindGame()); } }

        public MainMenuViewModel(IDataService dataService)
        {
            DataService = dataService;
            CheckUserProfile();
        }

        private async void CheckUserProfile()
        {
            if (!Preferences.ContainsKey("Username"))
            {
                await Shell.Current.GoToAsync("signup");
            }
        }

        public async Task FindGame()
        {
            if (!CanExecute()) { return; }
            ErrorMessage = String.Empty;
            
            if (String.IsNullOrEmpty(SearchText))
            {
                ErrorMessage = "Enter a title or scan a barcode";
                return;
            }
            SearchText = SearchText.Trim();
            Loading = true;

            try
            {
                var resp = await DataService.FindGamesBySearchText(SearchText);
                if (resp.IsSuccess)
                {

                }
                else { await DisplayAlert("Ooops...", $"{resp.ErrorMessage}", "Ok"); }
            }
            catch (Exception ex) { await DisplayAlert("Ooops...", $"{ex.Message}", "Ok"); }

            Loading = false;
        }
    }
}
