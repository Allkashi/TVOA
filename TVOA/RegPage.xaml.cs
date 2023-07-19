using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using dotMorten.Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Dadata;
using Dadata.Model;
using System.Threading;


namespace TVOA
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {


        public RegPage()
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
                NewUserStreet.ItemsSource = addresses;
            }
        }


        private void OnRegClicked(object sender, EventArgs e)
        {
            Connect req = new Connect();
            string userLogin = NewUserLogin.Text;
            string userPass = NewUserPass1.Text;
            string userMail = NewUserMail.Text;
            string[] userStreet = NewUserStreet.Text.Split(',');
            string query;

            if (string.IsNullOrEmpty(userLogin) || string.IsNullOrEmpty(userPass) || string.IsNullOrEmpty(NewUserPass2.Text)
                || string.IsNullOrEmpty(userMail) || string.IsNullOrEmpty(userStreet[2]) || string.IsNullOrEmpty(userStreet[3]))
            {
                DisplayAlert("Ошибка", "Заполните все поля!", "ОK");
            }
            else if (NewUserPass1.Text != NewUserPass2.Text)
                DisplayAlert("Ошибка", "Введенные пароли не совпадают! Проверьте корректность ввода", "ОK");
            else
            {
                query = $"{"get_reg"}|{userLogin}|{userPass}|{userMail}|{userStreet[2]}|{userStreet[3]}";

                var response = req.OpenConnect(query);

                var parts = response.Split('|');

                if (parts[0] != "OK")
                    DisplayAlert("Ошибка", "Пользователь с таким логином уже зарегистрирован!", "ОK");
                else
                {
                    DisplayAlert("Успех!", "Аккаунт был создан!", "ОK");
                    Preferences.Set("login_key", userLogin);
                    Navigation.PushAsync(new UserPage());
                }
            }
        }

    }

}