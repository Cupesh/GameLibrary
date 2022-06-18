using GameLibrary.Client.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Client.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel(IDataService dataService)
        {
            DataService = dataService;
            CheckUserProfile();
        }

        private async void CheckUserProfile()
        {
            if (!Preferences.ContainsKey("UserName"))
            {
                await Shell.Current.GoToAsync("signup");
            }
        }
    }
}
