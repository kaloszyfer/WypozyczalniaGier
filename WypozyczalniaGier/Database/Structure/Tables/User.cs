using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    class User
    {
        private string _tableName = "user";
        public string TableName { get => _tableName; }

        private int _iduser = -1;
        private string _name;
        private string _surname;
        private string _username;
        private string _email;
        private string _password;
        private int _idrole = -1;
        private DateTime? _create_time;

        public int Iduser { get => _iduser; set => _iduser = value; }
        public string Name { get => _name; set => _name = value; } // może być null
        public string Surname { get => _surname; set => _surname = value; } // może być null
        public string Username { get => _username; set => _username = value; }
        public string Email { get => _email; set => _email = value; } // może być null
        public string Password { get => _password; set => _password = value; }
        public int Idrole { get => _idrole; set => _idrole = value; }
        /// <summary>
        /// Uwaga! Metoda może zwracać null!
        /// </summary>
        public DateTime? Create_time { get => _create_time; set => _create_time = value; } // może być null
    }
}