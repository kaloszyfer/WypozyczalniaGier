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
    class ProductsTableOperator
    {
        /// <summary>
        /// Metoda dodająca rekord (IdProduct = -1) lub aktualizująca wskazany rekord (IdProduct = id wskazanego rekordu) do bazy danych
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdProduct"></param>
        /// <param name="IdType"></param>
        /// <param name="IdPlatform"></param>
        /// <param name="Name"></param>
        /// <param name="Subname"></param>
        /// <param name="Buy_Price"></param>
        /// <param name="Sell_Price"></param>
        /// <param name="Rent_Price"></param>
        /// <param name="Date_Added"></param>
        /// <param name="Last_Update"></param>
        /// <returns></returns>
        public static bool AddOrUpdate(Database.Client dbClient,
            int IdProduct, int IdType, int IdPlatform, string Name, string Subname, int Buy_Price, int Sell_Price, int Rent_Price, DateTime Date_Added, DateTime Last_Update)
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
                MySqlCommand mySqlCmd = new MySqlCommand("ProductsAddOrUpdate", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdProduct", IdProduct);
                mySqlCmd.Parameters.AddWithValue("_IdType", IdType);
                mySqlCmd.Parameters.AddWithValue("_IdPlatform", IdPlatform);
                mySqlCmd.Parameters.AddWithValue("_Name", Name);
                mySqlCmd.Parameters.AddWithValue("_Subname", Subname);
                mySqlCmd.Parameters.AddWithValue("_Buy_Price", Buy_Price);
                mySqlCmd.Parameters.AddWithValue("_Sell_Price", Sell_Price);
                mySqlCmd.Parameters.AddWithValue("_Rent_Price", Rent_Price);
                mySqlCmd.Parameters.AddWithValue("_Date_Added", Date_Added);
                mySqlCmd.Parameters.AddWithValue("_Last_Update", Last_Update);
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
        /// Metoda pobierająca z bazy danych MySQL pojedynczy rekord o podanym identyfikatorze (IdProduct) i przypisująca go do podanej strukturze tabeli (tableProductsItem).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdProduct"></param>
        /// <param name="tableProductsItem"></param>
        /// <returns></returns>
        public static bool Get(Database.Client dbClient, int IdProduct, Database.Structure.Tables.Products tableProductsItem)
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
                MySqlCommand mySqlCmd = new MySqlCommand("ProductsGet", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdProduct", IdProduct);
                mySqlCmd.ExecuteNonQuery();
                MySqlDataReader reader = mySqlCmd.ExecuteReader();
                if (reader.Read())
                {
                    int i = 0;
                    tableProductsItem.Idproduct = (int)reader[i++];
                    tableProductsItem.Idtype = (int)reader[i++];
                    tableProductsItem.Idplatform = (int)reader[i++];
                    tableProductsItem.Name = reader[i++].ToString();
                    tableProductsItem.Subname = reader[i++].ToString();
                    tableProductsItem.Buy_price = OperationHelper.PrepareInt32Value(reader[i++].ToString());
                    tableProductsItem.Sell_price = OperationHelper.PrepareInt32Value(reader[i++].ToString());
                    tableProductsItem.Rent_price = OperationHelper.PrepareInt32Value(reader[i++].ToString());
                    tableProductsItem.Date_added = OperationHelper.PrepareDateTimeValue(reader[i++].ToString());
                    tableProductsItem.Last_update = OperationHelper.PrepareDateTimeValue(reader[i].ToString());
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
        /// Metoda pobierająca z bazy danych MySQL wszystkie rekordy z tabeli i przypisująca je do podanej tabeli (tableProducts).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="tableProducts"></param>
        /// <returns></returns>
        public static bool GetAll(Database.Client dbClient, List<Database.Structure.Tables.Products> tableProducts)
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
                MySqlDataAdapter mySqlDa = new MySqlDataAdapter("ProductsGetAll", dbClient.Connection());
                mySqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                System.Data.DataTable dtProducts = new System.Data.DataTable();
                mySqlDa.Fill(dtProducts);
                tableProducts.Clear();
                for (int i = 0; i < dtProducts.Rows.Count; i++)
                {
                    Database.Structure.Tables.Products tableItem = new Database.Structure.Tables.Products();
                    int j = 0;
                    tableItem.Idproduct = (int)dtProducts.Rows[i].ItemArray[j++];
                    tableItem.Idtype = (int)dtProducts.Rows[i].ItemArray[j++];
                    tableItem.Idplatform = (int)dtProducts.Rows[i].ItemArray[j++];
                    tableItem.Name = dtProducts.Rows[i].ItemArray[j++].ToString();
                    tableItem.Subname = dtProducts.Rows[i].ItemArray[j++].ToString();
                    tableItem.Buy_price = OperationHelper.PrepareInt32Value(dtProducts.Rows[i].ItemArray[j++].ToString());
                    tableItem.Sell_price = OperationHelper.PrepareInt32Value(dtProducts.Rows[i].ItemArray[j++].ToString());
                    tableItem.Rent_price = OperationHelper.PrepareInt32Value(dtProducts.Rows[i].ItemArray[j++].ToString());
                    tableItem.Date_added = OperationHelper.PrepareDateTimeValue(dtProducts.Rows[i].ItemArray[j++].ToString());
                    tableItem.Last_update = OperationHelper.PrepareDateTimeValue(dtProducts.Rows[i].ItemArray[j].ToString());
                    tableProducts.Add(tableItem);
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
        /// Metoda usuwająca z bazy danych MySQL rekord o podanym identyfikatorze (IdProduct).
        /// </summary>
        /// <param name="dbClient"></param>
        /// <param name="IdProduct"></param>
        /// <returns></returns>
        public static bool Remove(Database.Client dbClient, int IdProduct)
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
                MySqlCommand mySqlCmd = new MySqlCommand("ProductsRemove", dbClient.Connection());
                mySqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_IdProduct", IdProduct);
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