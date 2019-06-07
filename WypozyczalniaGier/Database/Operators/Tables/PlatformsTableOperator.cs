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
    class PlatformsTableOperator
    {
        /// <summary>
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdPlatform) i przypisująca go do podanej strukturze tabeli (tablePlatformsItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdPlatform"></param>
        /// <param name="tablePlatformsItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdPlatform, Database.Structure.Tables.Platforms tablePlatformsItem)
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
                MySqlCommand mySqlCmd = new MySqlCommand("PlatformsGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdPlatform", IdPlatform);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tablePlatformsItem.Idplatform = (int)reader[i++];
                    tablePlatformsItem.Desc = reader[i].ToString();
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
        /// Metoda pobierająca z bazy danych MySQL wszystkie rekordy z tabeli i przypisująca je do podanej tabeli (tablePlatforms).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="tablePlatforms"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, List<Database.Structure.Tables.Platforms> tablePlatforms)
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
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("PlatformsGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtPlatforms = new System.Data.DataTable();
                mySqlDa.Fill(dtPlatforms);
                tablePlatforms.Clear();
                for (int i = 0; i < dtPlatforms.Rows.Count; i++)
                {
                    Database.Structure.Tables.Platforms tableItem = new Database.Structure.Tables.Platforms();
                    int j = 0;
                    tableItem.Idplatform = (int)dtPlatforms.Rows[i].ItemArray[j++];
                    tableItem.Desc = dtPlatforms.Rows[i].ItemArray[j].ToString();
                    tablePlatforms.Add(tableItem);
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