using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Dadata;
using Dadata.Model;
using dotMorten.Xamarin.Forms;

namespace TVOA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalCreateGPage : ContentPage
    {
        public ModalCreateGPage()
        {
            InitializeComponent();
        }

        private CancellationTokenSource cancellationTokenSource;
        private async void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            cancellationTokenSource?.Cancel();

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await Task.Delay(1000, cancellationTokenSource.Token);
                DebounceTimer_Elapsed(sender, args);
            }
            catch (TaskCanceledException)
            {
            }
        }


        private async void DebounceTimer_Elapsed(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var token = "71d407820382d1caa961d5833db3692984d764e4";
            var result = new SuggestAddressRequest(sender.Text)
            {
                locations = new[]
                    {
                     new Address() { kladr_id = "8600001000000" },
                    },
                to_bound = new AddressBound("") { value = "house" }
            };

            var api = new SuggestClientAsync(token);
            var response = await api.SuggestAddress(result);
            var address = response.suggestions[0].data;

            List<string> addresses = new List<string>();

            foreach (var suggestion in response.suggestions)
            {
                addresses.Add(suggestion.value.Replace("Ханты-Мансийский Автономный округ - Югра", "ХМАО-Югра"));

            }

            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                NewGroupeStreet.ItemsSource = addresses;
            }
        }

        private async void OnUndoButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string groupName = NameGroup.Text;
            string groupPass1 = PassGroup1.Text;
            string groupPass2 = PassGroup2.Text;
            string[] groupStreet = NewGroupeStreet.Text.Split(',');
            Connect req = new Connect();
            var status = Preferences.Get("login_key", "");
            string query;

            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(groupPass1) || string.IsNullOrEmpty(groupPass2)
                || string.IsNullOrEmpty(groupStreet[2]) || string.IsNullOrEmpty(groupStreet[3]))
            {
                await DisplayAlert("Ошибка", "Заполните все поля!", "ОK");
            }
            else if (groupPass1 != groupPass2)
                await DisplayAlert("Ошибка", "Введенные пароли не совпадают! Проверьте корректность ввода.", "ОK");
            else if (status != null)
            {
                Groupe groupe = new Groupe { Name = groupName, AddressId = groupStreet[2], HouseId = groupStreet[3]};
                string json = JsonConvert.SerializeObject(groupe);

                query = $"{"new_group"}|{status}|{json}|{groupPass1}";
                var response = req.OpenConnect(query);

                if (response == "OK")
                {
                    await DisplayAlert("Уведомление", "Группа успешно создана!", "ОK");
                    await Navigation.PopModalAsync();
                }
                else if (response == "GROUP_ALREADY_EXIST")
                    await DisplayAlert("Уведомление", "Группа с данным названием уже существует.", "ОK");
                else
                    await DisplayAlert("Уведомление", "Что-то пошло не так, проверьте корректность ввода.", "ОK");

            }
        }
     }
}