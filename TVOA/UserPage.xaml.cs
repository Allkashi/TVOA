using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PlannedMeet();
            PastMeet();
        }

        private async void GoToMeet(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            var selectedItem = viewCell.BindingContext as Meet;
            await Navigation.PushModalAsync(new ModalViewMPage(selectedItem));
        }

        private void PlannedMeet()
        {
            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");

            if (status != null)
            {
                query = $"{"planned_meet"}|{status}";
                var response = req.OpenConnect(query);
                var parts = response.Split('|');

                if (parts[0] == "OK")
                {
                    List<Meet> meet = JsonConvert.DeserializeObject<List<Meet>>(parts[1]);
                    PlannedListView.ItemsSource = meet;
                }
            }
        }

        private void PastMeet()
        {
            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");

            if (status != null)
            {
                query = $"{"past_meet"}|{status}";
                var response = req.OpenConnect(query);
                var parts = response.Split('|');

                if (parts[0] == "OK")
                {
                    List<Meet> meet = JsonConvert.DeserializeObject<List<Meet>>(parts[1]);
                    PastListView.ItemsSource = meet;
                }
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            Date.SelectedIndex = -1;
            Address.SelectedIndex = -1;
            SearchMeet.Text = "";
        }
        private void btnToArchive(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MeetCreatePage());
        }
        private void btnToMeet(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MeetCreatePage());
        }
        private void btnProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }
        private void btnGroupe_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GroupPage());
        }
    }
}