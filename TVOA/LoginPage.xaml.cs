using MySqlConnector;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string userLogin = UserLogin.Text;
            string userPass = UserPass.Text;
            string hashPass, salt;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = userLogin;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                salt = table.Rows[0]["salt"].ToString();
                hashPass = table.Rows[0]["password"].ToString();
                if (db.HashTextPassword(userPass, salt) == hashPass)
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