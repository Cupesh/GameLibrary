using GameLibrary.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameLibrary.Client.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public string EntryUserName { get; set; }
        public string PickerRegion { get; set; }
        public string LabelUserNameResult { get; set; }
        public string LabelRegionResult { get; set; }

        public ICommand OnConfirmClicked { get { return new Command(() => OnConfirm()); } }

        public SignUpViewModel(IDataService dataService)
        {
            DataService = dataService;
        }

        public void OnConfirm()
        {
            LabelUserNameResult = EntryUserName;
            LabelRegionResult = PickerRegion;

            RaisePropertyChanged(nameof(LabelUserNameResult));
            RaisePropertyChanged(nameof(LabelRegionResult));
        }
    }
}
