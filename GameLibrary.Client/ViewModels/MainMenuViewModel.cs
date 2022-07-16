﻿using GameLibrary.Client.Services;
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
        public bool IsLoading { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

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
    }
}
