using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    class Products
    {
        private string _tableName = "products";
        public string TableName { get => _tableName; }

        private int _idproduct = -1;
        private int _idtype = -1;
        private int _idplatform = -1;
        private string _name;
        private string _subname;
        private int _buy_price = -1;
        private int _sell_price = -1;
        private int _rent_price = -1;
        private DateTime? _date_added;
        private DateTime? _last_update;

        public int Idproduct { get => _idproduct; set => _idproduct = value; }
        public int Idtype { get => _idtype; set => _idtype = value; }
        public int Idplatform { get => _idplatform; set => _idplatform = value; }
        public string Name { get => _name; set => _name = value; } // może być null
        public string Subname { get => _subname; set => _subname = value; } // może być null
        public int Buy_price { get => _buy_price; set => _buy_price = value; } // może być null
        public int Sell_price { get => _sell_price; set => _sell_price = value; } // może być null
        public int Rent_price { get => _rent_price; set => _rent_price = value; } // może być null
        /// <summary>
        /// Uwaga! Metoda może zwracać null!
        /// </summary>
        public DateTime? Date_added { get => _date_added; set => _date_added = value; } // może być null
        /// <summary>
        /// Uwaga! Metoda może zwracać null!
        /// </summary>
        public DateTime? Last_update { get => _last_update; set => _last_update = value; } // może być null
    }
}