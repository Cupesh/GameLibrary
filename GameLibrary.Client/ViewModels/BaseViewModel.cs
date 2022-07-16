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

        public INavigation Navigation { get; set; }
        public IDataService DataService { get; set; }
    }
}
