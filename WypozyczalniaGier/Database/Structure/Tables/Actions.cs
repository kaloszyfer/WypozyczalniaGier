using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaGier.Database.Structure.Tables
{
    public class Actions
    {
        private string _tableName = "actions";
        public string TableName { get => _tableName; }

        private int _idactions = -1;
        private int _idtype_of_event = -1;
        private int _idproduct = -1;
        private int _operator = -1;
        private int _client = -1;
        private int _quantity = -1;
        private DateTime? _suggest_date;
        private int _idactionstart = -1;
        private DateTime? _date_added;
        private string _actionscol;

        public int Idactions { get => _idactions; set => _idactions = value; }
        public int Idtype_of_event { get => _idtype_of_event; set => _idtype_of_event = value; }
        public int Idproduct { get => _idproduct; set => _idproduct = value; }
        public int Operator { get => _operator; set => _operator = value; }
        public int Client { get => _client; set => _client = value; }
        public int Quantity { get => _quantity; set => _quantity = value; } // może być null
        /// <summary>
        /// Uwaga! Metoda może zwracać null!
        /// </summary>
        public DateTime? Suggest_date { get => _suggest_date; set => _suggest_date = value; } // może być null
        public int Idactionstart { get => _idactionstart; set => _idactionstart = value; } // może być null
        /// <summary>
        /// Uwaga! Metoda może zwracać null!
        /// </summary>
        public DateTime? Date_added { get => _date_added; set => _date_added = value; } // może być null
        public string Actionscol { get => _actionscol; set => _actionscol = value; } // może być null
    }
}