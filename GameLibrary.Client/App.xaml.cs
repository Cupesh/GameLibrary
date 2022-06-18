using GameLibrary.Client.Pages;

namespace GameLibrary.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("signup", typeof(SignUpPage));

            MainPage = new AppShell();
        }
    }
}