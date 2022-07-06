using GameLibrary.Client.Pages;

namespace GameLibrary.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
                Shell.Current.CurrentItem = PhoneTabs;

            Routing.RegisterRoute("signup", typeof(SignUpPage));
            Routing.RegisterRoute("signin", typeof(SignInPage));
            Routing.RegisterRoute("mainMenu", typeof(MainMenuPage));
        }
    }
}