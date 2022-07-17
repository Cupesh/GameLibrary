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
            ErrorMessage = String.Empty;

            try
            {
                var resp = await DataService.Login(User);
                if (resp.IsSuccess)
                {
                    OnSuccessfulLogin(resp.ApiData);
                    var toast = Toast.Make("Success!");
                    await toast.Show();
                }
                else
                {
                    var toast = Toast.Make("Invalid username or password");
                    await toast.Show();
                }
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long);
                await toast.Show();
            }

            Loading = false;
        }

        private async void OnSuccessfulLogin(User user)
        {
            Preferences.Set("UserId", user.UserId);
            Preferences.Set("Username", user.UserName);
            Preferences.Set("Region", user.Region);

            await Shell.Current.GoToAsync("../..");
        }
    }
}
