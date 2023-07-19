using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TVOA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupeEditPage : ContentPage
    {
        public GroupeEditPage()
        {
            InitializeComponent();
        }


        private async void btnUndo_Clicked(object sender, EventArgs e)
        {
            Undo.IsVisible = false;
            Save.IsVisible = false;
            UserName.IsEnabled = false;
            UserSurname.IsEnabled = false;
            UserAddress.IsEnabled = false;
            await Navigation.PopModalAsync();

        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            Undo.IsVisible = false;
            Save.IsVisible = false;
            UserName.IsEnabled = false;
            UserSurname.IsEnabled = false;
            UserAddress.IsEnabled = false;
            await Navigation.PopModalAsync();

        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            Undo.IsVisible = true;
            Save.IsVisible = true;
            UserName.IsEnabled = true;
            UserSurname.IsEnabled = true;
            UserAddress.IsEnabled = true;

        }

    }
}