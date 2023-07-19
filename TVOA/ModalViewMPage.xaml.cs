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
    public partial class ModalViewMPage : ContentPage
    {
        private Meet MT;
        public ModalViewMPage(Meet item)
        {
            InitializeComponent();
            MT = item;
            if (item.NameMeet == "Test2")
            {
                Vote.IsVisible = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = MT;

            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");
            if (status != null)
            {
                query = $"{"is_admin"}|{status}|{MT.IdGroupe}";
                var response = req.OpenConnect(query);
                if (response == "OK" && MT.Status == "На одобрении")
                {
                    AcceptMeet.IsVisible = true;
                    DenyMeet.IsVisible = true;
                }
                else if (MT.Status == "На одобрении")
                {
                    btnMLike.IsVisible = true;
                }

            }
        }

        private int oneCount = 0;
        private int twoCount = 0;
        private int threeCount = 0;
        private int fourCount = 0;
        private async void VoteButton_Click(object sender, EventArgs e)
        {
            if (OneButton.IsChecked)
            {
                oneCount++;
                OneButton.Content = $"Один - {oneCount}";
            }
            else if (TwoButton.IsChecked)
            {
                twoCount++;
                TwoButton.Content = $"Два - {twoCount}";
            }
            else if (ThreeButton.IsChecked)
            {
                threeCount++;
                ThreeButton.Content = $"Три - {threeCount}";
            }
            else if (FourButton.IsChecked)
            {
                fourCount++;
                FourButton.Content = $"Четыре - {fourCount}";
            }
            OneButton.IsChecked = false;
            TwoButton.IsChecked = false;
            ThreeButton.IsChecked = false;
            FourButton.IsChecked = false;
            VoteButton.IsEnabled = false;
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

            private async void AccMeet(object sender, EventArgs e)
        {
            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");
            if (status != null)
            {
                query = $"{"accept_meet"}|{MT.Id}";
                var response = req.OpenConnect(query);
                if (response == "OK")
                {
                    await DisplayAlert("Уведомление", "Проведение собрания успешно одобрено.", "ОK");
                    await Navigation.PopModalAsync();
                }
            }
        }

        private async void DenMeet(object sender, EventArgs e)
        {
            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");
            if (status != null)
            {
                query = $"{"deny_meet"}|{MT.Id}";
                var response = req.OpenConnect(query);
                if (response == "OK")
                {
                    await DisplayAlert("Уведомление", "Собрание было отклонено.", "ОK");
                    await Navigation.PopModalAsync();
                }
            }
        }

        private void Like(object sender, EventArgs e)
        {
            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");

            if (status != null)
            {
                query = $"{"like_meet"}|{status}|{MT.Id}";
                var response = req.OpenConnect(query);
                if (response == "OK")
                {
                    btnMLike.IsEnabled = false;
                    btnMLike.Source = "likeRed.png";
                }
            }
        }
    }
}