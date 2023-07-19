using MySqlConnector;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TVOA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

        }

        private void OnConnectClicked(object sender, EventArgs e)
        {
            Connect req = new Connect();
            string userLogin = UserLogin.Text;
            string userPass = UserPass.Text;
            string query;

            query = $"{"get_auth"}|{userLogin}|{userPass}";

            var response = req.OpenConnect(query);

            var parts = response.Split('|');

            if (parts[0] == "OK")
            {
                Preferences.Set("login_key", userLogin);
                Navigation.PushAsync(new UserPage());
            }
            else
            {
                DisplayAlert("Уведомление", "Такой пользователь не зарегистрирован!", "ОK");
            }    

        }

        private async void OnGoReg(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegPage());
        }
    }
}