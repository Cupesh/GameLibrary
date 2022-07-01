using Android.Content;
using GameLibrary.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameLibrary.Client.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string ValidationMessage { get; set; } = string.Empty;

        public ICommand OnCreateProfileClicked { get { return new Command(() => OnCreateProfile()); } }
        public ICommand OnBackButtonPressed { get { return new Command(() => BackButtonPressed()); } }

        public SignUpViewModel(IDataService dataService)
        {
            DataService = dataService;
        }

        public void OnCreateProfile()
        {
            ValidationMessage = string.Empty;
            RaisePropertyChanged(nameof(ValidationMessage));

            Username.Trim();
            if (Username.Length < 2 || Username.Length > 30)
            {
                ValidationMessage = "Username must be 2 - 30 characters";
                RaisePropertyChanged(nameof(ValidationMessage));
                return;
            }

            var passwordCheck = Password.Length >= 8 &&
                                Password.Count(c => char.IsDigit(c)) >= 1 &&
                                Password.Count(c => char.IsLetter(c)) >= 1;

            if (!passwordCheck)
            {
                ValidationMessage = "Password must be at least 8 characters. One letter, one digit.";
                RaisePropertyChanged(nameof(ValidationMessage));
                return;
            }

            if (String.IsNullOrEmpty(Region))
            {
                ValidationMessage = "Select a region.";
                RaisePropertyChanged(nameof(ValidationMessage));
                return;
            }
        }

        public bool BackButtonPressed()
        {
            return false;
        }
    }
}
