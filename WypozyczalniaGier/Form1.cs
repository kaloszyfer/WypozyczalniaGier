using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WypozyczalniaGier.Data;
using WypozyczalniaGier.Database;
//debug
using System.Diagnostics;

namespace WypozyczalniaGier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Obiekt klasy magazynującej, w którym przechowywane są dane z bazy MySQL
        /// </summary>
        private Data.Storage _dataStore = new Data.Storage();
        /// <summary>
        /// Obiekt klasy klienta bazy danych MySQL
        /// </summary>
        private Database.Client _dbClient = new Database.Client();

        private void Form1_Load(object sender, EventArgs e)
        {
            _PrepareDbClient();
            _ReadTablesDataFromDatabase();
            _dbClient.CloseConnection(); // po zakończeniu zczytywania zamknięcie połączenia
        }
        private void _PrepareDbClient()
        {
            // poniższe wartości można docelowo umieścić w zewnętrznym pliku konfiguracyjnym
            //_dbClient.Server = "localhost"; // 
            //_dbClient.UserId = "root";
            //_dbClient.Password = "zespolowy1"; // to hasło przypisywałem podczas instalacji MySql (jakby co można je później zmienić poprzez kwerendę)
            // nazwa bazy jest wkodowana w klasie Client: wypozyczalnia_gier
            //_dbClient.SetUp();
            //lub w jednej linii:
            _dbClient.SetUp("localhost", "root", "zespolowy1");
            //lub samo _dbClient.SetUp(); , ponieważ wartości "localhost", "root", "zespolowy1" są wartościami domyślnymi
        }
        private void _ReadTablesDataFromDatabase()
        {
            //użycie metod odczytujących z db table operatorów, odczyt z bazy i zapis do zmiennych struktury

            //DateTime timeNow = DateTime.Now;
            //Database.Operators.Tables.ActionsTableOperator.AddOrUpdate(_dbClient,-1,-1,-1,-1,-1,-1,DateTime.Now,-1,DateTime.Now,""); // chyba działa, ale występują klucze..

            Trace.WriteLine($"TEST: before data read count = {_dataStore.TableActions.Count}"); //output: "TEST: before data read count = 0"
            Database.Operators.Tables.ActionsTableOperator.GetAll(_dbClient, /*ref*/ _dataStore.TableActions);
            Trace.WriteLine($"TEST: after data read count = {_dataStore.TableActions.Count}"); //output: "TEST: after data read count = 50"

            var tabelkaTest = new Database.Structure.Tables.Actions();
            Database.Operators.Tables.ActionsTableOperator.Get(_dbClient, 25, tabelkaTest);
            Trace.WriteLine($"TESTTTT: read record = {tabelkaTest.Idactions},{tabelkaTest.Idtype_of_event},{tabelkaTest.Idproduct},{tabelkaTest.Operator},{tabelkaTest.Client},{tabelkaTest.Quantity},{tabelkaTest.Suggest_date},{tabelkaTest.Idactionstart},{tabelkaTest.Date_added},{tabelkaTest.Actionscol},"); //output: "TESTTTT: read record = 25,3,5,2,9,1,27.05.2019 00:00:00,0,21.05.2019 20:41:47,,"

            var tabelkaTest2 = new Database.Structure.Tables.Actions();
            Database.Operators.Tables.ActionsTableOperator.Get(_dbClient, 75, tabelkaTest2);
            Trace.WriteLine($"TESTTTT: read record = {tabelkaTest2.Idactions},{tabelkaTest2.Idtype_of_event},{tabelkaTest2.Idproduct},{tabelkaTest2.Operator},{tabelkaTest2.Client},{tabelkaTest2.Quantity},{tabelkaTest2.Suggest_date},{tabelkaTest2.Idactionstart},{tabelkaTest2.Date_added},{tabelkaTest2.Actionscol},"); //output: "TESTTTT: read record = 75,3,9,5,12,1,01.06.2019 00:00:00,0,21.05.2019 21:01:23,,"

            //Database.Operators.Tables.ActionsTableOperator.Remove(_dbClient, 75);

            Database.Operators.Tables.InventoryTableOperator.AddOrUpdate(_dbClient,-1,-1,-1,DateTime.Now);
        }
    }
}