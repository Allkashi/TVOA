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
    public partial class GroupPage : ContentPage
    {
  
        private async void btnJoin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModalJoinGPage());
        }
        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ModalCreateGPage());
        }

        public GroupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnStart();
        }

        private async void GoToGroup(object sender, EventArgs e)
        {
            var viewCell = sender as ViewCell;
            var selectedItem = viewCell.BindingContext as Groupe;
            await Navigation.PushAsync(new GroupControlPage(selectedItem));
        }

        private async void btnExit_Group(object sender, EventArgs e)
        {
            Connect req = new Connect();
            string query;
            var status = Preferences.Get("login_key", "");
            var label = sender as Label;
            var selectedItem = label.BindingContext as Groupe;
            var groupId = selectedItem.Id.ToString();

//            DisplayAlert("Уведомление", groupId, "ОK");

            if (status != null)
            {
                query = $"{"exit_group"}|{status}|{groupId}";
                var response = req.OpenConnect(query);

                if (response != "ERROR")
                {
                    OnAppearing();
                    await DisplayAlert("Уведомление", "Вы успешно покинули группу", "ОK");
                }
                else
                    await DisplayAlert("Уведомление", "Произошла ошибка, попробуйте позже", "ОK");
            }

        }

        public void OnStart()
        {
            Connect req = new Connect();
            string query;
            var status = Preferences.Get("login_key", "");

            if (status != null)
            {
                query = $"{"show_group"}|{status}";
                var response = req.OpenConnect(query);

                if (response != "NO_USER")
                    if (response != "NO_GROUPES")
                    {
                        List<Groupe> groupe = JsonConvert.DeserializeObject<List<Groupe>>(response);

                        if (groupe[0].Name == "OK")
                        {
                            groupe.RemoveAt(0);
                            ResultListView.ItemsSource = groupe;
                        }
                    }
            }
        }

    }
}
