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
    class ActionsTableOperator
    {
        /// <summary>
        /// Metoda dodająca rekord (IdActions = -1) lub aktualizująca wskazany rekord (IdActions = id wskazanego rekordu) do bazy danych
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdActions"></param>
        /// <param name="IdType_Of_Event"></param>
        /// <param name="IdProduct"></param>
        /// <param name="Operator"></param>
        /// <param name="Client"></param>
        /// <param name="Quantity"></param>
        /// <param name="Suggest_Date"></param>
        /// <param name="IdActionStart"></param>
        /// <param name="Date_Added"></param>
        /// <param name="ActionsCol"></param>
        /// <returns></returns>
        public static bool AddOrUpdate(Database.Client dbClient,
            int IdActions, int IdType_Of_Event, int IdProduct, int Operator, int Client, int Quantity, DateTime Suggest_Date, int IdActionStart, DateTime Date_Added, string ActionsCol)
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
                MySqlCommand mySqlCmd = new MySqlCommand("ActionsAddOrUpdate", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdActions", IdActions);
                mySqlCmd.Parameters.AddWithValue("_IdType_Of_Event", IdType_Of_Event);
                mySqlCmd.Parameters.AddWithValue("_IdProduct", IdProduct);
                mySqlCmd.Parameters.AddWithValue("_Operator", Operator);
                mySqlCmd.Parameters.AddWithValue("_Client", Client);
                mySqlCmd.Parameters.AddWithValue("_Quantity", Quantity);
                mySqlCmd.Parameters.AddWithValue("_Suggest_Date", Suggest_Date);
                mySqlCmd.Parameters.AddWithValue("_IdActionStart", IdActionStart);
                mySqlCmd.Parameters.AddWithValue("_Date_Added", Date_Added);
                mySqlCmd.Parameters.AddWithValue("_ActionsCol", ActionsCol);
                mySqlCmd.ExecuteNonQuery();
                return true;
            }
            catch(MySqlException ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
            // pamiętać, aby zamykać połączenie
        }
        /// <summary>
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdActions) i przypisująca go do podanej strukturze tabeli (tableActionsItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdActions"></param>
        /// <param name="tableActionsItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdActions, Database.Structure.Tables.Actions tableActionsItem)
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
                MySqlCommand mySqlCmd = new MySqlCommand("ActionsGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdActions", IdActions);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tableActionsItem.Idactions = (int)reader[i++];
                    tableActionsItem.Idtype_of_event = (int)reader[i++];
                    tableActionsItem.Idproduct = (int)reader[i++];
                    tableActionsItem.Operator = (int)reader[i++];
                    tableActionsItem.Client = (int)reader[i++];
                    tableActionsItem.Quantity = OperationHelper.PrepareInt32Value(reader[i++].ToString());
                    tableActionsItem.Suggest_date = OperationHelper.PrepareDateTimeValue(reader[i++].ToString());
                    tableActionsItem.Idactionstart = OperationHelper.PrepareInt32Value(reader[i++].ToString());
                    tableActionsItem.Date_added = OperationHelper.PrepareDateTimeValue(reader[i++].ToString());
                    tableActionsItem.Actionscol = reader[i].ToString();
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
        /// Metoda pobierająca z bazy danych MySQL wszystkie rekordy z tabeli i przypisująca je do podanej tabeli (tableActions).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="tableActions"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, /*ref*/ List<Database.Structure.Tables.Actions> tableActions)
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
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("ActionsGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtActions = new System.Data.DataTable();
                mySqlDa.Fill(dtActions);
                tableActions.Clear();
                for (int i = 0 ; i < dtActions.Rows.Count ; i++)
                {
                    Database.Structure.Tables.Actions tableItem = new Database.Structure.Tables.Actions();
                    int j = 0;
                    tableItem.Idactions = (int)dtActions.Rows[i].ItemArray[j++];
                    tableItem.Idtype_of_event = (int)dtActions.Rows[i].ItemArray[j++];
                    tableItem.Idproduct = (int)dtActions.Rows[i].ItemArray[j++];
                    tableItem.Operator = (int)dtActions.Rows[i].ItemArray[j++];
                    tableItem.Client = (int)dtActions.Rows[i].ItemArray[j++];
                    tableItem.Quantity = OperationHelper.PrepareInt32Value(dtActions.Rows[i].ItemArray[j++].ToString());
                    tableItem.Suggest_date = OperationHelper.PrepareDateTimeValue(dtActions.Rows[i].ItemArray[j++].ToString());
                    tableItem.Idactionstart = OperationHelper.PrepareInt32Value(dtActions.Rows[i].ItemArray[j++].ToString());
                    tableItem.Date_added = OperationHelper.PrepareDateTimeValue(dtActions.Rows[i].ItemArray[j++].ToString());
                    tableItem.Actionscol = dtActions.Rows[i].ItemArray[j].ToString();
                    tableActions.Add(tableItem);
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
        /// Metoda usuwająca z bazy danych MySQL rekord o podanym identyfikatorze (IdActions).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdActions"></param>
        /// <returns></returns>
        public static bool Remove(Database.Client dbClient, int IdActions)
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
                MySqlCommand mySqlCmd = new MySqlCommand("ActionsRemove", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdActions", IdActions);
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