using GameLibrary.Client.Services;
using Microsoft.Extensions.Options;
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
            GetUserProfile();
        }

        public async Task GetUserProfile()
        {
            var path = Path.Combine(FileSystem.Current.AppDataDirectory, "GameLibraryUserSettings.json");
            File.Create(path);
        }
    }
}
