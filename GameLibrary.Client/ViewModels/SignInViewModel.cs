using GameLibrary.Client.Services;
using GameLibrary.Shared.Models;
using CommunityToolkit.Maui.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using System.Threading;

namespace GameLibrary.Client.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        public User User { get; set; } = new();

        public ICommand OnLoginClicked { get { return new Command(() => CheckInput()); } }
        public ICommand OnForgotPasswordClicked { get { return new Command(async() => await ForgotPasswordClicked()); } }

        public SignInViewModel(IDataService dataService)
        {
            DataService = dataService;
        }

        public async void CheckInput()
        {
            ErrorMessage = string.Empty;

            User.UserName.Trim();
            if (User.UserName.Length < 2 || User.UserName.Length > 30)
            {
                ErrorMessage = "Username must be 2 - 30 characters";
                return;
            }

            var passwordCheck = User.Password.Length >= 8;

            if (!passwordCheck)
            {
                ErrorMessage = "Password must be at least 8 characters. One letter, one digit.";
                return;
            }

            await Login();
        }

        private async Task Login()
        {
            Loading = true;

            try
            {
                var resp = await DataService.Login(User);
                if (resp.IsSuccess)
                {
                    OnSuccessfulLogin(resp.ApiData);
                }
                else { await DisplayAlert("Ooops...", "Invalid username or password", "Ok"); }
            }
            catch (Exception ex) { await DisplayAlert("Ooops...", $"{ex.Message}", "Ok"); }

            Loading = false;
        }

        private async void OnSuccessfulLogin(User user)
        {
            Preferences.Set("UserId", user.UserId);
            Preferences.Set("Username", user.UserName);
            Preferences.Set("Region", user.Region);
            Preferences.Set("Email", user.EmailAddress);

            await Shell.Current.GoToAsync("../..");
        }

        private async Task ForgotPasswordClicked()
        {
            string email = await DisplayPrompt("Enter email address", "If there is an email address associated with your profile " +
                "you will receive an email with steps on how to reset your password");

            if (!String.IsNullOrEmpty(email))
            {
                Loading = true;
                try
                {
                    var resp = await DataService.SendPwdRecoveryEmail(email);
                    if (resp.IsSuccess)
                    {
                        bool success = resp.ApiData;
                        if (success)
                        {
                            await DisplayAlert("Email sent", "Check your inbox, reset your password and try to sign in with your new password", "Ok");
                        }
                        else
                        {
                            await DisplayAlert(":(","The email address is not associated with any profile", "Ok");
                        }
                    }                
                    else { await DisplayAlert("Ooops...", $"{resp.ErrorMessage}", "Ok"); }
                }
                catch (Exception ex) { await DisplayAlert("Ooops...", $"{ex.Message}", "Ok"); }

                Loading = false;
            }
        }
    }
}
