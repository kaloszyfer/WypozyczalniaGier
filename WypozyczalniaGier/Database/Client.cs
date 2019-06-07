using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//mysql
using MySql.Data.MySqlClient;
//debug
using System.Diagnostics;

namespace WypozyczalniaGier.Database
{
    /// <summary>
    /// Klasa klienta bazy danych MySQL umożliwiająca połączenie z bazą.
    /// </summary>
    class Client
    {
        // ——————————————————————————————————————————————————————————————————————————————————— //
        // prywatne zmienne
        private string _server;
        private string _userId;
        private string _password;
        // nazwa bazy danych = wypozyczalnia_gier
        private string _connectionString;
        private MySqlConnection _connection;
        // ——————————————————————————————————————————————————————————————————————————————————— //
        // konstruktor
        public Client()
        {
            _Init();
        }
        // ——————————————————————————————————————————————————————————————————————————————————— //
        // metody publiczne
        /// <summary>
        /// Adres serwera bazy MySQL, np. localhost
        /// </summary>
        public string Server { get => _server; set => _server = value; }
        /// <summary>
        /// Identyfikator użytkownika bazy MySQL, np. root
        /// </summary>
        public string UserId { get => _userId; set => _userId = value; }
        /// <summary>
        /// Hasło użytkownika bazy MySQL
        /// </summary>
        public string Password { get => _password; set => _password = value; }
        /// <summary>
        /// Ustawia obiekt w stan gotowości do nawiązania połączenia z bazą MySQL o podanych parametrach
        /// </summary>
        /// <param name="server"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public void SetUp(string server, string userId, string password)
        {
            _server = server;
            _userId = userId;
            _password = password;
            SetUp();
        }
        /// <summary>
        /// Ustawia obiekt w stan gotowości do nawiązania połączenia z bazą MySQL
        /// </summary>
        public void SetUp()
        {
            _connectionString = @"Server=" + _server + ";Database=wypozyczalnia_gier;Uid=" + _userId + ";Pwd=" + _password + ";";
            _connection = new MySqlConnection(_connectionString);
        }
        /// <summary>
        /// Zwraca odwołanie do połączenia z bazą danych MySQL
        /// </summary>
        /// <returns></returns>
        public MySqlConnection Connection()
        {
            return _connection;
        }
        /// <summary>
        /// Nawiązuje połączenie z bazą danych
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection()
        {
            return _OpenConnection();
        }
        /// <summary>
        /// Zamyka połączenie z bazą danych
        /// </summary>
        /// <returns></returns>
        public bool CloseConnection()
        {
            return _CloseConnection();
        }
        // ——————————————————————————————————————————————————————————————————————————————————— //
        // metody prywatne
        private void _Init()
        {
            _server = "localhost";
            _userId = "root";
            _password = "zespolowy1";
            _connection = null;
        }
        private bool _OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            //bool openResult = false; // komentarz na końcu metody
            try
            {
                _connection.Open();
                //openResult = true;
                return true/*openResult*/;
            }
            catch (MySqlException ex)
            {
                string msgContent;
                switch (ex.Number)
                {
                    case 0:
                        msgContent = "Nie można połączyć się z serwerem. Skontaktuj się z administratorem.";
                        Trace.WriteLine(msgContent); //MessageBox.Show(msgContent);
                        break;

                    case 1045:
                        msgContent = "Nieprawidłowy użytkownik i/lub hasło.";
                        Trace.WriteLine(msgContent); //MessageBox.Show(msgContent);
                        break;
                }
                //openResult = false;
                return false/*openResult*/;
            }
            /*finally   // sprawdzenie, czy jest już połączenie do bazy było na początku metody, a więc w razie
            {           // niepowodzenia (openResult=false) powinno być już zamknięte -> blok poniżej można zignorować
                if (!openResult)
                {
                    _CloseConnection(); //connection.Close();
                }
            }*/
        }
        private bool _CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine(ex.Message); //MessageBox.Show(ex.Message);
                return false;
            }
        }
        // ——————————————————————————————————————————————————————————————————————————————————— //
    }
}