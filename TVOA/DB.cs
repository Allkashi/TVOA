using MySqlConnector;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;



namespace TVOA
{
    [Serializable]
    public class Groupe
    {
        public uint Id { get; set; }

        public string Name { get; set; }

        public string AddressId { get; set; }

        public string HouseId { get; set; }

    }

    [Serializable]
    public class User
    {
        public uint Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public User()
        {
            Status = false;
        }
    }

    [Serializable]
    public class Meet
    {
        public uint Id { get; set; }

        public uint IdGroupe { get; set; }

        public string NameMeet { get; set; }

        public string Address { get; set; }
        public string Text { get; set; }

        public string DateStart { get; set; }

        public string Status { get; set; }

        public string Summary { get; set; }

        public int Counter { get; set; }
    } 
}
