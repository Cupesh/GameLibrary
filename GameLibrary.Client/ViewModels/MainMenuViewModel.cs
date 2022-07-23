using GameLibrary.Client.Services;
using GameLibrary.Shared.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameLibrary.Client.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public string SearchText { get; set; }
        public string SearchResultText { get; set; }
        public ObservableCollection<PSXGame> searchResultGamesList = new();
        public ObservableCollection<PSXGame> SearchResultGamesList
        {
            get => searchResultGamesList;
            set
            {
                searchResultGamesList = value;
                RaisePropertyChanged(nameof(searchResultGamesList));
            }
        }

        public ICommand OnFindGameClicked { get { return new Command(async() => await FindGame()); } }

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
                    var games = resp.ApiData;
                    SearchResultText = games.Count == 1 ? $"-- 1 result --" : $"-- {games.Count} results --";
                    RaisePropertyChanged(nameof(SearchResultText));

                    foreach (var game in games)
                    {
                        SearchResultGamesList.Add(game);
                    }

                    RaisePropertyChanged(nameof(SearchResultGamesList));
                }
                else { await DisplayAlert("Ooops...", $"{resp.ErrorMessage}", "Ok"); }
            }
            catch (Exception ex) { await DisplayAlert("Ooops...", $"{ex.Message}", "Ok"); }

            Loading = false;
        }
    }
}
