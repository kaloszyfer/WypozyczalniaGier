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
    class InventoryTableOperator
    {
        /// <summary>
        /// Metoda dodająca rekord (IdInventory = -1) lub aktualizująca wskazany rekord (IdInventory = id wskazanego rekordu) do bazy danych
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdInventory"></param>
        /// <param name="IdProduct"></param>
        /// <param name="Qty"></param>
        /// <param name="Last_Activity"></param>
        /// <returns></returns>
        public static bool AddOrUpdate(Database.Client dbClient,
            int IdInventory, int IdProduct, int Qty, DateTime Last_Activity)
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
                MySqlCommand mySqlCmd = new MySqlCommand("InventoryAddOrUpdate", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdInventory", IdInventory);
                mySqlCmd.Parameters.AddWithValue("_IdProduct", IdProduct);
                mySqlCmd.Parameters.AddWithValue("_Qty", Qty);
                mySqlCmd.Parameters.AddWithValue("_Last_Activity", Last_Activity);
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
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdInventory) i przypisująca go do podanej strukturze tabeli (tableInventoryItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdInventory"></param>
        /// <param name="tableInventoryItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdInventory, Database.Structure.Tables.Inventory tableInventoryItem)
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
                MySqlCommand mySqlCmd = new MySqlCommand("InventoryGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdInventory", IdInventory);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tableInventoryItem.Idinventory = (int)reader[i++];
                    tableInventoryItem.Idproduct = (int)reader[i++];
                    tableInventoryItem.Qty = (int)reader[i++];
                    tableInventoryItem.Last_activity = OperationHelper.PrepareDateTimeValue(reader[i].ToString());
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
        /// Metoda pobierająca z bazy danych MySQL wszystkie rekordy z tabeli i przypisująca je do podanej tabeli (tableInventory).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="tableInventory"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, List<Database.Structure.Tables.Inventory> tableInventory)
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
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("InventoryGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtInventory = new System.Data.DataTable();
                mySqlDa.Fill(dtInventory);
                tableInventory.Clear();
                for (int i = 0; i < dtInventory.Rows.Count; i++)
                {
                    Database.Structure.Tables.Inventory tableItem = new Database.Structure.Tables.Inventory();
                    int j = 0;
                    tableItem.Idinventory = (int)dtInventory.Rows[i].ItemArray[j++];
                    tableItem.Idproduct = (int)dtInventory.Rows[i].ItemArray[j++];
                    tableItem.Qty = (int)dtInventory.Rows[i].ItemArray[j++];
                    tableItem.Qty = (int)dtInventory.Rows[i].ItemArray[j++];
                    tableItem.Last_activity = OperationHelper.PrepareDateTimeValue(dtInventory.Rows[i].ItemArray[j].ToString());
                    tableInventory.Add(tableItem);
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
        /// Metoda usuwająca z bazy danych MySQL rekord o podanym identyfikatorze (IdInventory).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdInventory"></param>
        /// <returns></returns>
        public static bool Remove(Database.Client dbClient, int IdInventory)
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
                MySqlCommand mySqlCmd = new MySqlCommand("InventoryRemove", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdInventory", IdInventory);
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