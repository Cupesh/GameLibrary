using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GameLibrary.Client.Services;

namespace GameLibrary.Client.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        int count = 0;
        public string BtnText { get; set; }

        public MainPageViewModel(IDataService dataService)
        {
            DataService = dataService;
        }
    }
}