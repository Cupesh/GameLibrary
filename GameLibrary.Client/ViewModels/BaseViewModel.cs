using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Client.Services;

namespace GameLibrary.Client.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public IDataService DataService { get; set; }
        private bool loading;
        public bool Loading
        {
            get => loading;
            set
            {
                loading = value;
                RaisePropertyChanged(nameof(Loading));
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async Task DisplayAlert(string title, string message, string accept)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, accept);
        }

        // returns null if cancel is tapped
        public async Task<string> DisplayPrompt(string title, string message)
        {
            return await Application.Current.MainPage.DisplayPromptAsync(title, message);
        }
    }
}
