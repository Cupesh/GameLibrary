using GameLibrary.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Client.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(IDataService dataService)
        {
            DataService = dataService;
        }
    }
}
