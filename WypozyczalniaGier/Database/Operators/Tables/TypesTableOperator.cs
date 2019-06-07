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
    class TypesTableOperator
    {
        /// <summary>
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdType) i przypisująca go do podanej strukturze tabeli (tableTypesItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdType"></param>
        /// <param name="tableTypesItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdType, Database.Structure.Tables.Types tableTypesItem)
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
                MySqlCommand mySqlCmd = new MySqlCommand("TypesGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdType", IdType);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tableTypesItem.Idtype = (int)reader[i++];
                    tableTypesItem.Desc = reader[i].ToString();
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
        /// Metoda pobierająca z bazy danych MySQL wszystkie rekordy z tabeli i przypisująca je do podanej tabeli (tableTypes).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="tableTypes"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, List<Database.Structure.Tables.Types> tableTypes)
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
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("TypesGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtType_Of_Events = new System.Data.DataTable();
                mySqlDa.Fill(dtType_Of_Events);
                tableTypes.Clear();
                for (int i = 0; i < dtType_Of_Events.Rows.Count; i++)
                {
                    Database.Structure.Tables.Types tableItem = new Database.Structure.Tables.Types();
                    int j = 0;
                    tableItem.Idtype = (int)dtType_Of_Events.Rows[i].ItemArray[j++];
                    tableItem.Desc = dtType_Of_Events.Rows[i].ItemArray[j].ToString();
                    tableTypes.Add(tableItem);
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