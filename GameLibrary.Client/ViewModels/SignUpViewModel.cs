using GameLibrary.Client.Services;
using GameLibrary.Shared.Models;
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
        public bool Loading { get; set; }
        public User NewUser { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;

        public ICommand OnCreateProfileClicked { get { return new Command(() => OnCreateProfile()); } }
        public ICommand OnBackButtonPressed { get { return new Command(() => BackButtonPressed()); } }
        public ICommand OnLoginClicked { get { return new Command(() => LoginClicked()); } }

        public SignUpViewModel(IDataService dataService)
        {
            DataService = dataService;
        }

        public async void OnCreateProfile()
        {
            ErrorMessage = string.Empty;

            NewUser.UserName.Trim();
            if (NewUser.UserName.Length < 2 || NewUser.UserName.Length > 30)
            {
                ErrorMessage = "Username must be 2 - 30 characters";
                RaisePropertyChanged(nameof(ErrorMessage));
                return;
            }

            var passwordCheck = NewUser.Password.Length >= 8 &&
                                NewUser.Password.Count(c => char.IsDigit(c)) >= 1 &&
                                NewUser.Password.Count(c => char.IsLetter(c)) >= 1;

            if (!passwordCheck)
            {
                ErrorMessage = "Password must be at least 8 characters. One letter, one digit.";
                RaisePropertyChanged(nameof(ErrorMessage));
                return;
            }

            if (String.IsNullOrEmpty(NewUser.Region))
            {
                ErrorMessage = "Select a region.";
                RaisePropertyChanged(nameof(ErrorMessage));
                return;
            }

            bool isUnique = await CheckUniqueUserName();
            if (!isUnique)
            {
                ErrorMessage = "This user name is already taken, Choose a different one.";
                RaisePropertyChanged(nameof(ErrorMessage));
                return;
            }
            else
            {
                await CreateUser();
            }
        }

        public async Task<bool> CheckUniqueUserName()
        {
            Loading = true;
            RaisePropertyChanged(nameof(Loading));
            ErrorMessage = String.Empty;

            try
            {
                var resp = await DataService.CheckUserNameUniqueness(NewUser.UserName);
                if (resp.IsSuccess)
                {
                    Loading = false;
                    return true;
                }
                else
                {
                    ErrorMessage = resp.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            Loading = false;
            RaisePropertyChanged(nameof(Loading));
            RaisePropertyChanged(nameof(ErrorMessage));
            return false;
        }

        public async Task CreateUser()
        {
            Loading = true;
            RaisePropertyChanged(nameof(Loading));
            ErrorMessage = String.Empty;

            try
            {
                var resp = await DataService.CreateUser(NewUser);
                if (resp.IsSuccess)
                {

                }
                else
                {
                    ErrorMessage = resp.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            Loading = false;
            RaisePropertyChanged(nameof(Loading));
            RaisePropertyChanged(nameof(ErrorMessage));
        }

        public bool BackButtonPressed()
        {
            return false;
        }

        public async void LoginClicked()
        {
            await Shell.Current.GoToAsync("signin");
        }
    }
}
