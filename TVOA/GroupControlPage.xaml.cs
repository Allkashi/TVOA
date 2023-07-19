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
    public partial class GroupControlPage : ContentPage
    {

        private Groupe GP;

        public GroupControlPage(Groupe group)
        {
            InitializeComponent();
            GP = group;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = GP;

            Connect req = new Connect();
            string query;

            var status = Preferences.Get("login_key", "");
            if (status != null)
            {
                query = $"{"is_admin"}|{status}|{GP.Id}|{GP.Name}";
                var response = req.OpenConnect(query);
                if (response == "OK")
                {
                    btnGEdit.IsVisible = true;
                    ShowUser(response);
                    //Допилить отображение кнопки выгнать у админа
                }
                else
                    ShowUser("NO");

            }

        }

        private void ShowUser(string rsp)
        {
            Connect req = new Connect();
            string query;
            var status = Preferences.Get("login_key", "");

            query = $"{"user_list"}|{GP.Id}";
            var response = req.OpenConnect(query);
            var parts = response.Split('|');

            if (parts[0] != "NO_USER")
            {
                List<User> user = JsonConvert.DeserializeObject<List<User>>(parts[1]);
                if (rsp == "OK")
                {
                    for (int i = 0; i < user.Count; i++)
                        if (user[i].Name != status)
                            user[i].Status = true;
                }
                UserListView.ItemsSource = user;
            }

        }

        private async void BtnGEdit(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new GroupeEditPage());

        }

        private async void BtnDel_User(object sender, System.EventArgs e)
        {
            Connect req = new Connect();
            string query;

            var label = sender as Label;
            var selectedItem = label.BindingContext as User;
            var userId = selectedItem.Id.ToString();

            query = $"{"del_user"}|{GP.Id}|{userId}";
            var response = req.OpenConnect(query);

            if (response == "OK")
            {
                {
                    OnAppearing();
                    await DisplayAlert("Уведомление", "Пользователь исключен из группы.", "ОK");
                }
            }
            else
                await DisplayAlert("Уведомление", "Не удалось исключить пользователя.", "ОK");
        }
    }
}