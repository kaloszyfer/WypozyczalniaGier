using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//mysql
using MySql.Data.MySqlClient;
//debug
using System.Diagnostics;
//using System.Globalization;

namespace WypozyczalniaGier.Database.Operators.Tables
{
    /// <summary>
    /// Klasa służąca do przeprowadzania operacji na tabeli
    /// </summary>
    class UserTableOperator
    {
        /// <summary>
        /// Metoda dodająca rekord (IdUser = -1) lub aktualizująca wskazany rekord (IdUser = id wskazanego rekordu) do bazy danych
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdUser"></param>
        /// <param name="Name"></param>
        /// <param name="Surname"></param>
        /// <param name="UserName"></param>
        /// <param name="EMail"></param>
        /// <param name="Password"></param>
        /// <param name="IdRole"></param>
        /// <param name="Create_Time"></param>
        /// <returns></returns>
        public static bool AddOrUpdate(Database.Client dbClient,
            int IdUser, string Name, string Surname, string UserName, string EMail, string Password, int IdRole, DateTime Create_Time)
        {
            if (dbClient == null || dbClient.Connection() == null)
            {
                return false;
            }
            if (!dbClient.OpenConnection()) // próba nawiązania połączenia z bazą
            {
                return false;
            }
            try
            {
                MySqlCommand mySqlCmd = new MySqlCommand("UserAddOrUpdate", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdUser", IdUser);
                mySqlCmd.Parameters.AddWithValue("_Name", Name);
                mySqlCmd.Parameters.AddWithValue("_Surname", Surname);
                mySqlCmd.Parameters.AddWithValue("_UserName", UserName);
                mySqlCmd.Parameters.AddWithValue("_EMail", EMail);
                mySqlCmd.Parameters.AddWithValue("_Password", Password);
                mySqlCmd.Parameters.AddWithValue("_IdRole", IdRole);
                mySqlCmd.Parameters.AddWithValue("_Create_Time", Create_Time);
                mySqlCmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
            // pamiętać, aby zamykać połączenie
        }
        /// <summary>
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdUser) i przypisująca go do podanej strukturze tabeli (tableUserItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdUser"></param>
        /// <param name="tableUserItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdUser, Database.Structure.Tables.User tableUserItem)
        {
            if (dbClient == null || dbClient.Connection() == null)
            {
                return false;
            }
            if (!dbClient.OpenConnection()) // próba nawiązania połączenia z bazą
            {
                return false;
            }
            try
            {
                MySqlCommand mySqlCmd = new MySqlCommand("UserGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdUser", IdUser);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tableUserItem.Iduser = (int)reader[i++];
                    tableUserItem.Name = reader[i++].ToString();
                    tableUserItem.Surname = reader[i++].ToString();
                    tableUserItem.Username = reader[i++].ToString();
                    tableUserItem.Email = reader[i++].ToString();
                    tableUserItem.Password = reader[i++].ToString();
                    tableUserItem.Idrole = (int)reader[i++];
                    tableUserItem.Create_time = OperationHelper.PrepareDateTimeValue(reader[i].ToString());
                    reader.Close();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
            // pamiętać, aby zamykać połączenie
        }
        /// <summary>
        /// Metoda pobierająca z bazy danych MySQL wszystkie rekordy z tabeli i przypisująca je do podanej tabeli (tableUser).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="tableUser"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, List<Database.Structure.Tables.User> tableUser)
        {
            if (dbClient == null || dbClient.Connection() == null)
            {
                return false;
            }
            if (!dbClient.OpenConnection()) // próba nawiązania połączenia z bazą
            {
                return false;
            }
            try
            {
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("UserGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtUser = new System.Data.DataTable();
                mySqlDa.Fill(dtUser);
                tableUser.Clear();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    Database.Structure.Tables.User tableItem = new Database.Structure.Tables.User();
                    int j = 0;
                    tableItem.Iduser = (int)dtUser.Rows[i].ItemArray[j++];
                    tableItem.Name = dtUser.Rows[i].ItemArray[j++].ToString();
                    tableItem.Surname = dtUser.Rows[i].ItemArray[j++].ToString();
                    tableItem.Username = dtUser.Rows[i].ItemArray[j++].ToString();
                    tableItem.Email = dtUser.Rows[i].ItemArray[j++].ToString();
                    tableItem.Password = dtUser.Rows[i].ItemArray[j++].ToString();
                    tableItem.Idrole = (int)dtUser.Rows[i].ItemArray[j++];
                    tableItem.Create_time = OperationHelper.PrepareDateTimeValue(dtUser.Rows[i].ItemArray[j].ToString());
                    tableUser.Add(tableItem);
                }
                return true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
            // pamiętać, aby zamykać połączenie
        }
        /// <summary>
        /// Metoda usuwająca z bazy danych MySQL rekord o podanym identyfikatorze (IdUser).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public static bool Remove(Database.Client dbClient, int IdUser)
        {
            if (dbClient == null || dbClient.Connection() == null)
            {
                return false;
            }
            if (!dbClient.OpenConnection()) // próba nawiązania połączenia z bazą
            {
                return false;
            }
            try
            {
                MySqlCommand mySqlCmd = new MySqlCommand("UserRemove", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdUser", IdUser);
                mySqlCmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
            // pamiętać, aby zamykać połączenie
        }
    }
}