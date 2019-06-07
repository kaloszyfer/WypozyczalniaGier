using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//mysql
using MySql.Data.MySqlClient;
//debug
using System.Diagnostics;

namespace WypozyczalniaGier.Database.Operators.Tables
{
    /// <summary>
    /// Klasa służąca do przeprowadzania operacji na tabeli
    /// </summary>
    class RolesTableOperator
    {
        /// <summary>
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdRole) i przypisująca go do podanej strukturze tabeli (tableRolesItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdRole"></param>
        /// <param name="tableRolesItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdRole, Database.Structure.Tables.Roles tableRolesItem)
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
                MySqlCommand mySqlCmd = new MySqlCommand("RolesGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdRole", IdRole);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tableRolesItem.Idrole = (int)reader[i++];
                    tableRolesItem.Roledesc = reader[i].ToString();
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
        /// Metoda pobierająca z bazy danych MySQL wszystkie rekordy z tabeli i przypisująca je do podanej tabeli (tableRoles).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="tableRoles"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, List<Database.Structure.Tables.Roles> tableRoles)
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
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("RolesGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtRoles = new System.Data.DataTable();
                mySqlDa.Fill(dtRoles);
                tableRoles.Clear();
                for (int i = 0; i < dtRoles.Rows.Count; i++)
                {
                    Database.Structure.Tables.Roles tableItem = new Database.Structure.Tables.Roles();
                    int j = 0;
                    tableItem.Idrole = (int)dtRoles.Rows[i].ItemArray[j++];
                    tableItem.Roledesc = dtRoles.Rows[i].ItemArray[j].ToString();
                    tableRoles.Add(tableItem);
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
    }
}