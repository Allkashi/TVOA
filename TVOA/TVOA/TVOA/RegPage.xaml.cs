using MySqlConnector;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TVOA
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegPage : ContentPage
    {



        public RegPage()
        {
            InitializeComponent();
        }

        private void OnRegClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewUserLogin.Text) || string.IsNullOrEmpty(NewUserPass1.Text) || string.IsNullOrEmpty(NewUserPass2.Text)
                || string.IsNullOrEmpty(NewUserMail.Text) || string.IsNullOrEmpty(NewUserStreet.Text) || string.IsNullOrEmpty(NewUserHouse.Text))
            {
                DisplayAlert("Ошибка", "Заполните все поля!", "ОK");
            }
            else if (NewUserPass1.Text != NewUserPass2.Text)
                DisplayAlert("Ошибка", "Введенные пароли не совпадают! Проверьте корректность ввода", "ОK");
            else
            {

                DB db = new DB();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @ul", db.getConnection());
                command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = NewUserLogin.Text;

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                    DisplayAlert("Ошибка", "Пользователь с таким логином уже зарегистрирован!", "ОK");
                else
                    Registration(sender, e);
            }
        }

        private void Registration(object sender, EventArgs e)
        {
            DB db = new DB();
            string salt, pass;

            MySqlCommand command = new MySqlCommand("INSERT INTO users ( login, password, salt, mail, address, house) VALUES (@ul, @up, @us, @um, @ua, @uh)", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = NewUserLogin.Text;
            salt = db.Salt();
            pass = db.HashTextPassword(NewUserPass1.Text, salt);
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;
            command.Parameters.Add("@us", MySqlDbType.VarChar).Value = salt;
            command.Parameters.Add("@um", MySqlDbType.VarChar).Value = NewUserMail.Text;
            command.Parameters.Add("@ua", MySqlDbType.VarChar).Value = NewUserStreet.Text;
            command.Parameters.Add("@uh", MySqlDbType.VarChar).Value = NewUserHouse.Text;

            db.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                DisplayAlert("Успех!", "Аккаунт был создан!", "ОK");
                Navigation.PushAsync(new UserPage());
            }
            else
                DisplayAlert("Ошибка", "Что-то пошло не так...", "ОK");
            db.closeConnection();
        }

    }

}