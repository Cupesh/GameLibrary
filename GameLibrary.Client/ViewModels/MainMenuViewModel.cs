using GameLibrary.Client.Services;
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
        }
    }
}
