using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace TVOA
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var status = Preferences.Get("login_key", "");

            if (string.IsNullOrEmpty(status))
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new NavigationPage(new UserPage());
            //            MainPage = new MenuPage();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
