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
    class Type_Of_EventsTableOperator
    {
        /// <summary>
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdEvent) i przypisująca go do podanej strukturze tabeli (tableType_Of_EventsItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdEvent"></param>
        /// <param name="tableType_Of_EventsItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdEvent, Database.Structure.Tables.Type_Of_Events tableType_Of_EventsItem)
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
                MySqlCommand mySqlCmd = new MySqlCommand("Type_Of_EventsGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdEvent", IdEvent);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tableType_Of_EventsItem.Idevent = (int)reader[i++];
                    tableType_Of_EventsItem.Desc = reader[i].ToString();
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
        /// <param name="tableType_Of_Events"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, List<Database.Structure.Tables.Type_Of_Events> tableType_Of_Events)
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
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("Type_Of_EventsGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtType_Of_Events = new System.Data.DataTable();
                mySqlDa.Fill(dtType_Of_Events);
                tableType_Of_Events.Clear();
                for (int i = 0; i < dtType_Of_Events.Rows.Count; i++)
                {
                    Database.Structure.Tables.Type_Of_Events tableItem = new Database.Structure.Tables.Type_Of_Events();
                    int j = 0;
                    tableItem.Idevent = (int)dtType_Of_Events.Rows[i].ItemArray[j++];
                    tableItem.Desc = dtType_Of_Events.Rows[i].ItemArray[j].ToString();
                    tableType_Of_Events.Add(tableItem);
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