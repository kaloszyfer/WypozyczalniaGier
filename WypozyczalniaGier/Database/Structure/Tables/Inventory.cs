using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    class Inventory
    {
        private string _tableName = "inventory";
        public string TableName { get => _tableName; }

        private int _idinventory = -1;
        private int _idproduct = -1;
        private int _qty = -1;
        private DateTime? _last_activity;

        public int Idinventory { get => _idinventory; set => _idinventory = value; }
        public int Idproduct { get => _idproduct; set => _idproduct = value; }
        public int Qty { get => _qty; set => _qty = value; }
        /// <summary>
        /// Uwaga! Metoda może zwracać null!
        /// </summary>
        public DateTime? Last_activity { get => _last_activity; set => _last_activity = value; } // może być null
    }
}
