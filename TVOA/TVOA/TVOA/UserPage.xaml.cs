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
    public partial class UserPage : ContentPage
    {
        public UserPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            Period.SelectedIndex = -1;
            Address.SelectedIndex = -1;
            TypeMeet.SelectedIndex = -1;
        }
    }
}