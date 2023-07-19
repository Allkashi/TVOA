using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace TVOA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetCreatePage : ContentPage 
    {
        public MeetCreatePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PublishMeet();
            VoteMeet();
        }

        private void PublishMeet()
        {
            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");

            if (status != null)
            {
                query = $"{"publish_meet"}|{status}";
                var response = req.OpenConnect(query);
                var parts = response.Split('|');

                if (parts[0] == "OK")
                {
                    List<Meet> meet = JsonConvert.DeserializeObject<List<Meet>>(parts[1]);
                    PublishListView.ItemsSource = meet;
                }
            }

        }

        private void VoteMeet()
        {
            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");

            if (status != null)
            {
                query = $"{"vote_meet"}|{status}";
                var response = req.OpenConnect(query);
                var parts = response.Split('|');

                if (parts[0] == "OK")
                {
                    List<Meet> meet = JsonConvert.DeserializeObject<List<Meet>>(parts[1]);
                    VoteListView.ItemsSource = meet;
                }
            }

        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            Address.SelectedIndex = -1;
        }

        private async void GoToMeet(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            var selectedItem = viewCell.BindingContext as Meet;
            await Navigation.PushModalAsync(new ModalViewMPage(selectedItem));
        }

        private async void AddMeet(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModalCreateMPage());
        }

        private void OnMinusClicked(object sender, EventArgs e)
        {
            var label = sender as Label;
            var selectedItem = label.BindingContext as Groupe;
            var groupId = selectedItem.Id.ToString();
        }

        private void OnPlusClicked(object sender, EventArgs e)
        {
            var label = sender as Label;
            var selectedItem = label.BindingContext as Groupe;
            var groupId = selectedItem.Id.ToString();
        }
    }
}