using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace TVOA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPage()); 
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            Navigation.PushAsync(new MainPage()); 
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            Undo.IsVisible = true;
            Save.IsVisible = true;
            UserName.IsEnabled = true;
            UserSurname.IsEnabled = true;
            UserEMail.IsEnabled = true;
            UserAddress.IsEnabled = true;

        }

        private void btnUndo_Clicked(object sender, EventArgs e)
        {
            Undo.IsVisible = false;
            Save.IsVisible = false;
            UserName.IsEnabled = false;
            UserSurname.IsEnabled = false;
            UserEMail.IsEnabled = false;
            UserAddress.IsEnabled = false;

        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Undo.IsVisible = false;
            Save.IsVisible = false;
            UserName.IsEnabled = false;
            UserSurname.IsEnabled = false;
            UserEMail.IsEnabled = false;
            UserAddress.IsEnabled = false;

        }
    }
}