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
    public partial class ModalJoinGPage : ContentPage
    {
        public ModalJoinGPage()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            Connect req = new Connect();
            var status = Preferences.Get("login_key", "");
            string nameGroup = NameGroup.Text;
            string passGroup = PassGroup.Text;
            string query;
            

            query = $"{"join_group"}|{status}|{nameGroup}|{passGroup}";
            var response = req.OpenConnect(query);
            var parts = response.Split('|');

            if (parts[0] == "OK")
            {
                await DisplayAlert("Уведомление", "Вы успешно вступили в группу!", "ОK");
                await Navigation.PopModalAsync();

            }
            else if (parts[0] == "ALREADY_ON_GROUPE")
            {
                await DisplayAlert("Уведомление", "Вы уже состоите в данной группе.", "ОK");
            }
            else
                await DisplayAlert("Уведомление", "Группы с данным названием и паролем не найдено. Проверьте корректность введенных данных.", "ОK");
        }

        private async void OnUndoButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}