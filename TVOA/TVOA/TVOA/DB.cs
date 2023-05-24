using MySqlConnector;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;



namespace TVOA
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server=192.168.1.18;port=3306;username=username;password=password;database=tvoa;sslmode=0;AllowPublicKeyRetrieval=true");

        public void openConnection() //Подключение к бд
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection() //Отключение от бд
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection() // Возврат соединения с бд
        {
            return connection;
        }

        public string Salt()
        {
            const int NUMBER_OF_BYTES = 50;
            byte[] randomBytes = new byte[NUMBER_OF_BYTES];
            char[] chars = new char[NUMBER_OF_BYTES * 2];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                for (int i = 0; i < NUMBER_OF_BYTES; i++)
                {
                    byte b = randomBytes[i];
                    chars[i * 2] = (char)(97 + (b % 26)); // lowercase letter
                    chars[i * 2 + 1] = (char)(48 + (b % 10)); // digit
                }
            }

            string randomNumber = new string(chars);
            return randomNumber;
        }

        public string HashTextPassword(string TextPassword, string salt)
        {
            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(TextPassword);
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

            string pass = Encoding.UTF8.GetString(tmpHash);
            return pass;
        }

    }
}
