using GameLibrary.Client.Services;
using GameLibrary.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Client.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        public bool Loading { get; set; }
        public User User { get; set; } = new();
        public string ErrorMessage { get; set; } = string.Empty;

        public SignInViewModel(IDataService dataService)
        {
            DataService = dataService;
        }
    }
}
